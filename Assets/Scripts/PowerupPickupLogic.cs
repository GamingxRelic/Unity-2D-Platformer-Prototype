using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickupLogic : MonoBehaviour
{
    SpriteRenderer sprite;
    BoxCollider2D pickup_collider;
    AudioSource pickup_audio;

    private void Start() {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        pickup_collider = gameObject.GetComponent<BoxCollider2D>();
        pickup_audio = gameObject.GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();

        if(player != null) {
            player.air_jumps += 1;
        }

        pickup_audio.Play();
        sprite.enabled = false;
        pickup_collider.enabled = false;
        Destroy(gameObject, 1f);
    }
}
