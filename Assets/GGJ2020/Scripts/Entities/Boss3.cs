using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIController))]
public class Boss3 : MonoBehaviour
{
    AIController controller;

    void Start()
    {
        controller = GetComponent<AIController>();
    }

    void Update()
    {
        if (controller.currentHealth <= controller.health * 2 / 3)
        {
            if (controller.currentHealth <= controller.health / 3)
            {
                    controller.fireType = AI.FireType.Auto;
            }
            else
            {
                controller.fireType = AI.FireType.Triple;
            }
        }
    }
}
