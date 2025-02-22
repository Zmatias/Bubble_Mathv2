using UnityEngine;
using WebSocketSharp;
using TMPro;
using System.Collections;

public class WebSocketListener : MonoBehaviour
{
    private WebSocket ws;
    public string CurrentQRData;  // Stores the last received QR data

    public string text;
    public TextMeshProUGUI debubQR;
    public static WebSocketListener instance;

    public bool PuzzleAReady;
    public bool PuzzleBReady;

    public bool CheckMode;

    public GameObject PuzzleADone;
    public GameObject PuzzleBDone;
   
    void Start()
    {

        // Connect to the WebSocket server
        ws = new WebSocket("ws://127.0.0.1:5000/ws");

        // Define the behavior when a message is received
        ws.OnMessage += (sender, e) =>
        {
            string newQRData = e.Data;  // Get the new QR code data
            Debug.Log($"Raw data received: {newQRData}");

            // Compare the new QR data with the current one or check the timer
            if (!string.Equals(CurrentQRData, newQRData))
            {
                // Update the current QR data and reset the timer
                CurrentQRData = newQRData;

                Debug.Log($"New QR Code Detected: {CurrentQRData}");
            }

            
        };

        // Define what happens when the connection is established
        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket connected");
        };

        // Define what happens when the connection is closed
        ws.OnClose += (sender, e) =>
        {
            Debug.Log("WebSocket disconnected");
        };

        // Start the connection
        ws.Connect();
    }
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {


        debubQR.text = CurrentQRData;
        //Debug.Log(CurrentQRData);


        if(CurrentQRData=="puzzle_ready_A")
        {
            PuzzleReadyA();
        }
        else if(CurrentQRData=="puzzle_ready_B")
        {
            PuzzleReadyB();
        }
    }

    public void PuzzleReadyA()
    {
        if(!PuzzleAReady)
        {
            ScoreHandler.Instance.AddScoreToTeam(500,true,null);
            PuzzleADone.gameObject.SetActive(true);
            //Do something
            PuzzleAReady=true;
            CheckMode=false;
        }
    }

    public IEnumerator restartCheck()
    {
        yield return new WaitForSeconds(5.5f);
        CheckMode=false;
    }

    public void CheckAnimation()
    {

    }

    public void PuzzleReadyB()
    {
        if(!PuzzleBReady)
        {
            ScoreHandler.Instance.AddScoreToTeam(500,false,null);
            PuzzleBDone.gameObject.SetActive(true);
            //Do something
            PuzzleBReady=true;
            CheckMode=false;
        }
    }

    void OnDestroy()
    {
        // Close the WebSocket connection when the GameObject is destroyed
        if (ws != null && ws.IsAlive)
        {
            ws.Close();
        }
    }
}
