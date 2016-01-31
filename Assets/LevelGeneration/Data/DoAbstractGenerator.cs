﻿using UnityEngine;
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

    protected List<GameObject>[,] spawnedGameObjects;


    public void Start()
    {
        Debug.Assert(width != 0 && height != 0);

        CurWorld = new DoWorld(width, height);
        NextWorld = new DoWorld(width, height);

        spawnedGameObjects = new List<GameObject>[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                spawnedGameObjects[i, j] = new List<GameObject>();
            }
        }
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

    protected GameObject MyInstantiateObject(GameObject obj, int i, int j)
    {
        GameObject spawnedObj = (GameObject)GameObject.Instantiate(obj);

        spawnedGameObjects[i, j].Add(spawnedObj);

        spawnedObj.transform.parent = gameObject.transform;
        spawnedObj.transform.position = new Vector3(i, j, 0);

        return spawnedObj;
    }


    protected void DeleteSpawnedGameObjects()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < spawnedGameObjects[i, j].Count; k++)
                {
                    GameObject.Destroy(spawnedGameObjects[i, j][k]);
                }

                spawnedGameObjects[i, j].Clear();

            }
        }        
    }

    protected abstract void MyInit();

    public abstract void DoSteps();


    public void InterpretLevel()
    {
        DeleteSpawnedGameObjects();

        Debug.Assert(CurWorld != null);

        for (int i = 0; i < CurWorld.WorldWidth; i++)
        {
            for (int j = 0; j < CurWorld.WorldHeight; j++)
            {
                Debug.Assert(CurWorld.GetTileAt(i, j) != null);

                DoTile curTile = CurWorld.GetTileAt(i, j);

                //create ground:
                MyInstantiateObject(groundTilesPrefabs[random.Next(0, groundTilesPrefabs.Length)], i, j);

                //create solid walls:
                if (curTile.Type == DoTile.TileType.Obstacle)
                {
                    int id = CurWorld.GetNeighbourInfo(i, j);
                    MyInstantiateObject(tilabeObstaclesPrefabs[id], i, j);
                }


                if (curTile.TopObject == DoTile.ObjectOnTop.Grass)
                {
                   GameObject obj = MyInstantiateObject(grasDecoPrefabs[random.Next(grasDecoPrefabs.Length)], i, j);

                   if (RandFloat() < 0.5f)
                       FlipX(obj);
                }

                else if (curTile.TopObject == DoTile.ObjectOnTop.SingleBlocker)
                {
                    GameObject obj = MyInstantiateObject(singleTileBlockersPrefabs[random.Next(singleTileBlockersPrefabs.Length)], i, j);

                    if (RandFloat() < 0.5f)
                        FlipX(obj);
                   
                }

                else if (curTile.TopObject == DoTile.ObjectOnTop.BigPortal)
                {
                    MyInstantiateObject(pentagramPrefab, i, j);
                }

                else if (curTile.TopObject == DoTile.ObjectOnTop.Pool)
                {
                    GameObject obj = MyInstantiateObject(swampPoolPrefabs, i, j);

                    if (RandFloat() < 0.5f)
                        FlipX(obj);
                }

                else if (curTile.TopObject == DoTile.ObjectOnTop.SmallPortal)
                {
                    //TODO:
                    MyInstantiateObject(smallPortal, i, j);
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
