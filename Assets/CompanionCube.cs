using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionCube : MonoBehaviour
{
    bool is_being_held = false;
    bool player_in_range = false;
    Rigidbody2D rb;
    [SerializeField] private BoxCollider2D rb_collider;
    [SerializeField] private BoxCollider2D trigger_collider;
    PlayerMovement player;
    Vector3 held_offset = new Vector3(0, 1.5f, 0);
    float throw_force = 7f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = PlayerMovement.instance;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) {
            if(is_being_held) {
                DropBox(false);
            } else if(player_in_range) {
                PickupBox();
            }
        } 
        else if(Input.GetKeyDown(KeyCode.G)) {
            if(is_being_held) {
                DropBox(true);
            }
        }
    }


    void DropBox(bool throw_box) {
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.SetParent(null); // Detach from the player
        transform.position = player.gameObject.transform.position + held_offset;
        rb_collider.enabled = true;
        trigger_collider.enabled = true;
        is_being_held = false;

        if(throw_box) {
            Vector3 dir = player.rb.velocity.normalized;
            
            if(dir == Vector3.zero) {
                if(player.left_facing) {
                    dir = new Vector3(-1f, 0, 0);
                } else {
                    dir = new Vector3(1f, 0, 0);
                }
            }

            rb.velocity = dir * throw_force;
        } else {
            rb.velocity = player.rb.velocity;
        }
        
    }

    void PickupBox() {
        rb_collider.enabled = false;
        trigger_collider.enabled = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.SetParent(player.gameObject.transform);
        transform.position = player.gameObject.transform.position + held_offset;
        is_being_held = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            player_in_range = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            player_in_range = false;
    }
}
