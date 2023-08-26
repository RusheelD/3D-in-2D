using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData3D : MonoBehaviour
{
    public TileTypes[,,] level;
    public GameObject[,,] tiles;
    public MapData4D map4D;
    public int size;
    public Spaces3D space;
    public MaterialsContainer materials;
    public PlayerController3D player;
    public WorldMapData3D world;
    public bool linked2D;
}
