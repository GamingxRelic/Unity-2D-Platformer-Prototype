using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaLogic : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) {
            PlayerMovement.instance.on_death();

            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            player.Invoke("RespawnAtCheckpoint", 1f);
        }
    }
}
