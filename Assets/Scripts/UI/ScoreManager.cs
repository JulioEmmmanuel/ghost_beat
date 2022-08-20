using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager: MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField]
    private Text scoreText;

    private int score;

    public int Score
    {
        get
        {
            return score;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateScore(int addedScore)
    {
        if (scoreText != null)
        {
            score += addedScore;
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
