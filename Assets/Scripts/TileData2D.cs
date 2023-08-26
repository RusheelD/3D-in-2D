using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData2D : MonoBehaviour
{
    public TileTypes type;
    public MapData2D map;
    bool triggered = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Player2D" && type == TileTypes.TELEPORTER && !triggered)
        {
            collision.gameObject.SetActive(false);
            triggered = true;
            map.world.Show3DLevel();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.name == "Player2D" && type == TileTypes.TELEPORTER)
        {
            triggered = false;
        }
    }
}
