using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class WorldMapData3D : MonoBehaviour
{
    public CameraController mainCamera;
    public GameObject playerObject2D;
    public GameObject playerObject3D;
    public GameObject map2D;
    public MapData2D data2D;
    public MapData3D data3D;
    public GameObject map3D;
    public MapInitializer3D initializer;
    public int levelNum = 0;
    Canvas canvas;
    TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        canvas = transform.GetComponentInChildren<Canvas>();
        textMeshProUGUI = canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public void IncrementLevel()
    {
        mainCamera.flatMode = true;
        initializer.Delete3DLevel();
        levelNum = (levelNum + 1) % Levels.Levels3D.Length;
        initializer.SetupLevel2D(Levels.Levels3D[levelNum]);
    }

    public void Show3DLevel()
    {
        mainCamera.flatMode = false;
        initializer.Delete2DLevel();
        initializer.SetupLevel3D(Levels.Levels3D[levelNum]);
    }

    void Update()
    {
        string x = string.Format("{0:0.00}", data2D.player.x);
        string y = string.Format("{0:0.00}", data2D.player.y);
        string z = string.Format("{0:0.00}", data2D.player.z);
        // Debug.Log("(" + x + ", " + y + ", " + z + ")");
        textMeshProUGUI.SetText("(" + x + ", " + y + ", " + z+ ")");
    }
}
