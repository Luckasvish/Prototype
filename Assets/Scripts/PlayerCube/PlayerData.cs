using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{

    int playerScore;
    int playerControlledCubes;

    public PlayerData(int playerScore, int playerControlledCubes)
    {
        this.playerScore = playerScore;
        this.playerControlledCubes = playerControlledCubes;
    }

    public void UpdatePlayerScore(int playerScore)
    {
        this.playerScore += playerScore;
    }

    public void UpdatePlayerControlledCubes(int playerControlledCubes)
    {
        this.playerControlledCubes += playerControlledCubes;
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

    public int GetPlayerControlledCubes()
    {
        return playerControlledCubes;
    }
}
