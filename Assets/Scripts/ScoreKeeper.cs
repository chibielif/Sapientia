using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    // since we're going to use a percantage system for score keeping, we need to know two things:
    //1. how many correct answers user got 2.how many questions user has answered till that point

    private int correctAnswers = 0;
    private int questionsSeen = 0;

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }

    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }

    // if we do int division 3/4 gives 0, so we need  a float division but we need to return an int as answer
    // so we convert it to an int while also rounding that float into the closest int
    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }
}
