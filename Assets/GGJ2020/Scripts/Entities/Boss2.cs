using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AIController))]
public class Boss2 : MonoBehaviour
{
    AIController controller;
    Image lifeBar;

    void Start()
    {
        controller = GetComponent<AIController>();
        lifeBar = FindObjectOfType<UIManager>().bossLifeBar;
    }

    void Update()
    {
        if (controller.currentHealth <= controller.health / 2)
        {
            controller.fireType = AI.FireType.Triple;
        }
        lifeBar.fillAmount = (float)((double)controller.currentHealth / (double)controller.health);
    }

    // 5 / 5 = 1
    // 4 / 5 = 0.8
}
