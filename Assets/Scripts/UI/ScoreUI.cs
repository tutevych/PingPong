using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilites;

public class ScoreUI : Singleton<ScoreUI>
{
    public TextMeshProUGUI score;
    private int scoreCount;
    private int bestScore;

    private void OnEnable()
    {
        scoreCount = 0;
        bestScore = PlayerPrefs.GetInt(Constant.bestScore, 0);
        score.text = scoreCount.ToString();
    }

    public void UpdateScore()
    {
        scoreCount++;
        score.text = scoreCount.ToString();
        if (scoreCount > bestScore)
        {
            NewBestScore();
        }
    }

    public void NewBestScore()
    {
        PlayerPrefs.SetInt(Constant.bestScore, scoreCount);
    }
}
