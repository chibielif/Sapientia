using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndScreen endScreen;
    
    void Start()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
        
        //at the beginning of the game we want to see the quiz screen and not the end screen
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }


    void Update()
    {
        //if the game is complete (we check that with the bool from the quiz) then we want to activate the end screen
        if (quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
        }
    }
}
