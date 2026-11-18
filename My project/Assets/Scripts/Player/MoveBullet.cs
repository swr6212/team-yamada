using UnityEngine;

public class MoveBullet : MonoBehaviour
{ 
    [SerializeField]private float moveSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float deleteTime = 6.0f;
    [SerializeField] private int damage;
    [SerializeField] private GameObject hitEffect;
    void Start()
    {
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, deleteTime);
    }

    public void MoveDirection(Vector3 direction)
    {
        rb.linearVelocity = direction.normalized * moveSpeed;
    }

    public void SendDamage(int damageAmount)
    {
        damage = damageAmount;
    }

    public void OnTriggerEnter(Collider other)
    { 
        Enemy enemy = other.GetComponentInParent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            //Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
