using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Well");
        }
        if(other.tag == gameObject.tag)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);

        }
        else
        {
            Debug.Log("Are you OK");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
