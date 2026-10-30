using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHp = 10;
    [SerializeField]private GameObject playerObject;
    private int nowHp;
    
    void Awake()
    {
        nowHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("ダメージ " + damage + " 残り " + nowHp);
        if (nowHp <= 0)
        {
            return; 
        }
        nowHp -= damage;
        if (nowHp <= 0)
        {
            Debug.Log("ゲームオーバー");
        }
    }
}
