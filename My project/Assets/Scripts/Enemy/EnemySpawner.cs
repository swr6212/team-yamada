using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]private List<Transform> slots;
    [SerializeField] private List<WaveData> waveDatas;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private float damageZ;
    [SerializeField] private Transform playerTransform;
    private int currentWave = 0;
    private float waveTimer;
    private float spawnTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowWaveText(); 
    }

    // Update is called once per frame
    void Update()
    {
        waveTimer += Time.deltaTime;
        if (currentWave >= waveDatas.Count)
        {
            return;
        }
        WaveData waveData = waveDatas[currentWave];
        if (waveTimer >= waveData.waveDuration)
        {
            currentWave++;
            if(currentWave >= waveDatas.Count)
            {
                gameManager.ShowResult();
                return;
            }
            ShowWaveText();
            waveTimer = 0;
        }
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= waveData.spawnInterval)
        {
            SpawnRow(waveData);
            spawnTimer = 0;
        }
    }

    void SpawnRow(WaveData wave)
    {
        int enemyCount = Random.Range(wave.enemyMinRate, wave.enemyMaxRate + 1);
        List<Transform> forWork = new List<Transform>(slots);
        for(int i = 0; i < enemyCount; i++)
        {
            int enemyNum = Random.Range(0,forWork.Count);
            Vector3 enemyPos = forWork[enemyNum].position;
            EnemyData enemyData = PickEnemy(wave.enemyDatas);
            GameObject newEnemy = Instantiate(enemyData.enemyPrefab, enemyPos, Quaternion.identity);
            Enemy enemy = newEnemy.GetComponent<Enemy>();
            enemy.Setup(enemyData.moveType, playerTransform, damageZ);
            forWork.RemoveAt(enemyNum);
        }

    }

    EnemyData PickEnemy(List<EnemyData> enemyDatas)
    {
        float totalWeight = 0;
        foreach (EnemyData enemyData in enemyDatas)
        {
            totalWeight += enemyData.appearanceWeight;
        }
        float randomValue = Random.Range(0f, totalWeight);
        foreach (EnemyData enemyData in enemyDatas)
        {
            randomValue -= enemyData.appearanceWeight;
            if(randomValue <= 0)
            {
                return enemyData;
            }
        }
        return enemyDatas[0];
    }

    void ShowWaveText()
    {
        waveText.text = "WAVE " + (currentWave + 1);
        waveText.gameObject.SetActive(true);
    }
}
