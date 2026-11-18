using UnityEngine;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHp = 10;
    [SerializeField] private TextAsset csv;
    private int nowHp;
    [SerializeField] private GameManager gameManager;
    List<PlayerStatus> playerStatusList;

    void Awake()
    {
        if(csv == null)
        {
            Debug.LogError("CSV file is not assigned in the inspector.");
            nowHp = maxHp;
            return;
        }
        playerStatusList = PlayerStatusLoader.Load(csv); 
        PlayerData.SetStatusList(playerStatusList);
        int playerLevel = PlayerData.Level;
        if(playerLevel < 1 || playerLevel > playerStatusList.Count)
        {
            Debug.LogError("Invalid player level: " + playerLevel);
            nowHp = maxHp;
            return;
        }
        maxHp = playerStatusList[playerLevel - 1].hp;
        nowHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        if (nowHp <= 0)
        {
            return;
        }
        nowHp -= damage;   
        if (nowHp <= 0)
        {
            gameManager.ShowResult();
            Debug.Log("ゲームオーバー");
        }
    }
}
