using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerMovement player;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sprite;
    
    // Currently Unused:

    //.SetTrigger("Hurt");
    //.SetTrigger("Attack");
    //.SetInteger("AnimState", 1); //Combat Idle
    int idle_anim = 0; 


    void Start() {
        player.on_jump += OnJump;
        PlayerMovement.on_death += OnDeath;
        player.on_respawn += OnRespawn;
    }

    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Alpha1)) {
        //     animator.SetTrigger("Hurt");
        // }
        // else 
        // if(Input.GetKeyDown(KeyCode.Alpha2)) {
        //     player.Die();
        // }
        // else if(Input.GetKeyDown(KeyCode.Alpha3)) {
        //     player.Respawn();
        // }
        // else if(Input.GetKeyDown(KeyCode.Alpha4)) {
        //     animator.SetTrigger("Attack");
        // }
        // else if(Input.GetKeyDown(KeyCode.Alpha5)) {
        //     idle_anim = 1;
        // }
        // else if(Input.GetKeyDown(KeyCode.Alpha6)) {
        //     idle_anim = 0;
        // }

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
            } 
        else if(rb.velocity.x < -0.1f) {
            sprite.flipX = false;
        }

    }
    public void OnJump() {
        animator.SetTrigger("Jump");
        animator.SetBool("Grounded", player.is_grounded);
    }

    private void OnDeath() {
        animator.SetTrigger("Death");
    }

    private void OnRespawn() {
        animator.SetTrigger("Recover");
    }
}
