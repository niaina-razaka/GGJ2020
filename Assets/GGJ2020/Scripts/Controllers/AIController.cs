using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AIController : AI
{
    // If object is facing right or left
    [Header("Movement Settings")]
    public bool isFacingRight = true;

    // Movement speed
    public float moveSpeed = 2;

    // 1 if going right, -1 if going left
    int direction = 1;

    // Spawning point
    Vector2 spawningPoint;

    // Maximum patrolling distance from spawning point
    public float maxPatrolDistance = 5;

    // If object has just reached max distance
    bool maxDistanceReached = false;

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

    void Start()
    {
        //base.Start();
        rb = GetComponent<Rigidbody2D>();
        direction = (isFacingRight) ? 1 : -1;
        gravityValue = Mathf.Abs(Physics2D.gravity.y);

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
                    CheckEdge(position);
                    rb.velocity = new Vector2(direction * moveSpeed, 0);
                }
            }
            // If jump patrol
            else if (type == Type.JumpingPatrol)
            {
                if (grounded && previousGroundCheck != grounded)
                {
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
            CheckMaxDistanceReached();
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }

    // Checks if max patrol distance is reached (works on uniform movement types only)
    void CheckMaxDistanceReached()
    {
        if (Vector2.Distance(transform.position, spawningPoint) > maxPatrolDistance && !maxDistanceReached)
        {
            ChangeDirection();
            maxDistanceReached = true;
            StartCoroutine("StartDirectionChangeCountdown");
        }
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

    }

    // Ground patrol AI
    #region

    // Edge check parameters
    [Header("Ground Patrol Settings")]
    public Vector2 edgeCheckPosition;
    public float edgeCheckLength;


    void CheckEdge(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(position.x + edgeCheckPosition.x * direction, (transform.position.y + edgeCheckPosition.y)), Vector2.down, edgeCheckLength, environmentLayers);
        if (!hit)
        {
            ChangeDirection();
        }
    }
    #endregion

    // Ground jumping AI
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
        if(Vector2.Distance(spawningPoint, (Vector2)transform.position + Vector2.right * jumpValues.x) > maxPatrolDistance)
        {
            ChangeDirection();
        }
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + jumpValues.x * direction, (transform.position.y + nextDestinationCheckYPosition)), Vector2.down, nextDestinationCheckLength, environmentLayers);
        if (!hit)
        {
            ChangeDirection();
        }
    }
    #endregion

    // Flying AI
    #region
    #endregion
}