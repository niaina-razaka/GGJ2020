using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed;
    public GameObject floatingpoint;
    public GameObject effect;

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


            Destroy(gameObject);

        }
        if (other.tag == gameObject.tag)
        {
            GameObject floatingtext =  Instantiate(floatingpoint, transform.position, Quaternion.identity) as GameObject;
            Instantiate(effect, transform.position, Quaternion.identity);
            floatingtext.transform.GetChild(0).GetComponent<TextMesh>().text = "BIEN";
            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossNiv1>().TakeDamage();
            Destroy(other.gameObject);
            Destroy(gameObject);

        }
        else if(!other.CompareTag("Player") && !other.CompareTag("Platform"))
        {
            Debug.Log("Are you OK");
            GameObject floatingtext = Instantiate(floatingpoint, transform.position, Quaternion.identity) as GameObject;
            Instantiate(effect, transform.position, Quaternion.identity);
            floatingtext.transform.GetChild(0).GetComponent<TextMesh>().text = "SUCK";
            GameObject.FindGameObjectWithTag("CM").GetComponent<SimpleCameraShakeInCinemachine>().Shake();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Life -= 1;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
