using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    float timeLeft = 01.0f;
    int rand;

    // Start is called before the first frame update
    void Start()
    {
        rand= Random.Range(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
      
        timeLeft -= Time.deltaTime;
        if (timeLeft > 0)
        {
        
            transform.Translate(Vector2.left * rand * Time.deltaTime);
        }
    }
}
