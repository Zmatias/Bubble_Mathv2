using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHandler : MonoBehaviour
{
    public WebSocketListener data;
    public string dataReceived;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(WebSocketListener.instance.CurrentQRData=="puzzle_ready_B")
        {
            Debug.Log("B Done");
        }
    }   
}
