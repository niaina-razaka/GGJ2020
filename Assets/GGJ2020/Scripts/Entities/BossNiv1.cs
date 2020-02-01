using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossNiv1 : MonoBehaviour
{
    public Slider healthBar;
    private int life=10;
    private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = life;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
            healthBar.value = (float)life/maxHealth;
    }
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            life--;
        }
            
    }
    public void TakeDamage()
    {
        this.life -= 1;
    }
}
