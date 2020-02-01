using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight1 : MonoBehaviour
{
    public GameObject[] projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(projectile[0], transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(projectile[1], transform.position, Quaternion.identity);

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(projectile[2], transform.position, Quaternion.identity);

        }
    }
  
}
