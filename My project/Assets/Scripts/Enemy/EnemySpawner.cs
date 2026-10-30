using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField]private List<Transform> slots;
    [SerializeField] private float spawnInterval;
    private float spawnTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnInterval)
        {
            SpawnRow();
            spawnTimer = 0;
        }
    }

    void SpawnRow()
    {
        int enemyCount = Random.Range(1, 6);
        List<Transform> forWork = new List<Transform>(slots);
        for(int i = 0; i < enemyCount; i++)
        {
            int enemyNum = Random.Range(0,forWork.Count);
            Vector3 enemyPos = forWork[enemyNum].position;
            int roll = Random.Range(0, 100);
            MoveType enemyType;
            if(roll < 80)
            {
                enemyType = MoveType.Normal;
            }
            else if(roll < 90)
            {
                enemyType = MoveType.Accelerate;
            }
            else
            {
                enemyType = MoveType.Shoot;
            }
            GameObject newEnemy = Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
            Enemy enemy = newEnemy.GetComponent<Enemy>();
            enemy.Setup(enemyType);
            forWork.RemoveAt(enemyNum);
        }

    }
}
