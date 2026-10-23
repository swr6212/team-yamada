using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float wallMaxValue;
    [SerializeField]private float wallMinValue;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireInterval = 0.5f;
    private float fireTimer = 0;
    private Vector2 inputDirections;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //  ì¸óÕÇ™Ç»Ç≠Ç»Ç¡ÇΩÇÁÅAà⁄ìÆÇµÇ»Ç¢
        if (context.canceled)
        {
            inputDirections = Vector2.zero;
            return;
        }

        inputDirections = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    { 
        float displacement = inputDirections.x * moveSpeed * Time.fixedDeltaTime;
        float newX = rb.position.x + displacement;
        float restrictions = Mathf.Clamp(newX,wallMinValue, wallMaxValue);
        Vector3 movedPos = new Vector3(restrictions,rb.position.y,rb.position.z);
        rb.MovePosition(movedPos);
        

    }

    private void Shoot()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Plane plane = new Plane(Vector3.up, firePoint.position);
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 intersection = ray.GetPoint(distance);
            Vector3 newdirection = intersection - firePoint.position;
            newdirection.y = 0;
            if (newdirection.z <= 0)
            {
                return;
            }
            GameObject newbullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
            MoveBullet bulletMove = newbullet.GetComponent<MoveBullet>();
            bulletMove.MoveDirection(newdirection);
        }
    }
    
    void Update()
    {
        fireTimer += Time.deltaTime;
        if(fireTimer >= fireInterval)
        {
            Shoot();
            fireTimer = 0;
        }
    }
}
