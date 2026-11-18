using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireInterval = 0.5f;
    [SerializeField] private TextAsset csv;
    [SerializeField] private List<PlayerStatus> playerStatusList;
    private float fireTimer = 0;
    private int attackPower = 3;
    private int level = 1;
    
    void Awake()
    {
        if(csv == null)
        {
            Debug.LogError("CSV file is not assigned in the inspector.");
            return;
        }
        playerStatusList = PlayerStatusLoader.Load(csv);
        UpdateAttackPower();
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
        UpdateAttackPower();
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
            bulletMove.SendDamage(attackPower);
        }
    }

    private void UpdateAttackPower()
    {
        level = PlayerData.Level;
        if (playerStatusList == null || level < 1 || level > playerStatusList.Count)
        {
            Debug.LogError("Invalid player level: " + level);
            return;
        }        
        attackPower = playerStatusList[level - 1].attack;
    }
}
