using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class EnemySpawner : NetworkBehaviour 
{

    public float minSpawnTime;
    public float maxSpawnTime;

    [Range(0.0f, 1.0f)]
    public float spawnChance;

    private float curSpawnTime;

    [HideInInspector]
    public WorldManager manager;

    [HideInInspector]
    public GameObject[] enemies;

    private int numActiveEnemies;

    private List<GameObject> _spawnedEnemies = new List<GameObject>();


    void Start()
    {
        
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (isServer)
        {
            if (numActiveEnemies >= 5)
                return;

            curSpawnTime -= Time.deltaTime;

            if (curSpawnTime < 0.0f)
            {
                for (int i = _spawnedEnemies.Count - 1; i >= 0; i--)
                {
                    if (_spawnedEnemies[i] == null)
                    {
                        _spawnedEnemies.RemoveAt(i);
                        numActiveEnemies--;
                    }
                }

                SetSpawnTime();

                if (Random.value < spawnChance)
                {

                    SpawnEnemy();
                }
            }
        }
	}

    private void SpawnEnemy()
    {
        GameObject obj = manager.MyInstantiateObject(enemies[Random.Range(0, enemies.Length)], (int)gameObject.transform.position.x, (int)gameObject.transform.position.y, false);
        numActiveEnemies++;
    }

    void SetSpawnTime()
    {
        curSpawnTime = Random.value * (maxSpawnTime - minSpawnTime) + minSpawnTime;
    }
}
