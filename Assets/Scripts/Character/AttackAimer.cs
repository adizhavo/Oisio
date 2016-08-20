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
        float cursorDeltaX = InputConfig.GetCursorMovement().x * AimingDirection() * Sensibility;
        transform.rotation = Quaternion.Euler(0f, 0f, transform.localEulerAngles.z + cursorDeltaX);
    }

    public void ResetAim()
    {
        if (arrowInstance) arrowInstance.gameObject.SetActive(false);
        transform.localEulerAngles = Vector3.zero;
    }

    private int AimingDirection()
    {
        return InvertAiming ? 1 : -1;
    }

    public void ThrowArrow()
    {
        if (!arrowInstance) return;

        Vector3 shootDirection = arrowParent.position - transform.position;
        Vector3 deviation = enemy.NearbyEnemyDir(transform.position);
        arrowInstance.Shoot(shootDirection * ShootForce, deviation);

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
}