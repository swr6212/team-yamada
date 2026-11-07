
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BossBullet : MonoBehaviour
{
    [Header("’e‚Ìİ’è")]
    public float destroyTime = 10f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // ’e‚ğ”­Ë‚·‚é
    public void SetDirection(Vector3 direction, float speed)
    {
        if (direction.sqrMagnitude < 0.0001f)
            return;

        rb.linearVelocity = direction.normalized * speed;
    }

    // ’e‚ğŠ®‘S’â~‚³‚¹‚é
    public void StopBullet()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    // ’e‚ğ‹}”­i‚³‚¹‚é
    public void RestartBullet(Vector3 direction, float speed)
    {
        if (direction.sqrMagnitude < 0.0001f)
            return;

        rb.linearVelocity = Vector3.zero;
        rb.linearVelocity = direction.normalized * speed;
    }
}
