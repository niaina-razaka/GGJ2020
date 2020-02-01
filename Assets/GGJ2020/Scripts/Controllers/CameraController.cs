using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCam;
    public float lenght = 0.1f;
    public float shakeAmount = 0.1f;
    public float speed;
    public bool shake = false;
    public bool manualControl = false;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (manualControl)
        {
            float x_axis = Input.GetAxis("Horizontal");
            Vector2 movement = new Vector2(x_axis, 0);
            rb2d.velocity = movement * speed;
            Shake();
        }
    }

    public void Shake()
    {
        if (Input.GetKey(KeyCode.Space) && shake)
        {
            StartCoroutine(BeginShake());
        }
            
    }
    private IEnumerator BeginShake()
    {
        Vector3 oldPos = mainCam.transform.position;
        Vector3 op = mainCam.transform.position;
        float lenghtCoroutine = lenght;
        lenghtCoroutine += Time.time;
        while (Time.time <= lenghtCoroutine)
        {
            Vector3 camPos = mainCam.transform.position;
            float shakeX = Random.value * shakeAmount;
            float shakeY = Random.value * shakeAmount;

            camPos.x += shakeX;
            camPos.y += shakeY;

            mainCam.transform.position = camPos;

            yield return new WaitForEndOfFrame();
            mainCam.transform.position = oldPos;
        }
        mainCam.transform.position = op;
    }
}
