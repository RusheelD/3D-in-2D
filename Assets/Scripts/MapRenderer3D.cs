using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapRenderer3D : MonoBehaviour
{
    // public void RerenderTiles() { }
    
    public MapData3D map;
    [SerializeField] public GameObject PlayerRepresentation;
    [SerializeField] public GameObject Tile;
    public Material tileTextured;
    public Material teleporter;
    public Material empty;
    public Material playerMaterial;
    GameObject playerPin;
    GameObject tempTileBeneath;
    MeshRenderer pRenderer;
    MeshRenderer ttRenderer;
    bool swapped;
    bool toggled;
    // Start is called before the first frame update
    void Start()
    {
        swapped = false;
        toggled = false;
        playerPin = Instantiate(PlayerRepresentation, new Vector3(0, 0, 0), Quaternion.identity, transform);
        tempTileBeneath = Instantiate(Tile, new Vector3(0, 0, 0), Quaternion.identity, transform);
        pRenderer = playerPin.GetComponent<MeshRenderer>();
        ttRenderer = tempTileBeneath.GetComponent<MeshRenderer>();
        playerPin.SetActive(false);
        tempTileBeneath.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && !toggled)
        {
            if(map.linked2D)
            {
                return;
            }
            ToggleYView();
            toggled = true;
        }
        else if (!Input.GetKeyDown(KeyCode.Y))
        {
            toggled = false;
        }

        if (map.player.yView)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && !swapped)
        {
            SwitchSpace();
            if (map.linked2D)
            {
                Render3DTiles();
            }
            else
            {
                RerenderTiles();
            }
            swapped = true;
        }
        else if (!Input.GetKeyDown(KeyCode.R))
        {
            swapped = false;
        }
    }

    public void SwitchSpace()
    {
        map.space = map.space == Spaces3D.XZ ? Spaces3D.WZ : (map.space == Spaces3D.WZ ? Spaces3D.WX : Spaces3D.XZ);
        map.player.SwitchSpace();
    }

    void ToggleYView()
    {
        if (map.player.yView)
        {
            map.player.yView = false;
            playerPin.SetActive(false);
            tempTileBeneath.SetActive(false);
            map.player.gameObject.SetActive(true);
            RerenderTiles();
        }
        else
        {
            map.player.yView = true;
            playerPin.SetActive(true);
            map.player.gameObject.SetActive(false);
            RenderYView();
        }
    }

    void RenderYView()
    {
        int playerX = map.player.GetIntX() + map.size / 2;
        int playerY = map.player.GetIntY() + map.size / 2;
        int playerZ = map.player.GetIntZ() + map.size / 2;
        int playerW = map.player.GetIntW() + map.size / 2;

        playerPin.transform.position = new Vector3(playerX - map.size / 2, playerZ - map.size / 2, playerW - map.size / 2);
        pRenderer.material = new Material(playerMaterial);
        for (int x = map.size / 2 * -1; x <= map.size / 2; x++)
        {
            for (int z = map.size / 2 * -1; z <= map.size / 2; z++)
            {
                for (int w = map.size / 2 * -1; w <= map.size / 2; w++)
                {
                    int mapX = x + map.size / 2;
                    int mapZ = z + map.size / 2;
                    int mapW = w + map.size / 2;

                    int y = map.size - 1;
                    TileTypes curTile = map.map4D.level[y, mapX, mapZ, mapW];

                    for (y = map.size - 1; y >= 0; y--)
                    {
                        curTile = map.map4D.level[y, mapX, mapZ, mapW];
                        if (curTile != TileTypes.AIR)
                        {
                            break;
                        }
                    }

                    if (mapX == playerX && mapZ == playerZ && mapW == playerW && playerY < (y + 1))
                    {
                        pRenderer.material.color = new Color(pRenderer.material.color.r, pRenderer.material.color.g, pRenderer.material.color.b, 1 / 2f);
                    }

                    Color texColor = tileTextured.color;
                    Color teleporterColor = teleporter.color;
                    float colorScale = (((float)(map.size - 1) - y) / (float)(map.size - 1)) + 1;

                    if (curTile == TileTypes.TELEPORTER && y > 0)
                    {
                        int newY = y - 1;
                        TileTypes tempTile = map.map4D.level[newY, mapX, mapZ, mapW];
                        tempTileBeneath.transform.position = new Vector3(x, z, 1.1f);
                        tempTileBeneath.SetActive(true);

                        while (tempTile == TileTypes.AIR && y > 0)
                        {
                            newY--;
                            tempTile = map.map4D.level[newY, mapX, mapZ, mapW];
                        }
                        float newColorScale = (((float)(map.size - 1) - newY) / (float)(map.size - 1)) + 1;

                        switch (tempTile)
                        {
                            case TileTypes.GROUND:
                                ttRenderer.material = new Material(tileTextured);
                                ttRenderer.material.color = new Color(texColor.r / newColorScale, texColor.g / newColorScale, texColor.b / newColorScale);
                                break;
                            default:
                                ttRenderer.material = new Material(tileTextured);
                                ttRenderer.material.color = new Color(texColor.r / newColorScale, texColor.g / newColorScale, texColor.b / newColorScale);
                                break;
                        }

                    }

                    GameObject tile = map.tiles[mapZ, mapX, mapW];
                    MeshRenderer tileRenderer = tile.GetComponent<MeshRenderer>();
                    BoxCollider tileCollider = tile.GetComponent<BoxCollider>();
                    tile.GetComponent<TileData3D>().type = curTile;
                    tileRenderer.enabled = true;

                    switch (curTile)
                    {
                        case TileTypes.AIR:
                            tileRenderer.material = new Material(empty);
                            tileRenderer.enabled = false;
                            tileCollider.enabled = false;
                            break;
                        case TileTypes.GROUND:
                            tileRenderer.material = new Material(tileTextured);
                            tileRenderer.material.color = new Color(texColor.r / colorScale, texColor.g / colorScale, texColor.b / colorScale);
                            tileCollider.enabled = true;
                            break;
                        case TileTypes.TELEPORTER:
                            tileRenderer.material = new Material(teleporter);
                            tileRenderer.material.color = new Color(teleporterColor.r / colorScale, teleporterColor.g / colorScale, teleporterColor.b / colorScale);
                            tileCollider.enabled = true;
                            tileCollider.isTrigger = true;
                            break;
                        default:
                            tileRenderer.material = new Material(empty);
                            tileRenderer.enabled = false;
                            tileCollider.enabled = false;
                            break;
                    }
                }
            }
        }
    }

    public void Render3DTiles()
    {
        for (int y = map.size / 2 * -1; y <= map.size / 2; y++)
        {
            for (int x = map.size / 2 * -1; x <= map.size / 2; x++)
            {
                for (int z = map.size / 2 * -1; z <= map.size / 2; z++)
                {
                    int mapX = x + map.size / 2;
                    int mapY = y + map.size / 2;
                    int mapZ = z + map.size / 2;

                    GameObject tile = map.tiles[mapY, mapX, mapZ];
                    MeshRenderer tileRenderer = tile.GetComponent<MeshRenderer>();
                    BoxCollider tileCollider = tile.GetComponent<BoxCollider>();
                    tileCollider.isTrigger = false;

                    TileTypes curTile = map.level[mapY, mapX, mapZ];

                    tile.GetComponent<TileData3D>().type = curTile;
                    tileRenderer.enabled = true;

                    switch (curTile)
                    {
                        case TileTypes.AIR:
                            tileRenderer.material = new Material(empty);
                            tileRenderer.enabled = false;
                            tileCollider.enabled = false;
                            break;
                        case TileTypes.GROUND:
                            tileRenderer.material = new Material(tileTextured);
                            tileCollider.enabled = true;
                            break;
                        case TileTypes.TELEPORTER:
                            tileRenderer.material = new Material(teleporter);
                            tileCollider.enabled = true;
                            tileCollider.isTrigger = true;
                            break;
                        default:
                            tileRenderer.material = new Material(empty);
                            tileRenderer.enabled = false;
                            tileCollider.enabled = false;
                            break;
                    }
                }
            }
        }
    }

    public void RerenderTiles()
    {
        for (int y = map.size / 2 * -1; y <= map.size / 2; y++)
        {
            for (int x = map.size / 2 * -1; x <= map.size / 2; x++)
            {
                for (int z = map.size / 2 * -1; z <= map.size / 2; z++)
                {
                    int mapX = x + map.size / 2;
                    int mapY = y + map.size / 2;
                    int mapZ = z + map.size / 2;
                    int playerX = map.player.GetIntX() + map.size / 2;
                    int playerZ = map.player.GetIntZ() + map.size / 2;
                    int playerW = map.player.GetIntW() + map.size / 2;

                    GameObject tile = map.tiles[mapY, mapX, mapZ];
                    MeshRenderer tileRenderer = tile.GetComponent<MeshRenderer>();
                    BoxCollider tileCollider = tile.GetComponent<BoxCollider>();
                    tileCollider.isTrigger = false;

                    TileTypes curTile;

                    switch (map.space)
                    {
                        case Spaces3D.XZ:
                            curTile = map.map4D.level[mapY, mapX, mapZ, playerW];
                            break;
                        case Spaces3D.WZ:
                            curTile = map.map4D.level[mapY, playerX, mapZ, mapX];
                            break;
                        case Spaces3D.WX:
                            curTile = map.map4D.level[mapY, mapX, playerZ, mapZ];
                            break;
                        default:
                            curTile = map.map4D.level[mapY, mapX, mapZ, playerW];
                            break;
                    }

                    tile.GetComponent<TileData3D>().type = curTile;
                    tileRenderer.enabled = true;

                    switch (curTile)
                    {
                        case TileTypes.AIR:
                            tileRenderer.material = new Material(empty);
                            tileRenderer.enabled = false;
                            tileCollider.enabled = false;
                            break;
                        case TileTypes.GROUND:
                            tileRenderer.material = new Material(tileTextured);
                            tileCollider.enabled = true;
                            break;
                        case TileTypes.TELEPORTER:
                            tileRenderer.material = new Material(teleporter);
                            tileCollider.enabled = true;
                            tileCollider.isTrigger = true;
                            break;
                        default:
                            tileRenderer.material = new Material(empty);
                            tileRenderer.enabled = false;
                            tileCollider.enabled = false;
                            break;
                    }
                }
            }
        }
    }
}