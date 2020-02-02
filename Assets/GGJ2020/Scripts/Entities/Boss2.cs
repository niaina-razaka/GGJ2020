using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIController))]
public class Boss2 : MonoBehaviour
{
    AIController controller;

    void Start()
    {
        controller = GetComponent<AIController>();
    }

    void Update()
    {
        if (controller.currentHealth <= controller.health / 2)
        {
            controller.fireType = AI.FireType.Triple;
        }
    }


}
