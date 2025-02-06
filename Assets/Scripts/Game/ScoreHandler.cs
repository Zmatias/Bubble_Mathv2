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

    private void Awake()
    {
        Instance = this;
    }

    public void AddScoreToTeam(int rawAmount, bool isTeam1)
    {
        if (isTeam1)
        {
            int startScore = t1Score;
            t1Score += BuffHandler.Instance.ApplyScoreBuff(rawAmount, true);
            StartCoroutine(AnimateScore(t1ScoreText, startScore, t1Score));
        }
        else
        {
            int startScore = t2Score;
            t2Score += BuffHandler.Instance.ApplyScoreBuff(rawAmount, false);
            StartCoroutine(AnimateScore(t2ScoreText, startScore, t2Score));
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
