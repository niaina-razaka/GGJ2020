using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class AIController : AI
{
    // If object is facing right or left
    [Header("Movement Settings")]
    public bool isFacingRight = true;

    // Movement speed
    public float moveSpeed = 2;

    // 1 if going right, -1 if going left
    int direction = 1;

    public float checkFrontValue = .1f;

    [Space(10)]
    [Header("Ground Check Settings")]
    // LayerMask for environment collisions check
    public LayerMask environmentLayers;

    // Ground check parameters
    public Vector2 groundCheckPosition;
    public Vector2 groundCheckSize;

    // If object is grounded or not
    bool grounded = false;

    Rigidbody2D rb;
    CapsuleCollider2D capsule;
    float capsuleWidth;

    new void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        capsuleWidth = capsule.bounds.size.x;
        gravityValue = Mathf.Abs(Physics2D.gravity.y);
        if (isFacingRight)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        if(type == Type.FlyingPatrol)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        spawningPoint = transform.position;
    }

    void ChangeDirection()
    {
        isFacingRight = !isFacingRight;
        direction = -direction;
        transform.Rotate(Vector3.up * 180);
    }

    void CheckGround(Vector2 position)
    {
        grounded = false;
        Collider2D hit = Physics2D.OverlapBox(position + groundCheckPosition, groundCheckSize, 0, environmentLayers);
        if (hit)
        {
            grounded = true;
        }
    }
    void Update()
    {
        base.Update();
        print("lol");
        if (!canMove)
        {
            return;
        }

        Vector2 position = transform.position;
        // If object is a ground unit
        if(type != Type.FlyingPatrol)
        {
            CheckGround(position);
            // If Ground patrol
            if (type == Type.GroundPatrol)
            {
                if (grounded)
                {
                    CheckMaxDistanceReached();
                    if (target != null)
                    {
                            MoveTo(target.position, true, false);
                    }
                    else
                    {
                        if (!CheckEdge(position) || CheckFront(position))
                        {
                            ChangeDirection();
                        }
                        rb.velocity = new Vector2(direction * moveSpeed, 0);
                    }
                }
            }
            // If jump patrol
            else if (type == Type.JumpingPatrol)
            {
                if (grounded && previousGroundCheck != grounded)
                {
                    if (target)
                        JumpTo(target.transform.position);
                    checkNextDestination();
                    readyToJump = true;
                    jumping = false;
                }
                if (grounded && readyToJump)
                {
                    readyToJump = false;
                    StartCoroutine("InitiateJump");
                }
                if (jumping && !grounded)
                {
                    rb.velocity = new Vector2(jumpValues.x / (2 * timeToJumpApex) * rb.gravityScale * direction, rb.velocity.y);
                }
                previousGroundCheck = grounded;
            }
        }
        else if(type == Type.FlyingPatrol)
        {
            if (isFiring)
            {
                return;
            }
            CheckMaxDistanceReached();
            if (target != null)
            {
                MoveTo(target.position, false, true);
            }
            else
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
        }
    }

    // For moving towards/above the player (uniform movements only)
    void MoveTo(Vector2 destination, bool withPhysics, bool airborne)
    {
        if (!airborne)
        {
            if (!CheckEdge(transform.position) || CheckFront(transform.position))
            {
                return;
            }
        }
        if(Vector2.Distance(transform.position, new Vector2(destination.x, transform.position.y)) > .05f){
            alignedWithTarget = false;
            if((destination.x - transform.position.x > 0 && !isFacingRight) || (destination.x - transform.position.x < 0 && isFacingRight))
            {
                ChangeDirection();
            }
            if(withPhysics)
                rb.velocity = new Vector2(direction * moveSpeed, 0);
            else
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            alignedWithTarget = true;
        }
    }

    // For moving towards/above the player (jump movements only)
    void JumpTo(Vector2 destination)
    {
        if ((destination.x - transform.position.x > 0 && !isFacingRight) || (destination.x - transform.position.x < 0 && isFacingRight))
        {
            ChangeDirection();
        }
    }

    // Checks if max patrol distance is reached (works on uniform movement types only)
    void CheckMaxDistanceReached()
    {
        if (Mathf.Abs(transform.position.x - spawningPoint.x) >= maxPatrolDistance.x && !maxDistanceReached)
        {
            ChangeDirection();
            maxDistanceReached = true;
            StartCoroutine("StartDirectionChangeCountdown");
        }
    }

    bool CheckFront(Vector2 position)
    {
        Vector2 origin = position + Vector2.right * direction * capsuleWidth / 2;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right * direction, checkFrontValue, environmentLayers);
        if (hit)
        {
            return true;
        }
        return false;
    }

    // Refrains object from changing direction too fast when max patrolling distance is reached
    IEnumerator StartDirectionChangeCountdown()
    {
        yield return new WaitForSeconds(1);
        maxDistanceReached = false;
    }

    void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2)transform.position + groundCheckPosition, groundCheckSize);
        Gizmos.color = Color.black;
        Gizmos.DrawLine(spawningPoint + Vector2.left * maxPatrolDistance, spawningPoint + Vector2.right * maxPatrolDistance);

        Gizmos.color = Color.red;
        if (type == Type.GroundPatrol)
        {
            Vector2 edgePosition = new Vector2(transform.position.x + edgeCheckPosition.x * direction, (transform.position.y + edgeCheckPosition.y));
            Gizmos.DrawLine(edgePosition, edgePosition + Vector2.down * edgeCheckLength);
        }
        else if (type == Type.JumpingPatrol)
        {
            Vector2 edgePosition = new Vector2(transform.position.x + jumpValues.x * direction, (transform.position.y + nextDestinationCheckYPosition));
            Gizmos.DrawLine(edgePosition, edgePosition + Vector2.down * nextDestinationCheckLength);
        }
        else if (type == Type.FlyingPatrol)
        {

        }
        Gizmos.color = Color.cyan;
        Vector2 frontOrigin = (Vector2)transform.position + Vector2.right * direction * capsuleWidth / 2;
        Gizmos.DrawLine(frontOrigin, frontOrigin + Vector2.right * direction * checkFrontValue);
    }

    // Ground patrol AI movements
    #region

    // Edge check parameters
    [Header("Ground Patrol Settings")]
    public Vector2 edgeCheckPosition;
    public float edgeCheckLength;


    bool CheckEdge(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(position.x + edgeCheckPosition.x * direction, (transform.position.y + edgeCheckPosition.y)), Vector2.down, edgeCheckLength, environmentLayers);
        if (!hit)
        {
            return false;
        }
        return true;
    }
    #endregion

    // Ground jumping AI movements
    #region

    [Header("Ground Jumping Settings")]
    // Jump height and length values
    public Vector2 jumpValues = new Vector2(2, 2);

    // Time to reach the jump apex
    float timeToJumpApex;

    // Gravity y absolute value
    float gravityValue;

    // Seconds to wait before jumping again
    public float timeToWait = 2;

    // Stores the value of previous ground check
    bool previousGroundCheck;

    // Indicates if object is ready to jump
    bool readyToJump;

    // Indicates if object is jumping
    bool jumping = false;

    // Edge check parameters
    public float nextDestinationCheckYPosition;
    public float nextDestinationCheckLength;

    IEnumerator InitiateJump()
    {
        timeToJumpApex = Mathf.Sqrt((2 * jumpValues.y) / gravityValue);
        yield return new WaitForSeconds(timeToWait);
        float jumpVelocity = gravityValue * timeToJumpApex;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
        jumping = true;
    }

    void checkNextDestination()
    {
        if(Mathf.Abs((spawningPoint.x - (transform.position.x + jumpValues.x * direction))) > maxPatrolDistance.x)
        {
            ChangeDirection();
        }
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + jumpValues.x * direction, (transform.position.y + nextDestinationCheckYPosition)), Vector2.down, nextDestinationCheckLength, environmentLayers);
        if (!hit)
        {
            ChangeDirection();
        }
        Vector2 origin = (Vector2)transform.position + Vector2.right * direction * capsuleWidth / 2;
        hit = Physics2D.Raycast(origin, Vector2.right * direction, jumpValues.x, environmentLayers);
        if (hit)
        {
            ChangeDirection();
        }
    }
    #endregion

    // Flying AI movements
    #region
    #endregion
}