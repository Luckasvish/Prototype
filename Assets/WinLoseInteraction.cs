using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinLoseInteraction : MenuInteraction
{
    [SerializeField] GameObject Win;
    [SerializeField] TextMeshProUGUI WinText;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] GameObject Lose;

    internal void UpdateLogic()
    {
        Restart();
        BackToMenu();

    }

    internal void UpdateScoreUI(int playerIndex)
    {
        ScoreText.gameObject.SetActive(true);
        var _scoreText = GameplayManager.Instance.hasAchievedNewHighScore ? "New HighScore" : "Score";
        ScoreText.text = $"{_scoreText} : {GameplayManager.Instance.GetScore(playerIndex)}";
    }

    internal void SetWinText(int playerIndex)
    {
        WinText.text = $"Player {playerIndex+1} Win";
    }

    public GameObject GetLoseObject()
    {
        return Lose;
    }

    public GameObject GetWinObject()
    {
        return Win;
    }
}
