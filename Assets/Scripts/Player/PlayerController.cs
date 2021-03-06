﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement
    public enum TypeOfLevel {KillEnemiesFront, Fly, Run, Puzzles}
    public TypeOfLevel typeLevel;

    [Header("Controllers")]
    public Rigidbody2D rb;               // Rigid Body of Player 1 
    public float speed;                  // Speed movement
    private float moveHorit;             // Movement Horitzontal

    [Header("Cont. Level Kill Front and Run")]
    public float jumpForce;              // How much will jump
    public bool planMode;                // Goes down slowly

    [Header("Cont. Level Fly")]
    public bool flyMode;                 // Walking or flying
    public float flySpeed;               // Speed Flying
    private float moveVertic;            // Movement Vertical

    [Header("Shot")]
    public Transform gunPosition;        // Position where shot begins
    public GameObject shot;              // Bullet
    public bool isShooting;              // Know if we have shooted
    private int frameCounter;            // Delay tfor another shot

    [Header("CheckGround")]
    public bool isGrounded;              // Is at floor or not
    public float groundRadius;           // Radius of zone for detect floor
    public Transform groundChecker;      // Floor
    public LayerMask groundMask;

    [Header("Graphics")]
    public Transform graphics;           // Sprite
    public Animator anim;
    private bool facingRight;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();       // We get the component from de object
	}
	
	// Update is called once per frame
	void Update ()
    {
        //CheckGround
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundRadius, groundMask);   // Detection

        #region FLY
        if (typeLevel == TypeOfLevel.Fly)
        {
            if (!flyMode)
            {
                rb.gravityScale = 0;
                flyMode = true;
            }

            #region FlyMovement
            moveVertic = Input.GetAxis("Vertical");

            rb.velocity = new Vector2(rb.velocity.x, moveVertic * flySpeed);
            #endregion

        }
        #endregion

        #region WALK/ RUN
        else if ((typeLevel == TypeOfLevel.KillEnemiesFront) || (typeLevel == TypeOfLevel.Run))
        {
            if (flyMode)
            {
                rb.gravityScale = 1;
                flyMode = false;
            }

            #region Jump     
            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, jumpForce));
            }
            #endregion

            #region Plan Mode
            if (Input.GetKeyDown(KeyCode.P))
            {
                planMode = !planMode;
                if (planMode)
                {
                    if (rb.velocity.y < 0)
                        rb.gravityScale = 0.05f;
                    else
                        rb.gravityScale = 1f;
                }
                else
                {
                    rb.gravityScale = 1f;
                }
            }
            #endregion

        }
        #endregion

        #region PUZZLE
        else if (typeLevel == TypeOfLevel.Puzzles)
        {
            

        }
        #endregion

        #region Multiple Options
        if ((typeLevel == TypeOfLevel.KillEnemiesFront) || (typeLevel == TypeOfLevel.Run) || (typeLevel == TypeOfLevel.KillEnemiesFront))
        {
            #region Horitzontal Movement 
            //Input
            moveHorit = Input.GetAxis("Horizontal");

            //Movement
            rb.velocity = new Vector2(moveHorit * speed, rb.velocity.y);

            if (moveHorit < 0 && facingRight) Flip();
            else if (moveHorit > 0 && !facingRight) Flip();
            #endregion

            #region Shoot
            if (!isShooting && Input.GetButtonDown("Fire1"))
            {
                isShooting = true;
                GameObject shotAux = (Instantiate(shot, gunPosition.position, Quaternion.identity)) as GameObject;
                if (facingRight) shotAux.GetComponent<ShotBehaviour>().setShotConfig(7, 2.5f);//Speed and destruction time
                else if (!facingRight) shotAux.GetComponent<ShotBehaviour>().setShotConfig(-7, 2.5f);//Speed and destruction time

                frameCounter = 30;
            }

            if (isShooting)
            {
                frameCounter--;
                if (frameCounter <= 0) isShooting = false;

            }
            #endregion
        }
        #endregion
    }

    void Flip()
    {
        Vector2 newScale = graphics.localScale;
        newScale.x *= -1;
        graphics.localScale = newScale;
        facingRight = !facingRight;

    }

    //Creemes una funcion para saber como esta mirando 
    public bool GetFace()
    {
        return facingRight;
    }
}
