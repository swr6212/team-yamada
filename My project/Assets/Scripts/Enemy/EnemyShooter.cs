
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("弾の設定")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("発射設定")]
    [SerializeField] private float fireInterval = 2f;
    [SerializeField] private float bulletSpeed = 10f;

    private float timer;

    private void Update()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= fireInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    private void Shoot()
    {
        // FirePointの正面方向を取得する
        Vector3 direction = firePoint.forward;

        // 弾を生成する
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.LookRotation(direction)
        );

        // 弾にまっすぐ進む方向を設定する
        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction, bulletSpeed);
        }
    }
}
