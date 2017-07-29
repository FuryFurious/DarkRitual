using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class WorldManager : NetworkBehaviour 
{
    public static WorldManager Instance;

    public DoAbstractWorldGenerator worldGenerator;
    private bool newLevel = true;


    public GameObject staticSceneRoot;
    public GameObject dynamicSceneRoot;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Debug.Assert(staticSceneRoot);
        Debug.Assert(dynamicSceneRoot);
    }

	// Update is called once per frame
	void Update () 
    {
        if (isServer)
        {

            if (newLevel)
            {
                worldGenerator.Init();
                worldGenerator.InterpretLevel(this);
                newLevel = false;
            }


            else
            {
                //generate new map:
                if (Input.GetKeyDown(KeyCode.P))
                {
                    if (worldGenerator != null)
                    {
                        SceneManager.LoadScene("defaultScene");

                    }

                }

                else if (Input.GetKeyDown(KeyCode.Escape))
                    Application.Quit();
            }

        }

	}

    public GameObject MyInstantiateObject(GameObject obj, int i, int j, bool addDirectToScene)
    {
        GameObject spawnedObj = (GameObject)GameObject.Instantiate(obj);


        if (!addDirectToScene)
        {
            if (spawnedObj.isStatic)
                spawnedObj.transform.parent = staticSceneRoot.transform;
            else
                spawnedObj.transform.parent = dynamicSceneRoot.transform;
        }
    

        spawnedObj.transform.position = new Vector3(i, j, 0);
        NetworkServer.Spawn(spawnedObj);

        return spawnedObj;
    }










}
