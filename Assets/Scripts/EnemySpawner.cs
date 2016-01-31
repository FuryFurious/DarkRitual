using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour 
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


    void Start()
    {
        
    }
	
	// Update is called once per frame
	void Update () 
    {
        curSpawnTime -= Time.deltaTime;

        if (curSpawnTime < 0.0f)
        {
            SetSpawnTime();

            if(Random.value < spawnChance)
                SpawnEnemy();
        }
	}

    private void SpawnEnemy()
    {
        manager.MyInstantiateObject(enemies[Random.Range(0, enemies.Length)], (int)gameObject.transform.position.x, (int)gameObject.transform.position.y, false);
    }

    void SetSpawnTime()
    {
        curSpawnTime = Random.value * (maxSpawnTime - minSpawnTime) + minSpawnTime;
    }
}
