using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "ModeConfig")]
public class GameModeConfiguration : ScriptableObject
{
    [Header("Scene Config")]
    public SceneName GameScene;

    [Space(10)]
    [Header("Player Config")]
    public bool IsSinglePlayer;
    [Range(1,2)]
    public int NumberOfPlayers = 1;

    [Space(10)]
    [Header("Game Config")]


    [Space(5)]
    [Header("Cubes Config")]
    public int MinCubesToWin;
    [Tooltip("Define If The Game Mode Will Have Cube Limit Or Not")]
    public bool HasCubesLimit;
    [Tooltip("Will Only Be Used if HasCubesLimit is true")]
    public int CubesLimit;

    [Space(5)]
    [Header("Score Config")]
    public int MinScoreToWin;
    [Tooltip("Value Of Any Game Pontuation")]
    public int ScoreToSum = 1;

    public bool CanUseWinScore()
    {
        return HasCubesLimit;
    }
}