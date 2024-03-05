using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI playerControlledCubesText;

    [SerializeField] WinLoseInteraction winDefeatInteraction;

    GameObject Win;
    GameObject Defeat;

    int playerIndex;
    [SerializeField] Image playerImage;
    Color playerColor;

    private void Awake()
    {
        winDefeatInteraction.transform.parent = null;
        winDefeatInteraction.transform.position = Vector3.zero;
        Win = winDefeatInteraction.GetWinObject();
        Defeat = winDefeatInteraction.GetLoseObject();

        Win.SetActive(false);
        Defeat.SetActive(false);
    }

    private void Update()
    {
        if (GameplayManager.Instance.gameFinished)
        {
            winDefeatInteraction.UpdateLogic();
        }
    }

    public void SetPlayerIndex(int playerIndex)
    {
        this.playerIndex = playerIndex;
        playerColor = playerIndex == 0 ? Color.yellow : Color.red;
        UpdatePlayerUI();
    }
    
    void UpdatePlayerUI()
    {
        if(playerText != null)
            playerText.text = $"Player {playerIndex+1}";

        if(playerImage != null)
            playerImage.color = playerColor;

        winDefeatInteraction.SetWinText(playerIndex);
    }

    public void ShowLoserScreen()
    {
        Defeat.SetActive(true);
    }

    public void ShowWinnerScreen()
    {
        Win.SetActive(true);
    }

    public void UpdateScoreUI(int playerScore)
    {
        if (scoreText != null)
            scoreText.text = $"Score : {playerScore:D2}";
    }

    public void UpdateCubesUI(int playerControlledCubes)
    {
        if (playerControlledCubesText != null)
            playerControlledCubesText.text = $"ControlledCubes : {playerControlledCubes:D2}";
    }
}
