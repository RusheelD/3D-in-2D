using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInitializer3D : MonoBehaviour
{
    public WorldMapData3D worldData;
    public MapData2D data2D;
    public MapData3D data3D;
    [SerializeField] GameObject map2D;
    [SerializeField] GameObject map3D;
    [SerializeField] GameObject tile2D;
    [SerializeField] GameObject tile3D;
    [SerializeField] GameObject player2D;
    [SerializeField] GameObject player3D;
    [SerializeField] Sprite tileTextured;
    [SerializeField] Sprite empty;
    [SerializeField] Sprite teleporter;
    [SerializeField] Sprite tileTexturedTop;
    [SerializeField] Sprite emptyTop;
    [SerializeField] Sprite teleporterTop;

    GameObject wall1_2D;
    GameObject wall2_2D;

    GameObject wall1_3D;
    GameObject wall2_3D;
    GameObject wall3_3D;
    GameObject wall4_3D;
    // Start is called before the first frame update
    void Start()
    {
        MaterialsContainer materials = GetComponent<MaterialsContainer>();
        worldData = GetComponent<WorldMapData3D>();

        worldData.map2D = Instantiate(map2D, new Vector3(0, 0, 0), Quaternion.identity, transform);
        worldData.map3D = Instantiate(map3D, new Vector3(0, 0, 0), Quaternion.identity, transform);
        worldData.playerObject2D = Instantiate(player2D, new Vector3(0, 0, 0), Quaternion.identity);
        worldData.playerObject3D = Instantiate(player3D, new Vector3(0, 0, 0), Quaternion.identity);
        worldData.playerObject2D.transform.name = "Player2D";
        worldData.playerObject3D.transform.name = "Player3D";
        worldData.playerObject3D.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        worldData.playerObject3D.SetActive(false);
        worldData.initializer = this;

        worldData.map2D.name = "Map 2D";
        worldData.map3D.name = "Map 3D";

        data2D = worldData.map2D.GetComponent<MapData2D>();
        data3D = worldData.map3D.GetComponent<MapData3D>();

        worldData.data2D = data2D;
        worldData.data3D = data3D;
        worldData.mainCamera = Camera.main.GetComponent<CameraController>();
        worldData.mainCamera.mapData = data3D;

        MapRenderer2D mRenderer2D = worldData.map2D.GetComponent<MapRenderer2D>();
        MapRenderer3D mRenderer3D = worldData.map3D.GetComponent<MapRenderer3D>();

        mRenderer2D.map = data2D;
        mRenderer2D.tileTextured = tileTextured;
        mRenderer2D.empty = empty;
        mRenderer2D.tileTexturedTop = tileTexturedTop;
        mRenderer2D.emptyTop = emptyTop;
        mRenderer2D.teleporter = teleporter;
        mRenderer2D.teleporterTop = teleporterTop;

        mRenderer3D.map = data3D;
        mRenderer3D.tileTextured = materials.Ground;
        mRenderer3D.teleporter = materials.Teleporter;
        mRenderer3D.empty = materials.Air;
        mRenderer3D.playerMaterial = materials.Player;

        data2D.map3D = worldData.map3D.GetComponent<MapData3D>();
        data2D.world = worldData;
        data2D.plane = Planes2D.X;
        data2D.player = worldData.playerObject2D.GetComponent<PlayerController2D>();
        data2D.player.map = data2D;
        data2D.tiles = null;

        data3D.map4D = null;
        data3D.world = worldData;
        data3D.tiles = null;
        data3D.linked2D = true;
        data3D.space = Spaces3D.XZ;
        data3D.materials = materials;
        data3D.player = worldData.playerObject3D.GetComponent<PlayerController3D>();

        wall1_2D = Instantiate(tile2D, new Vector3(0, 0, 1), Quaternion.identity, worldData.map2D.transform);
        wall2_2D = Instantiate(tile2D, new Vector3(0, 0, 1), Quaternion.identity, worldData.map2D.transform);

        wall1_2D.GetComponent<Rigidbody2D>().angularDrag = 0;
        wall2_2D.GetComponent<Rigidbody2D>().angularDrag = 0;

        wall1_2D.transform.localScale = new Vector3(1, 1, 1);
        wall2_2D.transform.localScale = new Vector3(1, 1, 1);

        wall1_2D.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        wall2_2D.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        wall1_2D.transform.name = "Wall";
        wall2_2D.transform.name = "Wall";

        wall1_3D = Instantiate(tile3D, new Vector3(0, 0, 1), Quaternion.identity, worldData.map3D.transform);
        wall2_3D = Instantiate(tile3D, new Vector3(0, 0, 1), Quaternion.identity, worldData.map3D.transform);
        wall3_3D = Instantiate(tile3D, new Vector3(0, 0, 1), Quaternion.identity, worldData.map3D.transform);
        wall4_3D = Instantiate(tile3D, new Vector3(0, 0, 1), Quaternion.identity, worldData.map3D.transform);

        wall1_3D.transform.localScale = new Vector3(1, 1, 1);
        wall2_3D.transform.localScale = new Vector3(1, 1, 1);
        wall3_3D.transform.localScale = new Vector3(1, 1, 1);
        wall4_3D.transform.localScale = new Vector3(1, 1, 1);

        wall1_3D.GetComponent<MeshRenderer>().material = GetComponent<MaterialsContainer>().Air;
        wall2_3D.GetComponent<MeshRenderer>().material = GetComponent<MaterialsContainer>().Air;
        wall3_3D.GetComponent<MeshRenderer>().material = GetComponent<MaterialsContainer>().Air;
        wall4_3D.GetComponent<MeshRenderer>().material = GetComponent<MaterialsContainer>().Air;

        wall1_3D.GetComponent<MeshRenderer>().enabled = false;
        wall2_3D.GetComponent<MeshRenderer>().enabled = false;
        wall3_3D.GetComponent<MeshRenderer>().enabled = false;
        wall4_3D.GetComponent<MeshRenderer>().enabled = false;

        wall1_3D.transform.name = "Wall";
        wall2_3D.transform.name = "Wall";
        wall3_3D.transform.name = "Wall";
        wall4_3D.transform.name = "Wall";

        Delete3DLevel();
        SetupLevel2D(Levels.Level0);
    }

    public void SetupLevel2D(Level3D level)
    {
        float xOffset = data2D.xOffset;
        float yOffset = data2D.yOffset;
        float zOffset = data2D.zOffset;
        Delete2DLevel();
        worldData.map2D.SetActive(true);
        wall1_2D.SetActive(true);
        wall2_2D.SetActive(true);

        int length = level.map.GetLength(0);

        data2D.plane = level.startPlane;
        data2D.player.transform.position = level.startPos;
        data2D.player.SetX(level.startPos.x);
        data2D.player.SetY(level.startPos.y);
        data2D.player.SetZ(level.startPos.z);

        data2D.size = length;
        data2D.tiles = new GameObject[data2D.size, data2D.size];

        data3D.level = level.map;
        data3D.size = length;

        for (int y = length / 2 * -1; y <= length / 2; y++)
        {
            for (int x = length / 2 * -1; x <= length / 2; x++)
            {
                int mapX = x + length / 2;
                int mapY = y + length / 2;

                GameObject tile = Instantiate(tile2D, new Vector3(x + xOffset, y + yOffset, 1 + zOffset), Quaternion.identity, worldData.map2D.transform);
                tile.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                tile.GetComponent<TileData2D>().map = data2D;
                tile.name = "Tile (" + x + ", " + y + ")";

                data2D.tiles[mapY, mapX] = tile;
            }
        }
        worldData.map2D.GetComponent<MapRenderer2D>().RerenderTiles();

        wall1_2D.transform.position = new Vector3(length / 2 + 1, 0, 1);
        wall2_2D.transform.position = new Vector3(length / -2 - 1, 0, 1);

        wall1_2D.transform.localScale = new Vector3(1, length * 2, 1);
        wall2_2D.transform.localScale = new Vector3(1, length * 2, 1);
        data2D.player.gameObject.SetActive(true);
    }

    public void Delete2DLevel()
    {
        worldData.map2D.SetActive(false);
        data2D.player.gameObject.SetActive(false);

        wall1_2D.SetActive(false);
        wall2_2D.SetActive(false);

        if (data2D.tiles != null)
        {
            for (int i = 0; i < data2D.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < data2D.tiles.GetLength(0); j++)
                {
                    Destroy(data2D.tiles[i, j]);
                }
            }
        }

        data2D.tiles = null;
    }
    public void Delete3DLevel()
    {
        wall1_3D.SetActive(false);
        wall2_3D.SetActive(false);
        wall3_3D.SetActive(false);
        wall4_3D.SetActive(false);

        worldData.map3D.SetActive(false);
        data3D.player.gameObject.SetActive(false);
        if (data3D.tiles != null)
        {
            for (int i = 0; i < data3D.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < data3D.tiles.GetLength(0); j++)
                {
                    for (int k = 0; k < data3D.tiles.GetLength(0); k++)
                    {

                        Destroy(data3D.tiles[i, j, k]);
                    }
                }
            }
        }

        data3D.tiles = null;
    }

    public void SetupLevel3D(Level3D level)
    {
        Delete3DLevel();
        worldData.map3D.SetActive(true);
        wall1_3D.SetActive(true);
        wall2_3D.SetActive(true);
        wall3_3D.SetActive(true);
        wall4_3D.SetActive(true);

        int length = level.map.GetLength(0);

        data3D.space = level.startSpace;
        data3D.player.transform.position = level.startPos;
        data3D.player.transform.rotation = Quaternion.identity;
        data3D.player.Reset();
        Camera.main.GetComponent<CameraController>().Reset();
        data3D.player.SetX(level.startPos.x);
        data3D.player.SetY(level.startPos.y);
        data3D.player.SetZ(level.startPos.z);

        data3D.size = length;
        data3D.tiles = new GameObject[data3D.size, data3D.size, data3D.size];

        data3D.level = level.map;
        data3D.size = length;

        for (int y = length / 2 * -1; y <= length / 2; y++)
        {
            for (int x = length / 2 * -1; x <= length / 2; x++)
            {
                for (int z = length / 2 * -1; z <= length / 2; z++)
                {
                    int mapX = x + length / 2;
                    int mapY = y + length / 2;
                    int mapZ = z + length / 2;

                    GameObject tile = Instantiate(tile3D, new Vector3(x, y, z), Quaternion.identity, worldData.map3D.transform);
                    tile.GetComponent<TileData3D>().map = data3D;
                    tile.name = "Tile (" + x + ", " + y + ", " + z + ")";

                    data3D.tiles[mapY, mapX, mapZ] = tile;
                }
                
            }
        }
        worldData.map3D.GetComponent<MapRenderer3D>().Render3DTiles();

        wall1_3D.transform.position = new Vector3(length / 2 + 1, 0, 0);
        wall2_3D.transform.position = new Vector3(length / -2 - 1, 0, 0);
        wall3_3D.transform.position = new Vector3(0, 0, length / 2 + 1);
        wall4_3D.transform.position = new Vector3(0, 0, length / -2 - 1);

        wall1_3D.transform.localScale = new Vector3(1, length * 2, length * 2);
        wall2_3D.transform.localScale = new Vector3(1, length * 2, length * 2);
        wall3_3D.transform.localScale = new Vector3(length * 2, length * 2, 1);
        wall4_3D.transform.localScale = new Vector3(length * 2, length * 2, 1);

        // wall1_3D.transform.Rotate(90, 0, 90);
        // wall2_3D.transform.Rotate(90, 0, 90);
        data3D.player.gameObject.SetActive(true);
    }
}
