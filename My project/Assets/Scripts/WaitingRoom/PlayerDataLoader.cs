using UnityEngine;
using TMPro;

public class PlayerDataLoader : MonoBehaviour
{
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text levelText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money:" + PlayerData.Money;
        levelText.text = "Level:" + PlayerData.Level;
    }
}
