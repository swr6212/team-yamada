
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("弾の設定")]
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private int damage = 1;

    private Vector3 moveDirection;
    private float bulletSpeed;
    private bool hasHit = false;

    public void SetDirection(Vector3 direction, float speed)
    {
        moveDirection = direction.normalized;
        bulletSpeed = speed;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += moveDirection * bulletSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GiveDamage(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GiveDamage(other.gameObject);
        }
    }

    private void GiveDamage(GameObject target)
    {
        if (hasHit)
        {
            return;
        }
        if(!target.CompareTag("Player"))
        {
            return;
        }
        hasHit = true;
        PlayerHealth health = target.GetComponentInParent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
