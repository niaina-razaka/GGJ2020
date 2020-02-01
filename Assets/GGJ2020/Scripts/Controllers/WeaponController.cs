using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform shotPoint;

    public GameObject projectile;

    public float offset;
    private float timeBtWShots = 1;
    public float startTimeBtwShots;
    public float timeScale;


    public GameObject player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        Rotate();
    }

    void FollowPlayer()
    {
        transform.position = player.transform.position;
    }
    void Rotate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtWShots <= 0)
        {
           if (Input.GetMouseButton(1))
          //  if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBtWShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtWShots -= Time.deltaTime;
        }

    }
}
