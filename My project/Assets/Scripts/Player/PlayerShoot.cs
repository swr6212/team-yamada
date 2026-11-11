using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireInterval = 0.5f;
    private float fireTimer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            Shoot();
            fireTimer = 0;
        }
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
}
