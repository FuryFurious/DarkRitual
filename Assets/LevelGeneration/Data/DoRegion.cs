using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoRegion {

    private List<DoTile> tiles;
    public int RegionId { get; private set; }


    public DoRegion()
    {
        tiles = new List<DoTile>();
        RegionId = 0;
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

}
