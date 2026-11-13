using UnityEngine;

public enum MoveType
{
    Normal,
    Stop,
    Accelerate,
    Shoot
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private float changeZ;
    [SerializeField] private float acceleration;
    [SerializeField] private MoveType moveType;
    [SerializeField] private EnemyShooter enemyShooter;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private int damage;
    [SerializeField] private float homingZ;
    private int nowhp;
    private bool hasTriggered = false;//”­“®‚µ‚½‚©
    private float damageZ;
    private Transform playerTransform;

    void Awake()
    {
        nowhp = hp;
        if(enemyShooter != null )
        {
            enemyShooter.enabled = false;
        }
    }

    void Update()
    {
        Move();
        if(transform.position.z <= damageZ)
        {
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
            return;
        }
        if (!hasTriggered)
        {
            if (transform.position.z <= changeZ)
            {
                switch (moveType)
                {
                    case MoveType.Normal:
                        break;
                    case MoveType.Stop:
                        moveSpeed = 0;
                        break;
                    case MoveType.Accelerate:
                        moveSpeed *= acceleration;
                        break;
                    case MoveType.Shoot:
                        moveSpeed = 0;
                        if(enemyShooter != null)
                        {
                            enemyShooter.enabled = true;
                        }
                        break;
                }
                hasTriggered = true;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if(nowhp <= 0)
        {
            return;
        }
        nowhp -= damage;
        if(nowhp <= 0)
        {
            Destroy(gameObject);
            GameManager.AddDefeatedEnemy();
            GameManager.AddScore(10);
        }
    }

    private void Move()
    {
        if(playerTransform != null && homingZ >= transform.position.z)
        {
            Vector3 direction = (playerTransform.position - transform.position);
            direction.y = 0;
            direction.z = -1;
            direction.x = Mathf.Clamp(direction.x, -1, 1);
            direction = direction.normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    public void Setup(MoveType type, Transform playerTransform, float damageZ)
    {
        moveType = type;
        this.playerTransform = playerTransform;
        this.damageZ = damageZ;
        if(playerTransform != null)
        {
            playerHealth = playerTransform.GetComponent<PlayerHealth>();
        }
    }

}
