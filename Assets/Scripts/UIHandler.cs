using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance;
    // Start is called before the first frame update
    [Header("Text Scores")]
    public TextMeshProUGUI BlueScore;
    public TextMeshProUGUI RedScore;

    [Header("Questions")]
    public QuizUiElements RedQuiz;
    public QuizUiElements BlueQuiz;

    [Header("Frost")]
    public Image Team1Frost;
    public Image Team2Frost;

    [Header("Rest")]

    public Transform SwapPauseTransparent;

    [Header("PowerUps")]

    public Image T1PowerUp;
    public Image T2PowerUp;

    [Header("PowerUps Ico")]

    public Sprite Superchage;
    public Sprite BubbleStorm;
    public Sprite Dimminished;

    [Header("Team New Colors")]

    public Transform T1NewColor;
    public Transform T2NewColor;

    private void Awake() {
        instance=this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetT1PowerUpIco(string powerUp)
    {
        if(powerUp=="Supercharge")
        {
            T1PowerUp.sprite=Superchage;
        }
        else if(powerUp=="Diminished")
        {
            T1PowerUp.sprite=Dimminished;
        }
        else
        {
            T1PowerUp.sprite=BubbleStorm;
        }
    }

    public void SetT2PowerUpIco(string powerUp)
    {
        if(powerUp=="Supercharge")
        {
            T2PowerUp.sprite=Superchage;
        }
        else if(powerUp=="Diminished")
        {
            T2PowerUp.sprite=Dimminished;
        }
        else
        {
            T2PowerUp.sprite=BubbleStorm;
        }
    }

    public void updateBlueScore(string text)
    {
        BlueScore.text=text;
    }

    public void updateRedScore(string text)
    {
        RedScore.text=text;
    }

    [Serializable]
    public class QuizUiElements
    {
        public TextMeshProUGUI Question;
        [Header("Answers")]
        public TextMeshProUGUI Answer1;
        public TextMeshProUGUI Answer2;
        public TextMeshProUGUI Answer3;
        public TextMeshProUGUI Answer4;

        public void updateQuestion(string newQuestion)
        {
            Question.text=newQuestion;
        }

        public void updateAnswers(int[] newAnswers)
        {
            Answer1.text=newAnswers[0].ToString();
            Answer2.text=newAnswers[2].ToString();
            Answer3.text=newAnswers[3].ToString();
            Answer4.text=newAnswers[4].ToString();
        }

    }
}
