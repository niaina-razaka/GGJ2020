﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyProjectile : MonoBehaviour
{
    public enum TargetHealth { Normal, Owner}
    public TargetHealth targetHealth;
    public enum Type { Normal, Mayhem}
    // Projectile speed
    public float speed = 3;

    // Projectile lifeTime
    public float lifeTime = 5;

    // Projectile damage
    public int damage = 5;

    [HideInInspector]
    // Projectile direction
    public Vector2 direction;

    [HideInInspector]
    // Projectile direction
    public GameObject owner;

    [HideInInspector]
    public Type type = Type.Normal;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("StartLifetimeCooldown");

        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int enemyProjectileLayer = LayerMask.NameToLayer("EnemyProjectile");
        int environmentLayer = LayerMask.NameToLayer("Environment");
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer);
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyProjectileLayer);
        Physics2D.IgnoreLayerCollision(gameObject.layer, environmentLayer);
    }

    void Update()
    {
        if(type == Type.Normal)
        {
            rb.velocity = direction * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if(targetHealth == TargetHealth.Normal)
            {
                print("owner");
                PlayerController player = collider.GetComponent<PlayerController>();
                player.TakeDamage(damage);
            }
            if(targetHealth == TargetHealth.Owner)
            {
                print("owner");
                owner.GetComponent<AI>().TakeDamage(damage);
            }
        }
        StopCoroutine("StartLifetimeCooldown");
        Destroy(gameObject);
    }

    IEnumerator StartLifetimeCooldown()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

}
