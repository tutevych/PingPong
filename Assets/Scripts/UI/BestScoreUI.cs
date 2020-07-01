using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScoreUI : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    private int bestScoreCount;

    private void OnEnable()
    {
        bestScoreCount = PlayerPrefs.GetInt(Constant.bestScore, 0);
        bestScoreText.text = bestScoreCount.ToString();
    }
}
