using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private int damage = 1;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("“ü‚Á‚½: " + other.name);
        Enemy enemy = other.GetComponentInParent<Enemy>();
        if(enemy == null)
        {
            return;
        }
        playerHealth.TakeDamage(damage);
        Destroy(enemy.gameObject);
    }
}
