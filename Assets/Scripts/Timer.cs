using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    public bool loadNextQuestion;
    public bool isAnsweringQuestion;

    float timerValue;

    public float fillFraction;

    void Update()
    {

        UpdateTimer();

    }

    //for when user answers early
    public void CancelTimer(){
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        

        if (isAnsweringQuestion) // this is what to do while user is answering question
        {
            if (timerValue > 0) // if user still has time
            {
                fillFraction = timerValue / timeToCompleteQuestion; // this updates how much the image is filled according to time
            }
            else //if user's time runs out
            {
                timerValue = timeToShowCorrectAnswer;
                isAnsweringQuestion = false;
            }
        }
        else //now the user is seeing the correct answer on the screen
        {
            if (timerValue > 0) //user is looking at the correct answer
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else //now it's time to move on to the next question
            {
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
                isAnsweringQuestion = true;
            }
        }
    }
}
