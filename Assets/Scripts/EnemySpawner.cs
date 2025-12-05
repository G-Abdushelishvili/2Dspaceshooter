using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemy;



    void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, 2);
        float randomX = Random.Range(-8, 8);
        GameObject enemyClone = Instantiate(enemy[randomEnemy], new Vector2(randomX, transform.position.y), transform.rotation);
        Destroy(enemyClone, 6);
    }
    
    

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, 2);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
