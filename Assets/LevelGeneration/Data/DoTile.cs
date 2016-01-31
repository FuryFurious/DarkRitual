using UnityEngine;
using System.Collections;

public class DoTile 
{

    public int X { get; private set; }
    public int Y { get; private set; }
    public TileType Type { get; set; }
    public ObjectOnTop TopObject { get; set; }

    public enum TileType { Empty, Obstacle }

    public enum ObjectOnTop { None, Grass, Pool, SingleBlocker, BigPortal, SmallPortal }

    public DoTile(int x, int y)
    {
        X = x;
        Y = y;
    }

    public DoTile(DoTile copy)
    {
        this.X = copy.X;
        this.Y = copy.Y;
        this.Type = copy.Type;
        this.TopObject = copy.TopObject;
    }


}
