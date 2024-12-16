using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    enum Levels {
        ONE = 1,
        TWO = 2,
        THREE = 3,
        FOUR = 4
    }

    [SerializeField] private Levels next_level = Levels.ONE;

    private string GetLevelName(Levels level) {
        return level switch
        {
            Levels.ONE => "Level01",
            Levels.TWO => "Level02",
            Levels.THREE => "Level03",
            Levels.FOUR => "Level04",
            _ => "Level01"
        };
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) {
            SceneManager.LoadScene( GetLevelName(next_level) );
        }
    }




}
