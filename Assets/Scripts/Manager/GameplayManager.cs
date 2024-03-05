using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
class PlayerInfo
{
    public PlayerData playerData;
    public PlayerUI playerUI;
}

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    [SerializeField] GameModeConfiguration modeConfiguration;
    
    List<CubesController> players = new List<CubesController>();
    [SerializeField] GameObject playerPrefab;

    List<PlayerInfo> playerInfo = new List<PlayerInfo>();

    [SerializeField] GameObject playerUIPrefab;
    GameObject instantiatedPlayerUI;
    
    public Action<int> OnDefeat;
    public bool gameFinished = false;

    void Awake()
    {
        Instance = this;
        modeConfiguration = GameManager.Instance.GetSelectedConfiguration();

        Debug.Log($"modeConfiguration : {modeConfiguration}");
        gameFinished = false;
        OnDefeat += Defeat;
        InitGameMode();
    }

    private void OnDisable()
    {
        Instance = null;

        if (modeConfiguration.NumberOfPlayers > 0)
        {
            for (int _playerIndex = 1; _playerIndex < modeConfiguration.NumberOfPlayers; _playerIndex++)
                ControllerManager.Instance.RemovePlayerInputManager(_playerIndex);
        }
    }

    void InitGameMode()
    {
        ClearPlayerInfo();

        var _numOfPlayers = modeConfiguration.NumberOfPlayers;
        Debug.Log($"_numOfPlayers : {_numOfPlayers}");
        for (int _playerIndex = 0; _playerIndex < _numOfPlayers; _playerIndex++)
        {
            instantiatedPlayerUI = Instantiate(playerUIPrefab, gameObject.transform);
            players.Add(Instantiate(playerPrefab).GetComponent<CubesController>());

            if (_playerIndex > 0)
                ControllerManager.Instance.AddPlayerInputManager();

            playerInfo.Add(new PlayerInfo());
            playerInfo[_playerIndex].playerData = new PlayerData(0,0);
            playerInfo[_playerIndex].playerUI = instantiatedPlayerUI.GetComponent<PlayerUI>();
            playerInfo[_playerIndex].playerUI.SetPlayerIndex(_playerIndex);


            players[_playerIndex].SetPlayerIndex(_playerIndex);
        }
            Debug.Log("SAD");
    }

    void ClearPlayerInfo()
    {
        players.Clear();     
        playerInfo.Clear();
    }

    public void CheckVictoryCondition(int playerIndex)
    {
        if(playerInfo[playerIndex].playerData.GetPlayerControlledCubes() >= modeConfiguration.CubesLimit)
        {
            if(modeConfiguration.CanUseWinScore() && playerInfo[playerIndex].playerData.GetPlayerScore() >= modeConfiguration.MinScoreToWin)
            {
                Win(playerIndex);
            }
        }
    }

    void Win(int playerIndex)
    {
        gameFinished = true;
        playerInfo[playerIndex].playerUI.ShowWinnerScreen();
    }

    void Defeat(int playerIndex)
    {
        gameFinished = true;
        playerInfo[playerIndex].playerUI.ShowLoserScreen();
    }

    public bool HasCubesLimit()
    {
        return modeConfiguration.HasCubesLimit;
    }    
    
    public int GetCubesLimit()
    {
        return modeConfiguration.CubesLimit;
    }

    /// <summary>
    /// Add or subtract cubes counter and update UI
    /// </summary>
    /// <param name="playerIndex"></param>
    public void UpdateCubesNum(int playerIndex)
    {
        playerInfo[playerIndex].playerData.UpdatePlayerControlledCubes(+1);
        playerInfo[playerIndex].playerUI.UpdateCubesUI(playerInfo[playerIndex].playerData.GetPlayerControlledCubes());
    }

    /// <summary>
    /// Add or subtract score counter and update UI
    /// </summary>
    /// <param name="playerIndex"></param>
    public void UpdateScore(int playerIndex)
    {
        playerInfo[playerIndex].playerData.UpdatePlayerScore(modeConfiguration.ScoreToSum);
        playerInfo[playerIndex].playerUI.UpdateScoreUI(playerInfo[playerIndex].playerData.GetPlayerScore());
    }


}