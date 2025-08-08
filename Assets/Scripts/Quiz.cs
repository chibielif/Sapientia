using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//TODO: randomize answers places
//user can add questions
//questions have categories and user can select which categories they want to have every quiz
public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true; //I spent such time trying to find the error and it was this! I forgot to initialize it!
    //note to self: boolean values are false by default

    [Header("Button Colors")]
    [SerializeField] Sprite wrongAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite defaultAnswerSprite;


    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header(("Scoring"))]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    
    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.interactable = false;
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)//if the player didnt answer the question in time
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

        if (progressBar.value == progressBar.maxValue)
        {
            isComplete = true;
        }
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;
        Image correctButtonImage;
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            questionText.text = "Correct answer is: \n" + currentQuestion.GetAnswer(correctAnswerIndex);
            
            //wrong answer button
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = wrongAnswerSprite; // the answer that user selected is the wrong button
            
            //correct answer button
            correctButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            correctButtonImage.sprite = correctAnswerSprite;
        }
    }

    //getting the new question fresh out of oven
    void GetNextQuestion()
    {
        //check if there are still questions
        if (questions.Any())
        {
            SetButtonState(true); //well, we need to interact with the buttons now, don't we?
            SetDefaultButtonSprites(); //gotta set the sprites from "wrong" or "correct" to "default"
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }

    }

    //getting a random new question every time
    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        //before removing the question, we need to make sure it is in the list
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
        
    }
    //well, this basically displays the question on the screen
    void DisplayQuestion()
    {
        //gets the question text from the question object
        questionText.text = currentQuestion.GetQuestion();

        //a for loop to get all the answers related to the question in hand
        for (int n = 0; n < answerButtons.Length; n++)
        {
            //changing the button text to the answer text
            TextMeshProUGUI buttonText = answerButtons[n].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(n);
        }
    }

    //to change buttons' interactability during the quiz
    //if we're in the "answering" stage, we want interactable = true
    //but if the user already answered the question, we want interactable = false
    void SetButtonState(bool state)
    {
        for (int n = 0; n < answerButtons.Length; n++)
        {
            Button button = answerButtons[n].GetComponent<Button>();
            button.interactable = state;
        }
    }
    
    //at the beginning of a new question, we need to change the sprit3s back to the original
    void SetDefaultButtonSprites()
    {
        Image buttonImage;
        for (int n = 0; n < answerButtons.Length; n++)
        {
            buttonImage = answerButtons[n].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
