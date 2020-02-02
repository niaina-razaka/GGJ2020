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
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer);
        yield return new WaitForSeconds(invincibilityTime);
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, false);

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
    }


}
