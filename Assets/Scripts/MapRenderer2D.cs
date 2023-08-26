using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRenderer2D : MonoBehaviour
{
    public MapData2D map;
    [SerializeField] public GameObject PlayerRepresentation;
    [SerializeField] public GameObject Tile;
    public Sprite tileTextured;
    public Sprite teleporter;
    public Sprite empty;
    public Sprite tileTexturedTop;
    public Sprite teleporterTop;
    public Sprite emptyTop;
    GameObject playerPin;
    GameObject tempTileBeneath;
    List<GameObject> tempTilesBeneath;
    SpriteRenderer pRenderer;
    SpriteRenderer ttRenderer;
    bool swapped;
    bool toggled;
    // Start is called before the first frame update
    void Start()
    {
        swapped = false;
        toggled = false;
        playerPin = Instantiate(PlayerRepresentation, new Vector3(0, 0, 0), Quaternion.identity, transform);
        // tempTileBeneath = Instantiate(Tile, new Vector3(0, 0, 0), Quaternion.identity, transform);
        pRenderer = playerPin.GetComponent<SpriteRenderer>();
        playerPin.SetActive(false);
        tempTilesBeneath = new List<GameObject>();
        // tempTileBeneath.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && !toggled)
        {
            ToggleYView();
            toggled = true;
        } else if (!Input.GetKeyDown(KeyCode.Y))
        {
            toggled = false;
        }

        if (map.player.yView)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && !swapped) {
            SwitchPlane();
            RerenderTiles();
            swapped = true;
        } else if (!Input.GetKeyDown(KeyCode.R))
        {
            swapped = false;
        }
    }

    public void SwitchPlane()
    {
        map.plane = map.plane == Planes2D.X ? Planes2D.Z : Planes2D.X;
        map.player.SwitchPlane();
    }

    void ToggleYView()
    {
        if(map.player.yView)
        {
            map.player.yView = false;
            playerPin.SetActive(false);
            tempTilesBeneath.ForEach(Destroy);
            tempTilesBeneath.Clear();
            map.player.gameObject.SetActive(true);
            RerenderTiles();
        } else
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

        playerPin.transform.position = new Vector3(playerX - map.size / 2 + map.xOffset, playerZ - map.size / 2 + map.yOffset, 0 + map.zOffset);
        pRenderer.color = new Color(pRenderer.color.r, pRenderer.color.g, pRenderer.color.b, 1);
        for (int x = map.size / 2 * -1; x <= map.size / 2; x++)
        {
            for (int z = map.size / 2 * -1; z <= map.size / 2; z++)
            {
                int mapX = x + map.size / 2;
                int mapZ = z + map.size / 2;

                int y = map.size-1;
                TileTypes curTile = map.map3D.level[y, mapX, mapZ];

                for (y = map.size - 1; y >= 0; y--)
                {
                    curTile = map.map3D.level[y, mapX, mapZ];
                    if (curTile != TileTypes.AIR)
                    {
                        break;
                    }
                }

                if (mapX == playerX && mapZ == playerZ && playerY < (y + 1))
                {
                    pRenderer.color = new Color(pRenderer.color.r, pRenderer.color.g, pRenderer.color.b, 1 / 2f);
                }

                Color grassColor = map.map3D.materials.Ground.color;
                Color teleporterColor = map.map3D.materials.Teleporter.color;
                float colorScale = (((float)(map.size-1) - y) / (float)(map.size - 1)) + 1;

                if (curTile == TileTypes.TELEPORTER && y > 0)
                {
                    int newY = y - 1;
                    TileTypes tempTile = map.map3D.level[newY, mapX, mapZ];
                    tempTilesBeneath.Add(Instantiate(Tile, new Vector3(0, 0, 0), Quaternion.identity, transform));
                    GameObject tempTileBeneath = tempTilesBeneath[tempTilesBeneath.Count - 1];
                    ttRenderer = tempTileBeneath.GetComponent<SpriteRenderer>();
                    tempTileBeneath.transform.position = new Vector3(x + map.xOffset, z + map.yOffset, 1.1f + map.zOffset);
                    tempTileBeneath.SetActive(true);

                    while (tempTile == TileTypes.AIR && y > 0)
                    {
                        newY--;
                        tempTile = map.map3D.level[newY, mapX, mapZ];
                    }
                    float newColorScale = (((float)(map.size - 1) - newY) / (float)(map.size - 1)) + 1;

                    switch (tempTile)
                    {
                        case TileTypes.GROUND:
                            ttRenderer.sprite = tileTexturedTop;
                            ttRenderer.color = new Color(grassColor.r / newColorScale, grassColor.g / newColorScale, grassColor.b / newColorScale);
                            break;
                        default:
                            ttRenderer.sprite = tileTexturedTop;
                            ttRenderer.color = new Color(grassColor.r / newColorScale, grassColor.g / newColorScale, grassColor.b / newColorScale);
                            break;
                    }

                }

                GameObject tile = map.tiles[mapZ, mapX];
                SpriteRenderer tileRenderer = tile.GetComponent<SpriteRenderer>();
                BoxCollider2D tileCollider = tile.GetComponent<BoxCollider2D>();
                tile.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                tile.GetComponent<TileData2D>().type = curTile;

                switch (curTile)
                {
                    case TileTypes.AIR:
                        tileRenderer.color = map.map3D.materials.Air.color;
                        tileRenderer.sprite = emptyTop;
                        tileCollider.enabled = false;
                        break;
                    case TileTypes.GROUND:
                        tileRenderer.color = new Color(grassColor.r / colorScale, grassColor.g / colorScale, grassColor.b / colorScale);
                        tileRenderer.sprite = tileTexturedTop;
                        tileCollider.enabled = true;
                        break;
                    case TileTypes.TELEPORTER:
                        tileRenderer.color = new Color(teleporterColor.r / colorScale, teleporterColor.g / colorScale, teleporterColor.b / colorScale);
                        tileRenderer.sprite = teleporterTop;
                        tileCollider.enabled = true;
                        tileCollider.isTrigger = true;
                        break;
                    default:
                        tileRenderer.color = map.map3D.materials.Air.color;
                        tileRenderer.sprite = emptyTop;
                        tileCollider.enabled = false;
                        break;
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
                int mapX = x + map.size / 2;
                int mapY = y + map.size / 2;
                int playerX = map.player.GetIntX() + map.size / 2;
                int playerZ = map.player.GetIntZ() + map.size / 2;

                GameObject tile = map.tiles[mapY, mapX];
                SpriteRenderer tileRenderer = tile.GetComponent<SpriteRenderer>();
                BoxCollider2D tileCollider = tile.GetComponent<BoxCollider2D>();
                tileCollider.isTrigger = false;
                tile.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

                TileTypes curTile = map.plane == Planes2D.X ? map.map3D.level[mapY, mapX, playerZ] : map.map3D.level[mapY, playerX, mapX];
                tile.GetComponent<TileData2D>().type = curTile;

                switch (curTile)
                {
                    case TileTypes.AIR:
                        tileRenderer.color = map.map3D.materials.Air.color;
                        tileRenderer.sprite = empty;
                        tileCollider.enabled = false;
                        break;
                    case TileTypes.GROUND:
                        tileRenderer.color = map.map3D.materials.Ground.color;
                        tileRenderer.sprite = tileTextured;
                        tileCollider.enabled = true;
                        break;
                    case TileTypes.TELEPORTER:
                        tileRenderer.color = map.map3D.materials.Teleporter.color;
                        tileRenderer.sprite = teleporter;
                        tileCollider.enabled = true;
                        tileCollider.isTrigger = true;
                        break;
                    default:
                        tileRenderer.color = map.map3D.materials.Air.color;
                        tileRenderer.sprite = empty;
                        tileCollider.enabled = false;
                        break;
                }
            }
        }
    }
}
