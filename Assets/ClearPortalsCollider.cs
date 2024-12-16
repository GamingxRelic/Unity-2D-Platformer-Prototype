using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPortalsCollider : MonoBehaviour
{
    AudioSource audio;
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) {
            audio.Play();
            PortalGun portal_gun = other.gameObject.transform.Find("Portal Gun").GetComponent<PortalGun>();
            portal_gun.ClearPortals();
        }
    }
}
