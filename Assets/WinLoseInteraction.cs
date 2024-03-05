using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinLoseInteraction : MenuInteraction
{
    [SerializeField] GameObject Win;
    [SerializeField] TextMeshProUGUI WinText;
    [SerializeField] GameObject Lose;

    internal void UpdateLogic()
    {
        Restart();
        BackToMenu();
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
