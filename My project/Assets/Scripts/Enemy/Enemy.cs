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
    private int nowhp;
    private bool hasTriggered = false;//”­“®‚µ‚½‚©
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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
            return;
        }
    }

    public void TakeDamage(int damage)
    {
        nowhp -= damage;
        if(nowhp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    public void Setup(MoveType type)
    {
        moveType = type;

    }

}
