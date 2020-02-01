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
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Life -= 1;
            Destroy(gameObject);

        }
        if (other.tag == gameObject.tag)
        {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossNiv1>().TakeDamage();
            Destroy(other.gameObject);
            Destroy(gameObject);

        }
        else if(!other.CompareTag("Player"))
        {
            Debug.Log("Are you OK");
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Life -= 1;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
