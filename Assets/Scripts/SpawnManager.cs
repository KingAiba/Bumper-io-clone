using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float spawnRangeX = 18;
    public float spawnRangeZ = 10;

    public GameObject enemyPrefabs;
    // Start is called before the first frame update

    void Start()
    {
        SpawnEnemies(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GenerateSpawnPoint()
    {
        return new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
    }

    public void SpawnEnemies(int amount)
    {
        for(int i=0; i<amount;i++)
        {
            Instantiate(enemyPrefabs, GenerateSpawnPoint(), enemyPrefabs.transform.rotation);
        }
    }
}
