using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerMovement player;
    Animator animator;
    SpriteRenderer sprite;
    [SerializeField] AudioSource death_audio;
    
    // Currently Unused:

    //.SetTrigger("Hurt");
    //.SetTrigger("Attack");
    //.SetInteger("AnimState", 1); //Combat Idle
    int idle_anim = 0; 


    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = gameObject.GetComponent<PlayerMovement>();
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();


        player.on_jump += OnJump;
        PlayerMovement.instance.on_death += OnDeath;
        player.on_respawn += OnRespawn;
    }

    void Update()
    {

        if(!player.alive) {
            return;
        }

        // Set grounded check
        if(player.is_grounded) {
            animator.SetBool("Grounded", player.is_grounded);
        } else {
            animator.SetTrigger("Jump");
        }

        // Set airspeed
        animator.SetFloat("AirSpeed", rb.velocity.y);

        float horizontal_input = Input.GetAxisRaw("Horizontal");

        // Run
        if(Mathf.Abs(horizontal_input) > Mathf.Epsilon && player.is_grounded) {
            animator.SetInteger("AnimState", 2);
        }

        // Idle
        else {
            animator.SetInteger("AnimState", idle_anim);
        }

        // Flipping Character
        if(rb.velocity.x > 0.1f) {
                sprite.flipX = true;
                player.left_facing = false;
            } 
        else if(rb.velocity.x < -0.1f) {
            sprite.flipX = false;
            player.left_facing = true;
        }

    }
    public void OnJump() {
        animator.SetTrigger("Jump");
        animator.SetBool("Grounded", player.is_grounded);
    }

    private void OnDeath() {
        death_audio.Play();
        animator.SetTrigger("Death");
    }

    private void OnRespawn() {
        animator.SetTrigger("Recover");
    }
}
