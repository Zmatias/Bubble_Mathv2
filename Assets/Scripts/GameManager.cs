using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Time Settings")]
    public int startTimeInSeconds = 300; // Starting time in seconds (default is 5 minutes)
    public int timeBetweenSwaps = 60; // Time between swaps in seconds

    public float remainingTime;
    public bool isTimerRunning = false;
    public float swapTimer;

    public float logTimer = 1f; // Time interval for logging Readata
    public float logTimeElapsed = 0f; // Tracks elapsed time for logging

    [Header("Teams")]

    public int T1Color = 1;
    public int T2Color = 2;

    public int ChangeDuration = 3;

    public TextMeshProUGUI timerText; // Optional: UI Text to display the countdown timer

    [Header("Quiz Checker and QR")]

    public int TimeBetweenReads;

    string readData;

    public GameObject SwapBoxMenu;

    public string previousScan;

    public Color[] TeamCOlors;

    public Image Team1;

    public Image Team2;

    public float GameSpeed;
    public int multipleScoreT1=1; 
    public int multipleScoreT2=1; 
    public int MainAddScore=10;

    public string WinnerIs;

    private void Awake()
    {
        GameSpeed=1;
        instance = this;
    }

    void Start()
    {
        swapTimer = timeBetweenSwaps;
    }

    public int getTeamsColor(string LeftOrRight)
    {
        if(LeftOrRight=="Right")
        {
            return T1Color;
        }
        else
        {
            return T2Color;
        }
    }

    void Update()
    {
        Time.timeScale=GameSpeed;
        if (isTimerRunning)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                swapTimer -= Time.deltaTime;
                logTimeElapsed += Time.deltaTime;

                if (swapTimer <= 0)
                {
                    Swap();
                    swapTimer = timeBetweenSwaps; // Reset the swap timer
                }

                if (logTimeElapsed >= logTimer)
                {
                    checkAnswers();
                    logTimeElapsed = 0f; // Reset log timer
                }

                UpdateTimerDisplay(remainingTime);
            }
            else
            {
                remainingTime = 0;
                isTimerRunning = false;
                TimerOut();
            }
        }
    }

    public void checkAnswers()
    {
        

       
        Debug.Log("Check Answers");
        bool isTeam1 = true;
        string readData = WebSocketListener.instance.CurrentQRData;

        if (previousScan == readData)
        {
            return;
        }

        if (readData.Length>=2)
        {
            Debug.Log("Check String size");
            var team = readData[0];
            var choice = readData[1];

            if (team != '1')
            {
                isTeam1 = false;
            }

            int i = -1;
            if(choice=='A')
            {
                i = 0;
            }
            else if (choice == 'B')
            {
                i = 1;
            }
            else if (choice == 'C')
            {
                i = 2;
            }
            else if (choice == 'D')
            {
                i = 3;
            }

            if (i>=0 && i<4)
            {
                Debug.Log("Check String size 2");
                GetComponent<AudioSource>().Play();
                QuizManager.instance.CheckIfCorrectAnswer(i,isTeam1);
                previousScan = readData;
            }
            
        }
        

        // Alan call methods
        // When scanned Debuf Ends Clear data , suppose that card is not on the camera



    }

    public void clearScanData()
    {
        readData = "";
        WebSocketListener.instance.CurrentQRData = readData;
    }

    public void CheckAndScore(int bubbleColor,Bubble bubble,string LeftOrRigh)
    {
        if (bubbleColor == T1Color && LeftOrRigh=="Left")
        {
            var scoreToAddRaw = multipleScoreT1*MainAddScore;
            bubble.SpawnFloatingEffect(ScoreHandler.Instance.t1Score+scoreToAddRaw,true);
            //ScoreHandler.Instance.AddScoreToTeam(multipleScore*MainAddScore, true);            
        }
        else if (bubbleColor == T2Color && LeftOrRigh=="Right")
        {
            var scoreToAddRaw = multipleScoreT2*MainAddScore;
            bubble.SpawnFloatingEffect(ScoreHandler.Instance.t2Score+scoreToAddRaw,false);
            //ScoreHandler.Instance.AddScoreToTeam(multipleScore*MainAddScore, false);            
        }
    }

    public void changeColorOfTeams()
    {

    }

    public void StartTimer(int seconds)
    {
        remainingTime = seconds;
        isTimerRunning = true;
        swapTimer = timeBetweenSwaps; // Initialize swap timer
    }

    void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timerText != null)
        {
            timerText.text = formattedTime;
        }
        else
        {
            Debug.Log(formattedTime); // Logs time to the console if no UI Text is provided
        }
    }

    void TimerOut()
    {
        isTimerRunning = false;
        timerText.text = "00:00";
        MenuManager.Instance.Gameover();

        if(ScoreHandler.Instance.t1Score>ScoreHandler.Instance.t2Score)
        {
            WinnerIs="Left";
        }
        else
        {
            WinnerIs="Right";
        }
        Debug.Log("Timer is out!");
    }

    void Swap()
    {
        SwapBoxMenu.SetActive(true);
        StartCoroutine(WaitAnimationSwap());
    }

    IEnumerator WaitAnimationSwap()
    {
        BodySourceView.instance.DisableHandColliders();
        yield return new WaitForSeconds(2.5f);

        Debug.Log("Swap");

        

        StartCoroutine(unpauseGame());
    }

    IEnumerator unpauseGame()
    {
        yield return new WaitForSecondsRealtime(ChangeDuration);


        SwapBoxMenu.transform.GetChild(0).gameObject.SetActive(false);

        

        // Get the current team colors
        int team1 = T1Color;
        int team2 = T2Color;

        // Ensure new colors are valid and do not overlap
        int newT1Color = BubbleManager.Instance.GenerateUniqueRandomInt(team1);
        int newT2Color;

        do
        {
            newT2Color = BubbleManager.Instance.GenerateUniqueRandomInt(team2);
        } while (newT2Color == newT1Color); // ✅ Prevent the two teams from having the same color

        // Apply the new colors
        T1Color = newT1Color;
        T2Color = newT2Color;

        // Ensure color indices are within valid range
        if (T1Color >= 0 && T1Color < TeamCOlors.Length)
            Team1.color = TeamCOlors[T1Color];

        if (T2Color >= 0 && T2Color < TeamCOlors.Length)
            Team2.color = TeamCOlors[T2Color];

        Debug.Log($"New Colors - Team1: {T1Color}, Team2: {T2Color}");

        //Start Animation Change Colors
        UIHandler.instance.T1NewColor.Find("TeamColor").GetComponent<Image>().color = TeamCOlors[T1Color];
        UIHandler.instance.T1NewColor.gameObject.SetActive(true);

        UIHandler.instance.T2NewColor.Find("TeamColor").GetComponent<Image>().color = TeamCOlors[T2Color];
        UIHandler.instance.T2NewColor.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(2);

        UIHandler.instance.T1NewColor.gameObject.SetActive(false);
        UIHandler.instance.T2NewColor.gameObject.SetActive(false);

        SwapBoxMenu.SetActive(false);
        SwapBoxMenu.transform.GetChild(0).gameObject.SetActive(true);

        Time.timeScale = 1;

        BodySourceView.instance.EnableHandColliders();
    }
}


