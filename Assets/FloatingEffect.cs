using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingEffect : MonoBehaviour
{
    public Transform targetUI; // The UI element this will move to
    public float moveSpeed = 0.05f; // Speed of movement
    public float delayBeforeMove = 0.5f; // Wait time before moving
    public float destroyThreshold = 0.5f; // Distance before destroying

    private bool shouldMove = false;

    public bool isTeam1;
    
    public int RawAmount;

    public AudioSource audiosource;

    public bool RunOnce=true;

    void Start()
    {
        // Ensure it always faces the camera
        transform.LookAt(Camera.main.transform);
        StartCoroutine(StartMovingAfterDelay());
    }

    private IEnumerator StartMovingAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeMove);
        shouldMove = true;
    }

    void Update()
    {
        if (shouldMove && targetUI != null & RunOnce)
        {
            // Move towards the UI element
            transform.position = Vector3.Lerp(transform.position, targetUI.position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.identity;

            // Destroy when close enough
            if (Vector3.Distance(transform.position, targetUI.position) < destroyThreshold)
            {
                //AddScores Effect

                ScoreHandler.Instance.AddScoreToTeam(RawAmount,isTeam1,this);
                audiosource=targetUI.GetComponent<AudioSource>();
                audiosource.enabled=true;
                audiosource.Play();

                StartCoroutine(DestroyAndStop());
                RunOnce=false;
            }
        }
    }
    public IEnumerator DestroyAndStop()
    {
        yield return new WaitForSeconds(2.5f);
        audiosource.Stop();
        audiosource.enabled=false;

        DestroyElemet();
    }
    public void DestroyElemet()
    {
        Destroy(gameObject);
    }
}
