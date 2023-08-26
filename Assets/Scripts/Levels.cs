using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Levels
{
    static TileTypes a = TileTypes.AIR;
    static TileTypes g = TileTypes.GROUND;
    static TileTypes t = TileTypes.TELEPORTER;


    static TileTypes[,,] tilesLevel0 = {
        { 
            { g, g, g }, 
            { g, g, g }, 
            { g, g, g } 
        },
        { 
            { a, a, a }, 
            { a, a, a }, 
            { a, a, t } 
        },
        { 
            { a, a, a }, 
            { a, a, a }, 
            { a, a, a } 
        }
    };

    static TileTypes[,,] tilesLevel1 = {
        {
            { g, g, g, g, g },
            { g, g, g, g, g },
            { g, g, g, g, g },
            { g, g, g, g, g },
            { g, g, g, g, g },
        },
        {
            { g, g, g, g, g },
            { a, a, a, a, g },
            { a, a, a, a, a },
            { a, a, a, a, g },
            { g, g, g, g, g },
        },
        {
            { a, a, a, a, a },
            { a, a, a, a, g },
            { a, a, a, a, a },
            { a, a, a, a, g },
            { g, g, g, g, g },
        },
        {
            { a, a, a, a, a },
            { a, a, a, a, a },
            { a, a, a, a, a },
            { a, a, a, a, a },
            { g, g, g, g, g },
        },
        {
            { a, a, a, a, a },
            { a, a, a, a, a },
            { a, a, a, a, a },
            { g, a, a, a, a },
            { g, g, t, a, a },
        }
    };

    static TileTypes[,,] tilesLevel2 = {
        {
            { g, g, g, g, g },
            { g, g, g, g, g },
            { g, g, g, g, g },
            { g, g, g, g, g },
            { g, g, g, g, g },
        },
        {
            { g, g, g, g, g },
            { g, t, g, g, a },
            { g, g, g, g, g },
            { g, a, a, a, a },
            { g, g, a, a, a },
        },
        {
            { g, g, g, g, g },
            { g, a, g, a, a },
            { g, g, g, g, a },
            { g, a, a, a, a },
            { g, a, a, a, a },
        },
        {
            { g, g, g, g, g },
            { g, a, a, a, g },
            { g, g, g, g, g },
            { g, a, a, a, a },
            { a, a, a, a, a },
        },
        {
            { g, g, g, g, g },
            { g, g, g, g, g },
            { g, g, g, g, g },
            { a, a, a, a, a },
            { a, a, a, a, a },
        }
    };

    static TileTypes[,,] tilesLevel3 = {
        {
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
        },
        {
            { g, g, g, g, g, g, g },
            { g, a, a, a, a, a, g },
            { g, g, g, a, g, g, g },
            { g, t, g, a, g, g, g },
            { g, g, g, a, g, g, g },
            { g, a, a, a, g, g, g },
            { g, g, g, g, g, g, g },
        },
        {
            { g, g, g, g, g, g, g },
            { g, a, a, a, a, a, g },
            { g, g, g, a, g, a, g },
            { g, a, g, a, g, g, g },
            { g, g, g, a, g, g, g },
            { g, a, a, g, g, g, g },
            { g, g, g, g, g, g, g },
        },
        {
            { g, g, g, g, g, g, g },
            { g, g, g, a, g, a, g },
            { g, g, g, g, g, a, g },
            { g, a, g, g, g, a, g },
            { g, g, g, g, g, g, g },
            { g, g, g, a, g, g, g },
            { g, g, g, g, g, g, g },
        },
        {
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, a, g },
            { g, g, g, g, g, a, g },
            { g, a, g, g, g, a, g },
            { g, g, g, a, g, a, g },
            { g, g, a, a, a, g, g },
            { g, g, g, g, g, g, g },
        },
        {
            { g, g, g, g, g, g, g },
            { g, a, a, a, a, g, g },
            { g, g, g, a, g, a, g },
            { g, a, a, a, g, a, g },
            { g, g, g, a, g, a, g },
            { g, a, a, a, a, a, g },
            { g, g, g, g, g, g, g },
        },
        {
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
        },
    };

    static TileTypes[,,] tilesLevel4 = {
        {
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
        },
        {
            { g, g, a, g, a, g, g },
            { g, a, g, g, g, a, g },
            { a, g, a, g, a, g, a },
            { g, g, g, a, g, g, g },
            { a, g, a, g, a, g, a },
            { g, a, g, g, g, a, g },
            { g, g, a, g, a, g, g },
        },
        {
            { g, a, a, g, a, a, g },
            { a, a, a, g, a, a, a },
            { a, a, a, a, a, a, a },
            { g, g, a, a, a, g, g },
            { a, a, a, a, a, a, a },
            { a, a, a, g, a, a, a },
            { g, a, a, g, a, a, g },
        },
        {
            { a, a, a, g, a, a, a },
            { a, a, g, a, g, a, a },
            { a, g, a, a, a, g, a },
            { g, a, a, a, a, a, g },
            { a, g, a, a, a, g, a },
            { a, a, g, a, g, a, a },
            { a, a, a, g, a, a, a },
        },
        {
            { t, g, g, a, g, g, a },
            { g, g, g, a, g, g, g },
            { g, g, g, g, g, g, g },
            { a, a, g, g, g, a, a },
            { g, g, g, g, g, g, g },
            { g, g, g, a, g, g, g },
            { a, g, g, a, g, g, a },
        },
        {
            { g, g, a, a, a, g, g },
            { g, a, a, a, a, a, g },
            { a, a, g, g, g, a, a },
            { a, a, g, g, g, a, a },
            { a, a, g, g, g, a, a },
            { g, a, a, a, a, a, g },
            { g, g, a, a, a, g, g },
        },
        {
            { t, g, a, a, a, g, t },
            { g, a, a, a, a, a, g },
            { a, a, a, a, a, a, a },
            { a, a, a, g, a, a, a },
            { a, a, a, a, a, a, a },
            { g, a, a, a, a, a, g },
            { t, g, a, a, a, g, t },
        },
    };

    static TileTypes[,,] tilesLevel5 = {
        {
            { t, g, g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g, g, g },
        },
        {
            { a, g, g, a, a, a, a, a, a },
            { a, a, g, a, g, a, g, a, a },
            { g, a, g, a, g, g, a, a, a },
            { a, a, a, a, g, a, a, a, a },
            { g, g, g, g, a, a, a, a, a },
            { a, g, g, a, a, a, a, a, a },
            { a, g, a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a, a, a },
        },
        {
            { g, g, g, g, g, g, g, a, a },
            { g, g, g, g, g, g, a, a, a },
            { g, g, g, g, g, a, a, a, a },
            { g, g, g, g, a, a, a, a, a },
            { g, g, g, a, a, a, a, a, a },
            { g, g, a, a, a, a, a, a, a },
            { g, a, a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a, a, a },
        },
        {
            { g, g, g, g, g, g, a, a, g },
            { g, g, g, g, g, a, a, g, g },
            { g, g, g, g, a, a, g, g, g },
            { g, g, g, a, a, g, g, g, g },
            { g, g, a, a, g, g, g, g, g },
            { g, a, a, g, g, g, g, g, g },
            { a, a, g, g, g, g, g, g, g },
            { a, g, g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g, g, g },
        },
        {
            { g, g, g, g, g, a, a, a, a },
            { g, g, g, g, a, a, a, a, a },
            { g, g, g, a, a, a, a, a, a },
            { g, g, a, a, a, a, a, g, a },
            { g, a, a, a, a, a, a, g, a },
            { a, a, a, a, a, a, a, g, a },
            { a, a, a, a, a, a, a, g, g },
            { a, a, a, a, a, a, a, g, a },
            { a, a, a, a, a, a, g, t, a },
        },
        {
            { g, g, g, g, a, a, g, g, g },
            { g, g, g, a, a, g, g, g, g },
            { g, g, a, a, g, g, g, g, g },
            { g, a, a, g, g, g, g, g, g },
            { a, a, g, g, g, g, g, g, g },
            { a, g, g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g, g, a },
            { g, g, g, g, g, g, g, g, g },
        },
        {
            { g, g, g, a, a, a, a, a, a },
            { g, g, a, a, a, a, a, a, a },
            { g, a, a, a, a, a, g, g, g },
            { a, a, a, a, a, g, a, a, a },
            { a, a, a, a, g, a, a, g, a },
            { a, a, a, a, g, a, g, a, a },
            { a, a, a, a, g, a, a, g, a },
            { a, g, g, g, a, a, g, a, a },
            { a, a, a, a, a, g, a, a, a },
        },
        {
            { g, g, a, a, g, g, g, g, a },
            { g, a, a, g, g, g, a, g, g },
            { a, a, g, g, g, g, g, a, g },
            { a, g, a, g, g, g, g, g, g },
            { g, g, g, g, a, g, g, g, g },
            { g, g, g, a, g, g, g, g, g },
            { a, g, g, g, g, g, g, g, g },
            { g, g, g, g, g, a, g, g, g },
            { g, a, g, g, g, g, g, g, g },
        },
        {
            { g, a, a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a, a, a },
        },
    };

    static TileTypes[,,] tilesLevel6 = {
        {
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
            { g, g, g, g, g, g, g },
        },
        {
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { g, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, g, a, a, a, g },
        },
        {
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, g, a, a, g, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, g, g },
        },
        {
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, g, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, g },
        },
        {
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, g, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, g },
        },
        {
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, g, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, g, a, a, a, a, g },
        },
        {
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, a, a, a, a },
            { a, a, a, g, a, a, t },
        },
    };


    public static Level3D Level0 = new Level3D (tilesLevel0, new Vector3(0, 0, 0), Planes2D.X);
    public static Level3D Level1 = new Level3D (tilesLevel1, new Vector3(0, -1, 0), Planes2D.X);
    public static Level3D Level2 = new Level3D(tilesLevel2, new Vector3(1, -1, 0), Planes2D.X);
    public static Level3D Level3 = new Level3D(tilesLevel3, new Vector3(-2, -2, -2), Planes2D.Z);
    public static Level3D Level4 = new Level3D(tilesLevel4, new Vector3(0, 4, 0), Planes2D.Z);
    public static Level3D Level5 = new Level3D(tilesLevel5, new Vector3(-4, 6, -4), Planes2D.X);
    public static Level3D Level6 = new Level3D(tilesLevel6, new Vector3(-3, -2, -3), Planes2D.X);


    public static Level3D[] Levels3D = { Level0, Level1, Level2, Level3, Level4, Level5, Level6 };
    
}
public interface Level
{}
public class Level3D: Level
{
    public Level3D(Vector3 startPos)
    {
        map = new TileTypes[0, 0, 0];
        this.startPos = startPos;
        this.startPlane = Planes2D.X;
        this.startSpace = Spaces3D.XZ;
    }
    public Level3D(TileTypes[,,] map, Vector3 startPos, Planes2D startPlane)
    {
        this.map = map;
        this.startPos = startPos;
        this.startPlane = startPlane;
        this.startSpace = Spaces3D.XZ;
    }
    public TileTypes[,,] map { get; set; }
    public Vector3 startPos { get; set; }
    public Planes2D startPlane { get; set; }
    public Spaces3D startSpace { get; set; }
};

public class Level4D: Level
{
    public Level4D(Vector4 startPos)
    {
        map = new TileTypes[0, 0, 0, 0];
        this.startPos = startPos;
        this.startPlane = Spaces3D.XZ;
    }
    public Level4D(TileTypes[,,,] map, Vector4 startPos, Spaces3D startPlane)
    {
        this.map = map;
        this.startPos = startPos;
        this.startPlane = startPlane;
    }
    public TileTypes[,,,] map { get; set; }
    public Vector4 startPos { get; set; }
    public Spaces3D startPlane { get; set; }
}