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

    // Detection radius of player
    public float detectionRadius;

    // Target (player) if locked
    [HideInInspector]
    public Transform target;

    void Update()
    {
        target = null;
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
        if (hit)
        {
            target = hit.transform;
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        if (target != null)
            Gizmos.DrawLine(transform.position, target.position);
    }
}
