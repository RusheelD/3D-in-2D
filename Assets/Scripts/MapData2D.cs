using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData2D : MonoBehaviour
{
    public TileTypes[,] level;
    public int size;
    public GameObject[,] tiles;
    public MapData3D map3D;
    public Planes2D plane;
    public PlayerController2D player;
    public WorldMapData3D world;

    public float xOffset = 0;
    public float yOffset = -0.3f;
    public float zOffset = 0;
}
