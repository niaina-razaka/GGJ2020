﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyProjectile : MonoBehaviour
{
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
        if (collider.CompareTag("Enemy") || collider.CompareTag("EnemyProjectile"))
        {
            return;
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
