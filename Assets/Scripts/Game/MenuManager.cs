using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public GameObject startMenu;
    public GameObject gameoverMenu;
    public GameObject readyToBubbleObject;

    public bool Gamestarted;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Play bubble start animation of loading
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if(Gamestarted)
            {
                return;
            }
            StartGame();
            GameManager.instance.StartTimer(GameManager.instance.startTimeInSeconds);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void StartGame()
    {
        GetComponent<AudioSource>().Play();
        /*Start Gameplay (bubble spawns, close menu UI, Initialize Quiz, Voiceover,*/
        readyToBubbleObject.SetActive(true);
        BubbleManager.Instance.StartBubbles();
        startMenu.SetActive(false);
        QuizManager.instance.StartQuiz();
    }

    public void Gameover()
    {


        Destroy(BubbleManager.Instance);
        gameoverMenu.SetActive(true);
    }
}
