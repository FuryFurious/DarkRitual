using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public GameObject[] tilabeObstaclesPrefabs;
    public GameObject[] groundTilesPrefabs;
    public GameObject[] grasDecoPrefabs;
    public GameObject swampPoolPrefabs;
    public GameObject[] singleTileBlockersPrefabs;
    public GameObject pentagramPrefab;
    public GameObject smallPortal;
    public GameObject[] coloredPortals;
    public GameObject playerPrefab;
    public GameObject[] enemyPrefabs;


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

    protected void CreateBorder()
    {
        Debug.Assert(CurWorld != null);

        for (int i = 0; i < width; i++)
            CurWorld.GetTileAt(0, i).Type = DoTile.TileType.Obstacle;

        for (int i = 0; i < width; i++)
            CurWorld.GetTileAt(width - 1, i).Type = DoTile.TileType.Obstacle;

        for (int i = 0; i < height; i++)
            CurWorld.GetTileAt(i, height - 1).Type = DoTile.TileType.Obstacle;

        for (int i = 0; i < height; i++)
            CurWorld.GetTileAt(i, 0).Type = DoTile.TileType.Obstacle;   
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

    public abstract void DoSteps();


    public void InterpretLevel(WorldManager manager)
    {
        Debug.Assert(CurWorld != null);

        int portalCount = 0;

        for (int i = 0; i < CurWorld.WorldWidth; i++)
        {
            for (int j = 0; j < CurWorld.WorldHeight; j++)
            {
                Debug.Assert(CurWorld.GetTileAt(i, j) != null);

                DoTile curTile = CurWorld.GetTileAt(i, j);

                //create ground:
                manager.MyInstantiateObject(groundTilesPrefabs[random.Next(0, groundTilesPrefabs.Length)], i, j, false);

                //create solid walls:
                if (curTile.Type == DoTile.TileType.Obstacle)
                {
                    int id = CurWorld.GetNeighbourInfo(i, j);
                    manager.MyInstantiateObject(tilabeObstaclesPrefabs[id], i, j, false);
                }

                else if (curTile.SpawnEnemyHere)
                {
                    manager.MyInstantiateObject(enemyPrefabs[random.Next(enemyPrefabs.Length)], i, j, false);
                }


                if (curTile.TopObject == DoTile.ObjectOnTop.Grass)
                {
                    GameObject obj = manager.MyInstantiateObject(grasDecoPrefabs[random.Next(grasDecoPrefabs.Length)], i, j, false);

                   if (RandFloat() < 0.5f)
                       FlipX(obj);
                }

                else if (curTile.TopObject == DoTile.ObjectOnTop.SingleBlocker)
                {
                    GameObject obj = manager.MyInstantiateObject(singleTileBlockersPrefabs[random.Next(singleTileBlockersPrefabs.Length)], i, j, false);

                    if (RandFloat() < 0.5f)
                        FlipX(obj);
                   
                }

                else if (curTile.TopObject == DoTile.ObjectOnTop.BigPortal)
                {
                    manager.MyInstantiateObject(pentagramPrefab, i, j, false);
                }

                else if (curTile.TopObject == DoTile.ObjectOnTop.Pool)
                {
                    GameObject obj = manager.MyInstantiateObject(swampPoolPrefabs, i, j, false);

                    if (RandFloat() < 0.5f)
                        FlipX(obj);
                }

                else if (curTile.TopObject == DoTile.ObjectOnTop.SmallPortal)
                {

                    if (portalCount < PortalStoneTarget.NUM_COLORS)
                    {
                        GameObject obj = manager.MyInstantiateObject(smallPortal, i, j, false);

                        // obj.ad
                        GameObject sprite = manager.MyInstantiateObject(coloredPortals[portalCount], i, j, false);

                        sprite.transform.parent = obj.transform;
                        sprite.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

                        EnemySpawner spawner = obj.GetComponent<EnemySpawner>();
                        spawner.manager = manager;
                        spawner.enemies = enemyPrefabs;


                        obj.GetComponent<Health>().renderer = sprite.GetComponent<SpriteRenderer>();

                        portalCount++;
                    }
                }

                else if (curTile.TopObject == DoTile.ObjectOnTop.Player)
                {
                    manager.MyInstantiateObject(playerPrefab, i, j, true);
                }

 
            }
        }



    }

    void FlipX(GameObject obj)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            spriteRenderer.flipX = true;

        else
        {
            obj.transform.localScale = new Vector3(-obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z);
        }
    }



}
