using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) {
            string current_scene_name = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(current_scene_name);
        }
        
    }
}
