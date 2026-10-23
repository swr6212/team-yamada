using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private float moveSpeed = 2;
    private int nowhp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        nowhp = hp;
    }

    void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        nowhp -= damage;
        if(nowhp <= 0)
        {
            Destroy(gameObject);
        }
    }

}
