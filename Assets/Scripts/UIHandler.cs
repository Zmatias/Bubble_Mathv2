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
    public Image RedFrost;
    public Image BlueFrost;

    [Header("Rest")]

    public Transform SwapPauseTransparent;

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

    public void updateBlueScore(string text)
    {
        BlueScore.text=text;
    }

    public void updateRedScore(string text)
    {
        RedScore.text=text;
    }

    public void updateFrostRed(Color col)
    {
        RedFrost.color=col;
    }

    public void updateFrostBlue(Color col)
    {
        BlueFrost.color=col;
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
