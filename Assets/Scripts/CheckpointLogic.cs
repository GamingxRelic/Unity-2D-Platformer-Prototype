using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointLogic : MonoBehaviour
{
    [SerializeField] Transform checkpoint_transform;
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCheckpointHandler player = other.gameObject.GetComponent<PlayerCheckpointHandler>();
        if(player != null) {
            player.recent_checkpoint_pos = checkpoint_transform.position;
        }
    }
}
