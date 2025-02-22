using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Bubble))] // ✅ This makes the editor apply to the Bubble class
public class BubbleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw default Inspector
        DrawDefaultInspector();

        // Reference to the target script
        Bubble bubble = (Bubble)target;

        // Add a button to the Inspector
        if (GUILayout.Button("Pop Bubble"))
        {
            // Start the Pop Coroutine if the game is playing
            if (Application.isPlaying)
            {
                bubble.StartCoroutine(bubble.Pop());
            }
            else
            {
                Debug.LogWarning("Cannot pop the bubble in Edit mode! Start the game first.");
            }
        }
    }
}
