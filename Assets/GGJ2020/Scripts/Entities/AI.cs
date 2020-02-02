using System.Collections;
using System.Collections.Generic;
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
    public GameObject projectilePrefab;

    // Indicates if object is edible or not
    public bool edible;

    [Header("Combat Settings")]
    public int health = 1;
    public int damage = 1;

    protected bool canMove = true;

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
                    EnemyProjectile projectile = GameObject.Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<EnemyProjectile>();
                    projectile.direction = Vector2.down;
                    StartCoroutine("PauseMovement");
                    StartCoroutine("StartFireCooldown");
                    readyToFire = false;
                }
                break;
            case (FireType.Triple):
                if (readyToFire)
                {
                    EnemyProjectile projectile = GameObject.Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<EnemyProjectile>();
                    projectile.direction = Vector2.down;
                    projectile = GameObject.Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<EnemyProjectile>();
                    projectile.direction = new Vector2(-.5f, -.707f);
                    projectile = GameObject.Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<EnemyProjectile>();
                    projectile.direction = new Vector2(.5f, -.707f);
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

    IEnumerator FireAuto()
    {
        isFiring = true;
        float angle = Mathf.PI / 9;
        for(int i = 1; i < 9; i++)
        {
            EnemyProjectile projectile = GameObject.Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<EnemyProjectile>();
            projectile.direction = new Vector2(Mathf.Cos(Mathf.PI + i * angle), Mathf.Sin(Mathf.PI + i * angle));
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
            Rigidbody2D projectile = GameObject.Instantiate(projectilePrefab, mayhemProjectileSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            projectile.bodyType = RigidbodyType2D.Dynamic;
            projectile.gravityScale = .5f;
            projectile.GetComponent<EnemyProjectile>().type = EnemyProjectile.Type.Mayhem;
            int direction = (Random.value < .5f) ? -1 : 1;
            print(direction);
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
        health -= amount;
        if (health <= 0)
        {
            GameManager.Instance.KillEnemy(this);
            Destroy(gameObject);
        }
    }

}
