﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float startTmeBtwSpawn;
    private float tmeBtwSpawn;
    public GameObject[] enemies;

    private void Update()
    {
        if(tmeBtwSpawn <= 0)
        {
            int rand = Random.Range(0, enemies.Length);
          Instantiate(enemies[rand],transform.position,Quaternion.identity);

            tmeBtwSpawn = startTmeBtwSpawn;
        }
        else
        {
            tmeBtwSpawn -= Time.deltaTime;
        }
        if(GameObject.FindGameObjectWithTag("Boss").GetComponent<BossNiv1>().life == 5)
        {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<Spawner>().startTmeBtwSpawn = 0.5f;
        }
    }
}
