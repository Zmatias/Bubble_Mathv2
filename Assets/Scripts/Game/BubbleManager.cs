using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleManager : MonoBehaviour
{
    public static BubbleManager Instance;

    public GameObject mBubblePrefab;
    public Material[] materials;

    private List<Bubble> mAllBubbles = new List<Bubble>();
    public List<Bubble> leftBubbles = new List<Bubble>(); // ✅ Track left-side bubbles
    public List<Bubble> rightBubbles = new List<Bubble>(); // ✅ Track right-side bubbles

    private Vector2 mBottomLeft = Vector2.zero;
    private Vector2 mTopRight = Vector2.zero;

    public Transform yDivider; // The Transform that splits the screen vertically

    public Transform minX; // ✅ Defines the minimum X for spawning
    public Transform maxX; // ✅ Defines the maximum X for spawning

    private Vector2 leftSpawnMin, leftSpawnMax;
    private Vector2 rightSpawnMin, rightSpawnMax;

    public int leftBubbleCount = 0;  // ✅ Track the number of left bubbles

    public int rightBubbleCount = 0; // ✅ Track the number of right bubbles

    public int team1Color;  // ✅ Color index for Team 2 (right side)
    public int team2Color;  // ✅ Color index for Team 1 (left side)

    public float team1ColorPercentage = 100f; // ✅ 100% means all bubbles match their team color
    public float team2ColorPercentage = 100f; // ✅ 100% means all bubbles match their team color

    public List<Transform> leftSpawnPoints = new List<Transform>();
    public List<Transform> rightSpawnPoints = new List<Transform>();

    private int leftSpawnIndex = 0;  // ✅ Track current left-side spawn index
    private int rightSpawnIndex = 0; // ✅ Track current right-side spawn index
    public Transform leftUIElement; // Assign the left UI target
    public Transform rightUIElement; // Assign the right UI target

    public List<Transform> shrinkT1;

    public List<Transform> shrinkT2;



    private void Awake()
    {
        Instance = this;

        // ✅ Define left and right spawn areas using minX and maxX instead of full screen width
        leftSpawnMin = new Vector2(minX.position.x + 1f, yDivider.position.y); // Add buffer
        leftSpawnMax = new Vector2(yDivider.position.x - 1f, yDivider.position.y); // Stop at divider

        rightSpawnMin = new Vector2(yDivider.position.x + 1f, yDivider.position.y); // Start right after divider
        rightSpawnMax = new Vector2(maxX.position.x - 1f, yDivider.position.y); // Stop at maxX
    }

    public void StartBubbles()
    {
        StartCoroutine(CreateBubbles());
    }

    private IEnumerator CreateBubbles()
    {
        while (mAllBubbles.Count < 40)
        {
            Vector3 spawnPosition;

            // ✅ Ensure an equal number of bubbles on each side
            if (leftBubbleCount <= rightBubbleCount)
            {
                spawnPosition = GetLeftSpawnPosition();
                leftBubbleCount++; // Increase left count
            }
            else
            {
                spawnPosition = GetRightSpawnPosition();
                rightBubbleCount++; // Increase right count
            }

            // ✅ Create and add the bubble
            GameObject newBubbleObject = Instantiate(mBubblePrefab, spawnPosition, Quaternion.identity, transform);
            Bubble newBubble = newBubbleObject.GetComponent<Bubble>();

            // ✅ Track bubbles separately
            if (spawnPosition.x < yDivider.position.x)
            {
                leftBubbles.Add(newBubble);
                newBubble.isLeft=true;
            }
            else
            {
                rightBubbles.Add(newBubble);
                newBubble.isLeft=false;
            }

            // Setup bubble
            SetABubbleColor(newBubble);
            mAllBubbles.Add(newBubble);

            yield return new WaitForSeconds(0.5f);
        }
    }


    private Vector3 GetLeftSpawnPosition()
    {
        Vector3 spawnPosition = leftSpawnPoints[leftSpawnIndex].position; // ✅ Get position from list
        leftSpawnIndex = (leftSpawnIndex + 1) % leftSpawnPoints.Count; // ✅ Cycle through the list
        return spawnPosition;
    }

    private Vector3 GetRightSpawnPosition()
    {
        Vector3 spawnPosition = rightSpawnPoints[rightSpawnIndex].position; // ✅ Get position from list
        rightSpawnIndex = (rightSpawnIndex + 1) % rightSpawnPoints.Count; // ✅ Cycle through the list
        return spawnPosition;
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

     public Vector3 GetPlanePosition()
    {
        // Random Position
        float targetX = Random.Range(mBottomLeft.x + 5, mTopRight.x - 5);
        float targetY = -5f;

        return new Vector3(targetX, targetY, 0);
    }

    
    public void SetABubbleColor(Bubble bubble)
    {
        int assignedColor;
        
        if(leftBubbles.Contains(bubble))
        {
            leftBubbles.Remove(bubble);
        }
        else if(rightBubbles.Contains(bubble))
        {
            rightBubbles.Remove(bubble);
        }

        // Determine if the bubble is on the left or right side
        if (bubble.transform.position.x > yDivider.position.x) // ✅ Left Side (Team 1)
        {
            team2Color=GameManager.instance.getTeamsColor("Left");
            assignedColor = (Random.value * 100f <= team1ColorPercentage) ? team2Color : Random.Range(0, materials.Length);
            leftBubbles.Add(bubble);
            bubble.isLeft=true;
        }
        else // ✅ Right Side (Team 2)
        {
            team2Color=GameManager.instance.getTeamsColor("Right");
            assignedColor = (Random.value * 100f <= team2ColorPercentage) ? team2Color : Random.Range(0, materials.Length);
            rightBubbles.Add(bubble);
            bubble.isLeft=false;
        }

        // ✅ Apply the selected color
        bubble.transform.GetChild(2).GetComponent<MeshRenderer>().material = materials[assignedColor];
        bubble.bubbleColor = assignedColor;
    }

    public void SetABubbleColor(Bubble bubble,int colorTeam)
    {
        bubble.bubbleColor = colorTeam;
        bubble.transform.GetChild(2).GetComponent<MeshRenderer>().material = materials[colorTeam];
    }

    public void SetRandomABubbleColor(Bubble bubble)
    {
        bubble.bubbleColor = Random.Range(0, materials.Length);
        bubble.transform.GetChild(2).GetComponent<MeshRenderer>().material = materials[bubble.bubbleColor];
    }


    public void shrinkBubbles(string ShrinkT1orT2)
    {
        if(ShrinkT1orT2=="T1")
        {
            foreach(Bubble bub in leftBubbles)
            {
                bub.transform.localScale = 0.5f * bub.transform.localScale;
                shrinkT1.Add(bub.transform);
            }
        }
        else
        {
            foreach(Bubble bub in rightBubbles)
            {
                bub.transform.localScale = 0.5f * bub.transform.localScale;
                shrinkT2.Add(bub.transform);
            }
        }

        StartCoroutine(ResetSize(ShrinkT1orT2));
    }

    public void ColorBubbles(string colorBubbleT1orT2,int bubbleToColorT1orT2)
    {
        if(colorBubbleT1orT2=="T1")
        {
            foreach(Bubble bub in rightBubbles)
            {
                SetABubbleColor(bub,bubbleToColorT1orT2);
            }  
        }
        else
        {
            foreach(Bubble bub in leftBubbles)
            {
                SetABubbleColor(bub,bubbleToColorT1orT2);
            }  
        }

                  
        StartCoroutine(ResetColor(colorBubbleT1orT2));
    }

    public IEnumerator ResetSize(string ShrinkT1orT2)
    {
        yield return new WaitForSeconds(5f);

        if(ShrinkT1orT2=="T1")
        {
            foreach(Bubble bub in leftBubbles)
            {
                bub.transform.localScale = new Vector3(1,1,1);
            }
        }
        else
        {
            foreach(Bubble bub in rightBubbles)
            {
                bub.transform.localScale =  new Vector3(1,1,1);
            }
        }
    }

    public IEnumerator ResetColor(string ShrinkT1orT2)
    {
        yield return new WaitForSeconds(5f);

        if(ShrinkT1orT2=="T1")
        {
            foreach(Bubble bub in rightBubbles)
            {
                SetRandomABubbleColor(bub);
            }
        }
        else
        {
            foreach(Bubble bub in leftBubbles)
            {
                SetRandomABubbleColor(bub);
            }
        }
    }

    public Vector3 GetNextSpawnPosition(bool isLeftSide)
    {
        if (isLeftSide)
        {
            // ✅ Get position from left spawn list
            Vector3 spawnPosition = leftSpawnPoints[leftSpawnIndex].position;

            // ✅ Cycle through the list
            leftSpawnIndex = (leftSpawnIndex + 1) % leftSpawnPoints.Count;

            return spawnPosition;
        }
        else
        {
            // ✅ Get position from right spawn list
            Vector3 spawnPosition = rightSpawnPoints[rightSpawnIndex].position;

            // ✅ Cycle through the list
            rightSpawnIndex = (rightSpawnIndex + 1) % rightSpawnPoints.Count;

            return spawnPosition;
        }
    }


 /*     public void SetABubbleColor(Bubble bubble)  //ADD SOMETHING TO ADJUST THE RATE SO BUBBLE STORM INCREASES ODDS FOR SPAWNS OF THAT TEAM
    {
        int bubbleColor = Random.Range(0, materials.Length);
        bubble.transform.GetChild(2).GetComponent<MeshRenderer>().material = materials[bubbleColor];
        bubble.bubbleColor = bubbleColor;


    } */

    
}


