
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletSpawner : MonoBehaviour
{
    private enum AttackType
    {
        Aimed,
        Spread,
        StopAndAim,
        RapidAimed
    }

    [Header("Šî–{İ’è")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform player;

    [Header("UŒ‚‚ÌON / OFF")]
    [SerializeField] private bool enableAimed = true;
    [SerializeField] private bool enableSpread = true;
    [SerializeField] private bool enableStopAndAim = true;
    [SerializeField] private bool enableRapidAimed = true;

    [Header("UŒ‚‚Ì’Š‘IƒEƒFƒCƒg")]
    [Min(0f)]
    [SerializeField] private float aimedWeight = 50f;
    [Min(0f)]
    [SerializeField] private float spreadWeight = 30f;
    [Min(0f)]
    [SerializeField] private float stopWeight = 20f;
    [Min(0f)]
    [SerializeField] private float rapidWeight = 20f;

    [Header("‡@ ©‹@‘_‚¢’e")]
    [SerializeField] private int aimedBulletCount = 3;
    [SerializeField] private float aimedSpeed = 7f;
    [SerializeField] private float aimedShotInterval = 0.3f;
    [SerializeField] private float aimedAttackInterval = 5f;

    [Header("‡A îó’e")]
    [SerializeField] private int spreadBulletCount = 9;
    [SerializeField] private float spreadAngle = 90f;
    [SerializeField] private float spreadSpeed = 5f;
    [SerializeField] private float spreadAttackInterval = 5f;

    [Header("‡B ’â~EÄ”­Ë’e")]
    [SerializeField] private int stopBulletCount = 9;
    [SerializeField] private float stopSpreadAngle = 90f;
    [SerializeField] private float stopBulletSpeed = 5f;
    [SerializeField] private float stopAfter = 2f;
    [SerializeField] private float stopDuration = 1f;
    [SerializeField] private float restartSpeed = 7f;
    [SerializeField] private float stopAttackInterval = 5f;

    [Header("‡C ©‹@‘_‚¢˜AË’e")]
    [SerializeField] private int rapidBulletCount = 8;
    [SerializeField] private float rapidSpeed = 6f;
    [SerializeField] private float rapidShotInterval = 0.15f;
    [SerializeField] private float rapidAttackInterval = 5f;

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObject =
                GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        StartCoroutine(ShootLoop());
    }

    private IEnumerator ShootLoop()
    {
        while (true)
        {
            List<AttackType> enabledAttacks = GetEnabledAttacks();

            if (enabledAttacks.Count == 0)
            {
                Debug.LogWarning("—LŒø‚Èƒ{ƒXUŒ‚‚ª‚ ‚è‚Ü‚¹‚ñB");
                yield return new WaitForSeconds(1f);
                continue;
            }

            AttackType selectedAttack = SelectAttack(enabledAttacks);

            yield return StartCoroutine(ExecuteAttack(selectedAttack));

            yield return new WaitForSeconds(
                GetAttackInterval(selectedAttack)
            );
        }
    }

    // g—p‰Â”\‚ÈUŒ‚‚ğæ“¾
    private List<AttackType> GetEnabledAttacks()
    {
        List<AttackType> attacks = new List<AttackType>();

        if (enableAimed && aimedWeight > 0f)
            attacks.Add(AttackType.Aimed);

        if (enableSpread && spreadWeight > 0f)
            attacks.Add(AttackType.Spread);

        if (enableStopAndAim && stopWeight > 0f)
            attacks.Add(AttackType.StopAndAim);

        if (enableRapidAimed && rapidWeight > 0f)
            attacks.Add(AttackType.RapidAimed);

        return attacks;
    }

    // ƒEƒFƒCƒg‚É‰‚¶‚Äƒ‰ƒ“ƒ_ƒ€‘I‘ğ
    private AttackType SelectAttack(List<AttackType> attacks)
    {
        float totalWeight = 0f;

        foreach (AttackType attack in attacks)
        {
            totalWeight += GetWeight(attack);
        }

        float randomValue = Random.Range(0f, totalWeight);

        foreach (AttackType attack in attacks)
        {
            randomValue -= GetWeight(attack);

            if (randomValue <= 0f)
            {
                return attack;
            }
        }

        return attacks[attacks.Count - 1];
    }

    private float GetWeight(AttackType attack)
    {
        switch (attack)
        {
            case AttackType.Aimed:
                return aimedWeight;

            case AttackType.Spread:
                return spreadWeight;

            case AttackType.StopAndAim:
                return stopWeight;

            case AttackType.RapidAimed:
                return rapidWeight;
        }

        return 0f;
    }

    // UŒ‚‚ğÀs
    private IEnumerator ExecuteAttack(AttackType attack)
    {
        switch (attack)
        {
            case AttackType.Aimed:
                yield return StartCoroutine(ShootAimed());
                break;

            case AttackType.Spread:
                ShootSpread(
                    spreadBulletCount,
                    spreadAngle,
                    spreadSpeed
                );
                break;

            case AttackType.StopAndAim:
                yield return StartCoroutine(ShootStopAndAim());
                break;

            case AttackType.RapidAimed:
                yield return StartCoroutine(ShootRapidAimed());
                break;
        }
    }

    // ‡@ ©‹@‘_‚¢’e
    private IEnumerator ShootAimed()
    {
        for (int i = 0; i < aimedBulletCount; i++)
        {
            Vector3 direction = GetDirectionToPlayer(transform.position);

            CreateBullet(direction, aimedSpeed);

            yield return new WaitForSeconds(aimedShotInterval);
        }
    }

    // ‡A îó’e
    private void ShootSpread(int bulletCount, float angle, float speed)
    {
        if (bulletCount <= 0)
            return;

        Vector3 baseDirection = GetDirectionToPlayer(transform.position);

        float startAngle = -angle / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = bulletCount == 1
                ? 0f
                : startAngle + angle * i / (bulletCount - 1);

            Vector3 direction =
                Quaternion.AngleAxis(currentAngle, Vector3.up)
                * baseDirection;

            CreateBullet(direction, speed);
        }
    }

    // ‡B ’â~EÄ”­Ë’e
    private IEnumerator ShootStopAndAim()
    {
        List<BossBullet> spawnedBullets = new List<BossBullet>();

        if (stopBulletCount <= 0)
            yield break;

        Vector3 baseDirection = GetDirectionToPlayer(transform.position);

        float startAngle = -stopSpreadAngle / 2f;

        // îó‚É’e‚ğ”­Ë
        for (int i = 0; i < stopBulletCount; i++)
        {
            float currentAngle = stopBulletCount == 1
                ? 0f
                : startAngle + stopSpreadAngle * i / (stopBulletCount - 1);

            Vector3 direction =
                Quaternion.AngleAxis(currentAngle, Vector3.up)
                * baseDirection;

            BossBullet bullet = CreateBullet(direction, stopBulletSpeed);

            if (bullet != null)
            {
                spawnedBullets.Add(bullet);
            }
        }

        // ”òsŠÔ
        yield return new WaitForSeconds(stopAfter);

        // ‘S’e’â~
        foreach (BossBullet bullet in spawnedBullets)
        {
            if (bullet != null)
            {
                bullet.StopBullet();
            }
        }

        // ’â~ŠÔ
        yield return new WaitForSeconds(stopDuration);

        // Ä”­Ë‚·‚éuŠÔ‚ÌƒvƒŒƒCƒ„[ˆÊ’u‚ğæ“¾
        if (player == null)
            yield break;

        Vector3 targetPosition = player.position;

        // ‘S’e‚ğ‚»‚ê‚¼‚ê‚ÌˆÊ’u‚©‚ç“¯‚¶–Ú•WˆÊ’u‚ÖŒü‚¯‚é
        foreach (BossBullet bullet in spawnedBullets)
        {
            if (bullet == null)
                continue;

            Vector3 direction = targetPosition - bullet.transform.position;

            // XZ•½–Ê‚Å•ûŒü‚ğŒvZ
            direction.y = 0f;

            if (direction.sqrMagnitude < 0.001f)
                direction = transform.forward;

            direction.Normalize();

            bullet.RestartBullet(direction, restartSpeed);
        }
    }

    // ‡C ©‹@‘_‚¢˜AË’e
    private IEnumerator ShootRapidAimed()
    {
        for (int i = 0; i < rapidBulletCount; i++)
        {
            Vector3 direction = GetDirectionToPlayer(transform.position);

            CreateBullet(direction, rapidSpeed);

            if (i < rapidBulletCount - 1)
            {
                yield return new WaitForSeconds(rapidShotInterval);
            }
        }
    }

    // w’è‚µ‚½ˆÊ’u‚©‚çƒvƒŒƒCƒ„[‚Ö‚Ì•ûŒü‚ğæ“¾
    private Vector3 GetDirectionToPlayer(Vector3 origin)
    {
        if (player == null)
        {
            return transform.forward;
        }

        Vector3 direction = player.position - origin;

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
        {
            return transform.forward;
        }

        return direction.normalized;
    }

    // ’e‚ğ¶¬‚µ‚Ä”­Ë
    private BossBullet CreateBullet(Vector3 direction, float speed)
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñB");
            return null;
        }

        GameObject bulletObject = Instantiate(
            bulletPrefab,
            transform.position,
            Quaternion.identity
        );

        BossBullet bullet = bulletObject.GetComponent<BossBullet>();

        if (bullet == null)
        {
            Debug.LogError("Bullet Prefab‚ÉBossBullet‚ª•t‚¢‚Ä‚¢‚Ü‚¹‚ñB");
            Destroy(bulletObject);
            return null;
        }

        bullet.SetDirection(direction, speed);

        return bullet;
    }

    // UŒ‚‚²‚Æ‚Ì‘Ò‹@ŠÔ
    private float GetAttackInterval(AttackType attack)
    {
        switch (attack)
        {
            case AttackType.Aimed:
                return aimedAttackInterval;

            case AttackType.Spread:
                return spreadAttackInterval;

            case AttackType.StopAndAim:
                return stopAttackInterval;

            case AttackType.RapidAimed:
                return rapidAttackInterval;
        }

        return 5f;
    }
}
