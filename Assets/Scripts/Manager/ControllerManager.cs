using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public static ControllerManager Instance;
    public List<InputManager> playerInputs = new List<InputManager>();

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Update()
    {
        for (int playerIndex = 0; playerIndex < playerInputs.Count; playerIndex++)
        {
            playerInputs[playerIndex].UpdateLogic(playerIndex);
        }
    }

    public InputManager AddPlayerInputManager()
    {
        var _inputManager = new InputManager();
        playerInputs.Add(_inputManager);
        return _inputManager;
    }

    public void RemovePlayerInputManager(InputManager inputManagerToRemove)
    {
        playerInputs.Remove(inputManagerToRemove);
    }

    public void RemovePlayerInputManager(int playerIndex)
    {
        playerInputs.RemoveAt(playerIndex);
    }

    public InputManager GetPlayerInputManager(int playerIndex)
    {
        return playerInputs[playerIndex];
    }
}
