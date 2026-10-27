
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("弾の設定")]
    [SerializeField] private float lifeTime = 5f;

    private Vector3 moveDirection;
    private float bulletSpeed;

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
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
