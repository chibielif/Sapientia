using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;
    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void ShowFinalScore()
    {
        finalScoreText.text = "Congratulations!\nYou scored" +  scoreKeeper.CalculateScore() + "%";
    }
    
}
