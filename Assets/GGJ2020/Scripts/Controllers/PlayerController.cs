﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player
{
    public PlayerMovement controller;

    public float runSpeed = 40f;

    public SpriteRenderer spriteRenderer;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    new void Start()
    {
        base.Start();
        controller = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (horizontalMove != 0)
        {
            gameObject.GetComponent<Animator>().SetBool("isRunning", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isRunning", false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            if (controller.Grounded)
            {
                AudioManager.Instance.PlaySound("jump");
            }
        }

        //if (Input.GetButtonDown("Crouch"))
        //{
        //    crouch = true;
        //}
        //else if (Input.GetButtonUp("Crouch"))
        //{
        //    crouch = false;
        //}

    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void PlayFootStep()
    {
        if(horizontalMove != 0 && controller.Grounded)
        AudioManager.Instance.PlaySound("footstep");
    }

}
