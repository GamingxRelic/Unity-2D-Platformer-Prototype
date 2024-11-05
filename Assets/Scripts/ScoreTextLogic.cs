using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextLogic : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    public delegate void OnScoreIncrease();
    public static OnScoreIncrease score_increase;
    int score = 0;

    void Start()
    {
        score_increase += UpdateScoreText;
    } 

    void UpdateScoreText() {
        score += 1;
        text.text = "Score: " + score;
    }
}
