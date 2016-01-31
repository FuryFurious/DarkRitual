using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DoRegion : IComparable<DoRegion>
{

    private List<DoTile> tiles;
    public int RegionId { get; private set; }


    public DoRegion()
    {
        tiles = new List<DoTile>();
        RegionId = -1;
    }

    public DoRegion(int regionId)
    {
        tiles = new List<DoTile>();
        RegionId = regionId;
    }



    public void AddTile(DoTile tile)
    {
        tiles.Add(tile);
    }

    public List<DoTile> GetTiles()
    {
        return tiles;
    }

    public int RegionSize()
    {
        return tiles.Count;
    }

    public int CompareTo(DoRegion other)
    {
        return other.RegionSize() - this.RegionSize();
    }

    public DoTile GetTile(int id)
    {
        return tiles[id];
    }

    public void RemoveAt(int id)
    {
        tiles.RemoveAt(id);
    }

    public void Remove(DoTile doTile)
    {
        tiles.Remove(doTile);
    }
}
