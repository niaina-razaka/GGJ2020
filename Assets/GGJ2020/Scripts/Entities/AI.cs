using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AI : MonoBehaviour
{

    // AI type
    public enum Type { GroundPatrol, JumpingPatrol, FlyingPatrol }
    [Header("AI Settings")]
    public Type type;

    // Player layermask
    public LayerMask playerLayer;

    // Spawning point
    protected Vector2 spawningPoint;

    // Maximum patrolling distance from spawning point
    public Vector2 maxPatrolDistance = new Vector2(5, 10);

    // If object has just reached max distance
    protected bool maxDistanceReached = false;

    // Player detection range margin
    protected float margin = .5f;

    // Target (player) if locked
    [HideInInspector]
    public Transform target;

    // Projectile spawn point
    public Transform projectileSpawnPoint;

    // Projectile spawn point
    public Transform mayhemProjectileSpawnPoint;

    // Projectile prefab
    public GameObject[] projectilePrefabs;

    // Probability of each projectile
    public float[] projectileProbabilities;

    // Current projectile
    GameObject currentProjectile;

    // Indicates if object is edible or not
    public bool edible;

    public GameObject destroyEffect;

    [Header("Combat Settings")]
    public int health = 1;
    public int damage = 1;
    //[HideInInspector]
    public int currentHealth;

    protected bool canMove = true;

    float[,] probabilities;

    protected void Start()
    {
        currentHealth = health;
        currentProjectile = projectilePrefabs[0];
        float totalProbability = 0;
        for(int i = 0; i < projectilePrefabs.Length; i++)
        {
            totalProbability += projectileProbabilities[i];
        }
        if(totalProbability != 100)
        {
            throw new System.Exception("Probability is not 100%");
        }

        probabilities = new float[projectileProbabilities.Length, 2];
        float start = 0;
        for(int j = 0; j < projectileProbabilities.Length; j++)
        {
            float end = start + projectileProbabilities[j] / 100;
            probabilities[j, 0] = start;
            probabilities[j, 1] = end;
            start += end;
            print(probabilities[j, 0] + " ... " + probabilities[j, 1]);
        }
    }

    protected void Update()
    {
        target = null;
        Collider2D hit = Physics2D.OverlapBox(spawningPoint, new Vector2((maxPatrolDistance.x - margin) * 2, maxPatrolDistance.y), 0, playerLayer);
        if (hit)
        {
            target = hit.transform;
        }
        if(type == Type.FlyingPatrol)
        {
            TryLandProjectile();
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawningPoint, new Vector2((maxPatrolDistance.x - margin) * 2, maxPatrolDistance.y));
        Gizmos.color = Color.red;
        if (target != null)
            Gizmos.DrawLine(transform.position, target.position);
    }

    // Flying AI behaviours
    public enum FireType { Single, Triple, Auto, Mayhem}
    [Header("Flying Attack Settings")]
    public FireType fireType;
    public float fireCooldown = 2;

    public bool readyToFire = true;
    public float pauseAfterFire = .5f;
    protected bool alignedWithTarget = false;

    // Indicates if object is firing
    protected bool isFiring = false;

    IEnumerator StartFireCooldown()
    {
        yield return new WaitForSeconds(fireCooldown);
        readyToFire = true;
    }

    void TryLandProjectile()
    {
        if (target == null)
            return;
        switch (fireType)
        {
            case (FireType.Single):
                if (alignedWithTarget && readyToFire)
                {
                    SelectNewProjectile();
                    EnemyProjectile projectile = GameObject.Instantiate(currentProjectile, projectileSpawnPoint.position, Quaternion.identity).GetComponent<EnemyProjectile>();
                    projectile.direction = Vector2.down;
                    projectile.owner = gameObject;
                    StartCoroutine("PauseMovement");
                    StartCoroutine("StartFireCooldown");
                    readyToFire = false;
                }
                break;
            case (FireType.Triple):
                if (readyToFire)
                {
                    SelectNewProjectile();
                    EnemyProjectile projectile = GameObject.Instantiate(currentProjectile, projectileSpawnPoint.position, Quaternion.identity).GetComponent<EnemyProjectile>();
                    projectile.direction = Vector2.down;
                    projectile.owner = gameObject;
                    SelectNewProjectile();
                    projectile = GameObject.Instantiate(currentProjectile, projectileSpawnPoint.position, Quaternion.identity).GetComponent<EnemyProjectile>();
                    projectile.direction = new Vector2(-.5f, -.707f);
                    projectile.owner = gameObject;
                    SelectNewProjectile();
                    projectile = GameObject.Instantiate(currentProjectile, projectileSpawnPoint.position, Quaternion.identity).GetComponent<EnemyProjectile>();
                    projectile.direction = new Vector2(.5f, -.707f);
                    projectile.owner = gameObject;
                    readyToFire = false;
                    StartCoroutine("PauseMovement");
                    StartCoroutine("StartFireCooldown");
                }
                break;
            case (FireType.Auto):
                if (readyToFire)
                {
                    StartCoroutine("FireAuto");
                    readyToFire = false;
                }
                break;
            case (FireType.Mayhem):
                if (readyToFire)
                {
                    StartCoroutine("FireMayhem");
                    readyToFire = false;
                }
                break;
        }
    }

    void SelectNewProjectile()
    {
        float value = Random.value;
        for(int i = 0; i < projectileProbabilities.Length; i++)
        {
            if (value >= probabilities[i, 0] && value < probabilities[i, 1])
            {
                currentProjectile = projectilePrefabs[i];
                return;
            }
        }
        currentProjectile = projectilePrefabs[0];
    }

    IEnumerator FireAuto()
    {
        isFiring = true;
        float angle = Mathf.PI / 9;
        for(int i = 1; i < 9; i++)
        {
            SelectNewProjectile();
            EnemyProjectile projectile = GameObject.Instantiate(currentProjectile, projectileSpawnPoint.position, Quaternion.identity).GetComponent<EnemyProjectile>();
            projectile.direction = new Vector2(Mathf.Cos(Mathf.PI + i * angle), Mathf.Sin(Mathf.PI + i * angle));
            projectile.owner = gameObject;
            yield return new WaitForSeconds(.25f);
        }
        isFiring = false;
        StartCoroutine("StartFireCooldown");
    }

    IEnumerator FireMayhem()
    {
        isFiring = true;
        System.Random r = new System.Random();
        float angle = Mathf.PI / 9;
        for (int i = 0; i < 30; i++)
        {
            SelectNewProjectile();
            Rigidbody2D projectile = GameObject.Instantiate(currentProjectile, mayhemProjectileSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            EnemyProjectile enemyProjectile = projectile.GetComponent<EnemyProjectile>();
            enemyProjectile.owner = gameObject;
            projectile.bodyType = RigidbodyType2D.Dynamic;
            projectile.gravityScale = .5f;
            projectile.GetComponent<EnemyProjectile>().type = EnemyProjectile.Type.Mayhem;
            int direction = (Random.value < .5f) ? -1 : 1;
            projectile.AddForce(new Vector2(Random.value * direction, Random.value) * 3, ForceMode2D.Impulse);
            yield return new WaitForSeconds(.075f);
        }
        isFiring = false;
        StartCoroutine("StartFireCooldown");
    }

    IEnumerator PauseMovement()
    {
        canMove = false;
        yield return new WaitForSeconds(pauseAfterFire);
        canMove = true;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            GameManager.Instance.KillEnemy(this);
            if(destroyEffect)
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
