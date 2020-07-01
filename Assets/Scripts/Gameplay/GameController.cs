using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilites;

public class GameController : Singleton<GameController>
{
    public Ball ball;
    public GameObject scoreUI;
    public GameObject menuUI;
    public MoveRacket[] rockets;
    public void Play()
    {
        scoreUI.SetActive(true);
        ball.gameObject.SetActive(true);
    }

    public void Lose()
    {
        foreach (var item in rockets)
        {
            item.SetStartPosition();
        }
        scoreUI.SetActive(false);
        ball.gameObject.SetActive(false);
        ball.Restart();
        menuUI.SetActive(true);
    }
}
