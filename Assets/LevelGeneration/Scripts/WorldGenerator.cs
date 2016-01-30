using UnityEngine;
using System.Collections;

public class WorldGenerator : MonoBehaviour 
{

    public DoAbstractWorldGenerator worldGenerator;

    public GameObject freeTilePrefab;
    public GameObject blockTilePrefab;

    private GameObject[,] spawnedGameObjects;
    private int numFreeTiles;

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
        //generate new map:
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (worldGenerator != null)
            {
                worldGenerator.Init();
                CreateLevelFromMap();
            }
        
        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (worldGenerator != null)
            {
                worldGenerator.DoStep();
                CreateLevelFromMap();
            }
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (worldGenerator != null)
            {
                worldGenerator.DoStep();
                CreateLevelFromMap();
            }
        }
	}

    public GameObject[] tilabeObstacles;
    public GameObject[] groundTiles;
    public GameObject[] grasDeco;


    void CreateLevelFromMap()
    {
        DeleteChildren();

        DoWorld world = worldGenerator.CurWorld;

        Debug.Assert(world != null);

        spawnedGameObjects = new GameObject[world.WorldWidth, world.WorldHeight];

        for (int i = 0; i < world.WorldWidth; i++)
        {
            for (int j = 0; j < world.WorldHeight; j++)
            {

                GameObject newTile = null;

                Debug.Assert(world.GetTileAt(i, j) != null);

                
                if (world.GetTileAt(i, j).Type == DoTile.TileType.Empty){
                    newTile = (GameObject)GameObject.Instantiate(groundTiles[Random.Range(0, groundTiles.Length)]);
                }
                 
                else
                {
                    int id = world.GetNeighbourInfo(i, j);
                    newTile = (GameObject)GameObject.Instantiate(tilabeObstacles[id]);
                }
                   
                
                spawnedGameObjects[i, j] = newTile;

                newTile.transform.parent = gameObject.transform;
                newTile.transform.position = new Vector3(i, j, 0);

            }
        }
    }

   
    private void DeleteChildren()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject.Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }




}
