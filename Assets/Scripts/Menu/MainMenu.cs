using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MenuInteraction
{
    public TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        UpdateHighScoreText();
    }

    void UpdateHighScoreText()
    {
        scoreText.text = $"HighScore : {PlayerPrefs.GetInt("HighScore", 0)}";
    }
}
