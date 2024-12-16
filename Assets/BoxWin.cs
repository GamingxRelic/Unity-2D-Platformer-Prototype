using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWin : MonoBehaviour
{
    bool activated = false;
    [SerializeField] private GameObject exit;
    AudioSource audio;

    void Start()
    {
        exit.SetActive(false);
        audio = gameObject.GetComponent<AudioSource>();
    }
   void OnTriggerEnter2D(Collider2D other)
   {
        if(other.gameObject.CompareTag("CompanionCube") && !activated) {
            audio.Play();
            Destroy(other.gameObject);
            exit.SetActive(true);
        }
   }
}
