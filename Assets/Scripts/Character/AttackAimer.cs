using UnityEngine;

public class AttackAimer : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowParent;
    public bool InvertAiming;
    public float Sensibility = 1f;
    public float ShootForce;

    private Projectile arrowInstance;
    private EnemyDirectionAim enemy;

    private float cursorDeltaX;

    private void Start()
    {
        enemy = new EnemyDirectionAim();
    }

    public void Aim()
    {
        CheckArrow();
        RotateAimer();
    }

    private void CheckArrow()
    {
        if (!arrowInstance)
        {
            arrowInstance = (Instantiate(arrowPrefab) as GameObject).GetComponent<Projectile>();
            arrowInstance.transform.SetParent(arrowParent, false);
            arrowInstance.transform.localPosition = Vector3.zero;
        }

        arrowInstance.gameObject.SetActive(true);
    }

    private void RotateAimer()
    {
        cursorDeltaX += InputConfig.GetCursorMovement().y * AimingDirection() * Sensibility;
        Vector3 targetPos = enemy.NearbyEnemyDir(transform.position);
        transform.rotation = Quaternion.LookRotation(targetPos);
        transform.rotation = Quaternion.Euler(transform.localEulerAngles.x + cursorDeltaX, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    public void ResetAim()
    {
        if (arrowInstance) arrowInstance.gameObject.SetActive(false);
        transform.localEulerAngles = Vector3.zero;
        cursorDeltaX = 0f;
    }

    private int AimingDirection()
    {
        return InvertAiming ? 1 : -1;
    }

    public void ThrowArrow()
    {
        if (!arrowInstance) return;

        EventTrigger attackEvent = new CustomEvent(transform.position, EventSubject.Attack, GameConfig.projectilePriority);
        Vector3 shootDirection = arrowParent.position - transform.position;
        arrowInstance.Shoot(attackEvent, shootDirection * ShootForce);

        arrowInstance.transform.SetParent(null);
        arrowInstance = null;
    }
}

public class EnemyDirectionAim
{
    private GameObject[] enemies;
    private string enemyTag = "Enemy";

    public EnemyDirectionAim()
    {
        enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    }

    public Vector3 NearbyEnemyDir(Vector3 currentPos)
    {
        Vector3? closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach(GameObject go in enemies)
        {
            float distance = Vector3.Distance(currentPos, go.transform.position);
            if (!closestTarget.HasValue || distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = go.transform.position;
            }
        }

        Vector3 direction = (closestTarget.Value - currentPos).normalized;
        direction.y = 0;

        return direction;
    }

    public Vector3 NearbyEnemyPos(Vector3 currentPos)
    {
        Vector3? closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach(GameObject go in enemies)
        {
            float distance = Vector3.Distance(currentPos, go.transform.position);
            if (!closestTarget.HasValue || distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = go.transform.position;
            }
        }

        return closestTarget.Value;
    }
}