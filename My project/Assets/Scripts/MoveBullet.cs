using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    [SerializeField]private Vector3 orienTation = new Vector3(1,0,1).normalized;
    [SerializeField]private float moveSpeed;
    [SerializeField] private Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.linearVelocity = orienTation * moveSpeed;
    }
}
