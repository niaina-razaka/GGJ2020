using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // If object is facing right or left
    bool isFacingRight = true;
    public bool IsFacingRight
    {
        get
        {
            return isFacingRight;
        }
        set
        {
            isFacingRight = value;
            direction = (value) ? 1 : -1;
        }
    }

    // Movement speed
    public float moveSpeed = 2;

    // 1 if going right, -1 if going left
    int direction = 1;

    // If object is grounded or not
    bool grounded = false;

    // LayerMask for environment collisions check
    public LayerMask environmentLayers;

    // Ground check parameters
    public Vector2 groundCheckPosition;
    public Vector2 groundCheckSize;

    // Edge check parameters
    public Vector2 edgeCheckPosition;
    public float edgeCheckLength;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = (isFacingRight) ? 1 : -1;
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
    void FixedUpdate()
    {
        Vector2 position = transform.position;
        CheckGround(position);
        if (grounded)
        {
            CheckEdge(position);
            rb.velocity = new Vector2(direction * moveSpeed, 0);
        }
    }

    void CheckEdge(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(position.x + edgeCheckPosition.x * direction, (transform.position.y + edgeCheckPosition.y)), Vector2.down, edgeCheckLength, environmentLayers);
        if (!hit)
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        IsFacingRight = !IsFacingRight;
        transform.Rotate(Vector3.up * 180);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2)transform.position + groundCheckPosition, groundCheckSize);
        Gizmos.color = Color.red;
        Vector2 edgePosition = new Vector2(transform.position.x + edgeCheckPosition.x * direction, (transform.position.y + edgeCheckPosition.y));
        Gizmos.DrawLine(edgePosition, edgePosition + Vector2.down * edgeCheckLength);
    }
}
