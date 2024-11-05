using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    
    [SerializeField] float coyote_time;
    int colliding_body_count;

    void FixedUpdate()
    {
        if(colliding_body_count > 0) {
            player.is_grounded = true;
        } else {
            if(player.coyote_time_remaining > 0.0f) {
                player.coyote_time_remaining -= Time.deltaTime;
            }
            player.is_grounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        colliding_body_count += 1;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        colliding_body_count -= 1;
        if(colliding_body_count == 0.0f) {
            player.coyote_time_remaining = coyote_time;
        }
    }
}
