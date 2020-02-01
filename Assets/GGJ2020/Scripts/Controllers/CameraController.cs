using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x_axis = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(x_axis, 0);
        rb2d.velocity = movement * speed;
    }
}
