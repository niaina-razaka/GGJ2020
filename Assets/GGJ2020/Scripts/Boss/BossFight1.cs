using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight1 : MonoBehaviour
{
    public GameObject[] projectile;
    public Transform shotpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(projectile[0], shotpoint.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(projectile[1], shotpoint.position, Quaternion.identity);

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Instantiate(projectile[2], shotpoint.position, Quaternion.identity);

        }
    }
  
}
