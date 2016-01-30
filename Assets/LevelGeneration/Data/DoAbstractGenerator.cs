using UnityEngine;
using System.Collections;

public abstract class DoAbstractWorldGenerator
    : MonoBehaviour
{
    public int desiredSeed = -1;
    public int width;
    public int height;

    public int curSeed;

    protected System.Random random;

    public DoWorld CurWorld { get; protected set;}
    public DoWorld NextWorld { get; protected set; }


    public DoAbstractWorldGenerator()
    {
    }

    public void Start()
    {
        Debug.Assert(width != 0 && height != 0);

        CurWorld = new DoWorld(width, height);
        NextWorld = new DoWorld(width, height);
    }



    protected float RandFloat()
    {
        return (float)random.NextDouble();
    }

    protected void CreateNoise(float chance)
    {

        for (int i = 0; i < CurWorld.WorldWidth; i++)
        {
            for (int j = 0; j < CurWorld.WorldHeight; j++)
            {
                if (RandFloat() < chance)
                    CurWorld.GetTileAt(i, j).Type = DoTile.TileType.Obstacle;
                else
                    CurWorld.GetTileAt(i, j).Type = DoTile.TileType.Empty;
            }
        }
    }


    public void Init()
    {
        if (desiredSeed == -1)
            this.curSeed = System.Environment.TickCount;
        else
            this.curSeed = desiredSeed;

        random = new System.Random(curSeed);

        MyInit();
    }

    protected abstract void MyInit();

    public abstract void DoStep();

    public abstract void DoSteps(int numSteps);
}
