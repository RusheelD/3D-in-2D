using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData3D : MonoBehaviour
{
    public TileTypes type;
    public MapData3D map;
    bool triggered = false;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.name == "Player3D" && type == TileTypes.TELEPORTER && !triggered)
        {
            collision.gameObject.SetActive(false);
            triggered = true;
            map.world.IncrementLevel();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.name == "Player3D" && type == TileTypes.TELEPORTER)
        {
            triggered = false;
        }
    }
}
