using System;
using System.Collections;
using UnityEngine;
[Serializable]
public class Player: MonoBehaviour
{
    [Header("Player stat")]
    public int Life = 5;
    public float Energy = 3;

    // Becomes invincible briefly after taking damage
    public bool invincibility = true;
    public float invincibilityTime = 2;


    PlayerController playerController;

    protected void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void TakeDamage(int amount)
    {
        Life -= amount;
        if (invincibility)
        {
            StartCoroutine("ActivateInvincibility");
        }
        if (Life <= 0)
        {
            Kill();
        }
    }

    IEnumerator ActivateInvincibility()
    {
        StartCoroutine("InvincibleAnimation");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer);
        yield return new WaitForSeconds(invincibilityTime);

        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, false);
        StopCoroutine("InvincibleAnimation");
        ChangeSpriteAlpha(255);
    }

    IEnumerator InvincibleAnimation()
    {
        while (true)
        {
            print("test");
            yield return new WaitForSeconds(.1f);
            Color tmp = playerController.spriteRenderer.color;
            float alpha = (tmp.a == 0) ? 255 : 0;
            ChangeSpriteAlpha(alpha);
        }
    }

    void ChangeSpriteAlpha(float value)
    {
        Color tmp = playerController.spriteRenderer.color;
        tmp.a = value;
        playerController.spriteRenderer.color = tmp;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AI enemy = collision.gameObject.GetComponent<AI>();
            TakeDamage(enemy.damage);
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            Kill();
        }
    }

    void Kill()
    {
        gameObject.SetActive(false);
        AudioManager.Instance.PlaySound("dead");
        GameManager.Instance.EndGame();
    }
}
