using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed;
    public GameObject floatingpoint;
    public GameObject effect;
    private static int count=0;

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
            GameObject.FindGameObjectWithTag("CM").GetComponent<SimpleCameraShakeInCinemachine>().Shake();

            count = 0;
            Destroy(gameObject);

        }
        if (other.tag == gameObject.tag)
        {
            GameObject floatingtext =  Instantiate(floatingpoint, transform.position, Quaternion.identity) as GameObject;
            Instantiate(effect, transform.position, Quaternion.identity);
            if (count == 3)
            {
                floatingtext.transform.GetChild(0).GetComponent<TextMesh>().text = "PIX";

            }
            else
            {
                floatingtext.transform.GetChild(0).GetComponent<TextMesh>().text = "NICE";

            }
            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossNiv1>().TakeDamage();
            count++;
            Destroy(other.gameObject);
            Destroy(gameObject);

        }
        else if(!other.CompareTag("Player") && !other.CompareTag("Platform"))
        {
            Debug.Log("Are you OK" + count);
            GameObject floatingtext = Instantiate(floatingpoint, transform.position, Quaternion.identity) as GameObject;
            Instantiate(effect, transform.position, Quaternion.identity);
            if (count == -3)
            {
                floatingtext.transform.GetChild(0).GetComponent<TextMesh>().text = "ARE YOU OK ?";

            }
            else
            {
                floatingtext.transform.GetChild(0).GetComponent<TextMesh>().text = "BAD";

            }
            GameObject.FindGameObjectWithTag("CM").GetComponent<SimpleCameraShakeInCinemachine>().Shake();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Life -= 1;
            count--;
            Destroy(other.gameObject);
            Destroy(gameObject);
            
        }
    }
}
