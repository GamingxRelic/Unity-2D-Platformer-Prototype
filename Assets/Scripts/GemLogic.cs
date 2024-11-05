using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemLogic : MonoBehaviour
{
    SpriteRenderer sprite;
    BoxCollider2D gem_collider;
    AudioSource gem_audio;

    private void Start() {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        gem_collider = gameObject.GetComponent<BoxCollider2D>();
        gem_audio = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) {
            ScoreTextLogic.score_increase();
            gem_audio.Play();
            sprite.enabled = false;
            gem_collider.enabled = false;
            Destroy(gameObject, 1f);
        }
    }
}
