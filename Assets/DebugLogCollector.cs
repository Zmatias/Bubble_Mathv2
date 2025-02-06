using UnityEngine;

public class DebugLogCollector : MonoBehaviour
{
    // A single string to store logs starting with "Raw data received"
    public string collectedData = "";

    void OnEnable()
    {
        // Subscribe to the Unity log callback
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        // Unsubscribe from the Unity log callback
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Check if the log message starts with "Raw data received"
        if (logString.StartsWith("Raw data received"))
        {
            // Append the log to the collectedData string with a newline
            collectedData += logString + "\n";

            // Optional: Debug to confirm it's being stored
            Debug.Log($"Stored Log: {logString}");
        }
    }

    void Update()
    {
        // You can add logic here to display or process the collected data if needed
    }

    // Public method to retrieve the collected logs
    public string GetCollectedData()
    {
        return collectedData;
    }
}
