using UnityEngine;
using System.Collections;

public struct Vec2i {

    public int x;
    public int y;

    public Vec2i(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Vec2i(int z)
    {
        this.x = z;
        this.y = z;
    }
	
}
