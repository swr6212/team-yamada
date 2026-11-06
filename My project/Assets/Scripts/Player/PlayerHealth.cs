using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHp = 10;
    private int nowHp;
    [SerializeField] private GameManager gameManager;

    void Awake()
    {
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
