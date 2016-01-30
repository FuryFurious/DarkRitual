using UnityEngine;
using System.Collections;

public class DoTile 
{

    public int X { get; private set; }
    public int Y { get; private set; }
    public TileType Type { get; set; }

    public enum TileType { Empty, Obstacle }


    public DoTile(int x, int y)
    {
        X = x;
        Y = y;
    }

    public DoTile(int x, int y, TileType type)
    {
        X = x;
        Y = y;
        this.Type = type;
    }

    public DoTile(DoTile copy)
    {
        this.X = copy.X;
        this.Y = copy.Y;
        this.Type = copy.Type;
    }



}
