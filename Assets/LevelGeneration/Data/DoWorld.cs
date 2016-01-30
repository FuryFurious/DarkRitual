using UnityEngine;
using System.Collections;
using System.Collections.Generic;


using System;
public class DoWorld 
{
    [Flags]
    private enum NeighbourInfo
    {
        None = 0, 
        Top = 1, 
        Left = 2, 
        Down = 4, 
        Right = 8,
    }

    private DoTile[,] worldTiles;
    private List<DoRegion> regions;

    public int WorldWidth { get; private set; }
    public int WorldHeight { get; private set; }

    public DoTile GetTileAt(int x, int y)
    {
        return worldTiles[x, y];
    }

    public void SetTileAt(int x, int y, DoTile tile)
    {
       worldTiles[x, y] = tile;
    }

    public DoWorld(int width, int height)
    {
        WorldWidth = width;
        WorldHeight = height;

        regions = new List<DoRegion>();
        worldTiles = new DoTile[WorldWidth, WorldHeight];

        for (int i = 0; i < WorldWidth; i++)
        {
            for (int j = 0; j < WorldHeight; j++)
            {
                worldTiles[i, j] = new DoTile(i, j, DoTile.TileType.Empty);
            }
        }
    }

    public void SetTiles(DoTile[,] tiles)
    {
        Debug.Assert(tiles.GetLength(0) == WorldWidth && tiles.GetLength(1) == WorldHeight);
        worldTiles = tiles;
    }

    public int CountNeighbours(int x, int y, DoTile.TileType type)
    {
        int count = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int idX = x + i;
                int idY = y + j;

                if (i == 0 && j == 0)
                    continue;

                else if (idX < 0 || idY < 0 || idX >= WorldWidth || idY >= WorldHeight)
                    count++;

                else if (GetTileAt(idX, idY).Type == type)
                    count++;
            }
        }

        return count;
    }


    /// <summary>Looks where the given Tile as neighbours of a given Type.</summary>
    public int GetNeighbourInfo(int x, int y)
    {
        Debug.Assert(GetTileAt(x, y).Type == DoTile.TileType.Obstacle);
        NeighbourInfo info = NeighbourInfo.None;
        //top
        if (y == WorldHeight - 1 || GetTileAt(x, y + 1).Type == DoTile.TileType.Obstacle)
        {
            info |= NeighbourInfo.Top;
        }

        //left
        if(x == 0 || GetTileAt(x - 1, y).Type == DoTile.TileType.Obstacle)
        {
            info |= NeighbourInfo.Left;
        }

        //down
        if(y == 0 || GetTileAt(x, y - 1).Type == DoTile.TileType.Obstacle)
        {
            info |= NeighbourInfo.Down;
        }

        //right
        if (x == WorldWidth - 1 || GetTileAt(x + 1, y).Type == DoTile.TileType.Obstacle)
        {
            info |= NeighbourInfo.Right;
        }

        return (int)info;
    }



}
