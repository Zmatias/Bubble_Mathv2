using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public static ScoreHandler Instance;

    public TextMeshProUGUI t1ScoreText;
    public TextMeshProUGUI t2ScoreText;
    public int t1Score;
    public int t2Score;

    public float countSpeed = 1.0f;

    public FloatingEffect floatEffect;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScoreToTeam(int rawAmount, bool isTeam1,FloatingEffect effect)
    {
        if (isTeam1)
        {
            int startScore = t1Score;
            t1Score=t1Score+rawAmount;
            StartCoroutine(AnimateScore(t1ScoreText, startScore, t1Score));
            floatEffect=effect;
        }
        else
        {
            int startScore = t2Score;
            t2Score=t2Score+rawAmount;
            StartCoroutine(AnimateScore(t2ScoreText, startScore, t2Score));
            floatEffect=effect;
        }
    }

    private IEnumerator AnimateScore(TextMeshProUGUI scoreText, int startValue, int endValue)
    {
        float elapsedTime = 0f;
        float duration = Mathf.Abs(endValue - startValue) / countSpeed;



        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            int newScore = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, elapsedTime / duration));
            scoreText.text = newScore.ToString();
            yield return null; // Wait for the next frame
        }

        // Ensure the score ends on the exact final value
        scoreText.text = endValue.ToString();
    }
}
