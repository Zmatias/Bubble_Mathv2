using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public static QuizManager instance;

    public TextAsset csvFile;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        LoadQuestionsFromTextAsset(csvFile);
    }
    [System.Serializable]
    public class Question
    {
        public string Equation;
        public int CorrectAnswer;
        public int[] Answers;

        public Question(string equation, int correctAnswer, int[] answers)
        {
            Equation = equation;
            CorrectAnswer = correctAnswer;
            Answers = answers;
        }
    }

    public List<Question> Questions = new List<Question>();
    public int t1currentQuestionIndex = -1;
    public int t2currentQuestionIndex = -1;


    public GameObject[] redT1;
    public GameObject[] greenT1;
    public GameObject[] redT2;
    public GameObject[] greenT2;
    public void StartQuiz()
    {
        GetNextQuestion(true);
        GetNextQuestion(false);
    }

    public void GetNextQuestion(bool isTeam1)
    {
        if (isTeam1)
        {
            t1currentQuestionIndex++;
            UIHandler.instance.RedQuiz.Question.text = Questions[t1currentQuestionIndex].Equation;
            UIHandler.instance.RedQuiz.Answer1.text = Questions[t1currentQuestionIndex].Answers[0].ToString();
            UIHandler.instance.RedQuiz.Answer2.text = Questions[t1currentQuestionIndex].Answers[1].ToString();
            UIHandler.instance.RedQuiz.Answer3.text = Questions[t1currentQuestionIndex].Answers[2].ToString();
            UIHandler.instance.RedQuiz.Answer4.text = Questions[t1currentQuestionIndex].Answers[3].ToString();
        }
        else
        {
            t2currentQuestionIndex++;
            UIHandler.instance.BlueQuiz.Question.text = Questions[t2currentQuestionIndex].Equation;
            UIHandler.instance.BlueQuiz.Answer1.text = Questions[t2currentQuestionIndex].Answers[0].ToString();
            UIHandler.instance.BlueQuiz.Answer2.text = Questions[t2currentQuestionIndex].Answers[1].ToString();
            UIHandler.instance.BlueQuiz.Answer3.text = Questions[t2currentQuestionIndex].Answers[2].ToString();
            UIHandler.instance.BlueQuiz.Answer4.text = Questions[t2currentQuestionIndex].Answers[3].ToString();
        }
    }

    public void CheckIfCorrectAnswer(int choiceId, bool isTeam1)
    {
        Debug.Log("Before If");
        if (isTeam1)
        {
            Debug.Log("After If1");
            if (Questions[t1currentQuestionIndex].CorrectAnswer == Questions[t1currentQuestionIndex].Answers[choiceId])
            {
                Debug.Log("Check Correct");
                BuffHandler.Instance.AddRandomBuffToTeam(true);
                GetNextQuestion(isTeam1);
                greenT1[choiceId].SetActive(true);
                StartCoroutine(deactivateAnswer(greenT1[choiceId]));
            }
            else
            {
                redT1[choiceId].SetActive(true);
                StartCoroutine(deactivateAnswer(redT1[choiceId]));
            }
            
        }
        else
        {
            Debug.Log("After If2");
            if (Questions[t2currentQuestionIndex].CorrectAnswer == Questions[t2currentQuestionIndex].Answers[choiceId])
            {
                Debug.Log("Check Correct2");
                BuffHandler.Instance.AddRandomBuffToTeam(false);
                GetNextQuestion(isTeam1);
                greenT2[choiceId].SetActive(true);
                StartCoroutine(deactivateAnswer(greenT2[choiceId]));
                //
            }
            else
            {
                redT2[choiceId].SetActive(true);
                StartCoroutine(deactivateAnswer(redT2[choiceId]));
            }
        }
    }

    private IEnumerator deactivateAnswer(GameObject obj)
    {
        yield return new WaitForSeconds(2f);

        obj.SetActive(false);
    }

    public void LoadQuestionsFromTextAsset(TextAsset csvFile)
    {
        if (csvFile == null)
        {
            Debug.LogError("CSV file is null!");
            return;
        }

        string[] lines = csvFile.text.Split('\n'); // Split the file content into lines

        // Skip the header row
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] values = line.Split(','); // Assuming tab-separated values

            if (values.Length < 5)
            {
                Debug.LogError("Malformed CSV line: " + line);
                continue;
            }

            string equation = values[0];
            int correctAnswer = int.Parse(values[1]);
            int wrong1 = int.Parse(values[2]);
            int wrong2 = int.Parse(values[3]);
            int wrong3 = int.Parse(values[4]);

            int[] answers = new int[] { correctAnswer, wrong1, wrong2, wrong3 };
            ShuffleArray(answers); // Randomize the answer order

            Questions.Add(new Question(equation, correctAnswer, answers));
        }

        Debug.Log("Questions loaded successfully from TextAsset.");
    }

    private void ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

}
