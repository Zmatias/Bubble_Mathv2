using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public static BubbleManager Instance;

    public GameObject mBubblePrefab;
    public Material[] materials;

    private List<Bubble> mAllBubbles = new List<Bubble>();
    private Vector2 mBottomLeft = Vector2.zero;
    private Vector2 mTopRight = Vector2.zero;

    private void Awake()
    {
        // Bounding values
        Instance = this;
        mBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.farClipPlane));
        mTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight / 2, Camera.main.farClipPlane));
    }

    public void StartBubbles()
    {
        StartCoroutine(CreateBubbles());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.farClipPlane)), 0.5f);
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, Camera.main.farClipPlane)), 0.5f);
    }

    public Vector3 GetPlanePosition()
    {
        // Random Position
        float targetX = Random.Range(mBottomLeft.x + 5, mTopRight.x - 5);
        float targetY = -5f;

        return new Vector3(targetX, targetY, 0);
    }

    private IEnumerator CreateBubbles()
    {
        while (mAllBubbles.Count < 40)
        {
            // Create and add
            GameObject newBubbleObject = Instantiate(mBubblePrefab, GetPlanePosition(), Quaternion.identity, transform);
            Bubble newBubble = newBubbleObject.GetComponent<Bubble>();

            // Setup bubble
            SetABubbleColor(newBubble);
            mAllBubbles.Add(newBubble);

            yield return new WaitForSeconds(0.5f);
        }
    }

    public int GenerateUniqueRandomInt(int currentInt)
    {
        int min = 0;
        int max = materials.Length - 1;
        if (max - min <= 1 && currentInt >= min && currentInt <= max)
        {
            Debug.LogError("Range is too small to generate a unique number.");
            return currentInt; // Returning the current number since no unique number is possible
        }

        int newInt;
        do
        {
            newInt = Random.Range(min, max + 1); // Random.Range for int is exclusive of max, so +1 is added
        } while (newInt == currentInt);

        return newInt;
    }

    public void SetABubbleColor(Bubble bubble)  //ADD SOMETHING TO ADJUST THE RATE SO BUBBLE STORM INCREASES ODDS FOR SPAWNS OF THAT TEAM
    {
        int bubbleColor = Random.Range(0, materials.Length);
        bubble.transform.GetChild(2).GetComponent<MeshRenderer>().material = materials[bubbleColor];
        bubble.bubbleColor = bubbleColor;

        /*if (bubbleColor == GameManager.instance.T1Color)
        {
            if (BuffHandler.Instance.CheckIfTeamHasDiminished(false))
                bubble.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            else
                bubble.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (bubbleColor == GameManager.instance.T2Color)
        {
            if (BuffHandler.Instance.CheckIfTeamHasDiminished(true))
                bubble.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            else
                bubble.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            bubble.transform.localScale = new Vector3(1, 1, 1);
        }*/
    }
}


