using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInteraction : MonoBehaviour
{
    InputManager.Inputs inputs;

    private void Update()
    {
        inputs = ControllerManager.Instance.GetPlayerInputManager(0).GetInputs();
    }

    protected void BackToMenu()
    {
        if (inputs.NorthButtonPressed)
        {
            SceneLoader.Instance.LoadSceneAsync(SceneName.MainMenu);
        }
    }

    protected void Restart()
    {
        if (inputs.WestButtonPressed)
        {
            SceneLoader.Instance.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }    
    
    public void Play(GameModeConfiguration gameModeConfiguration)
    {
        GameManager.Instance.SetSelectedConfiguration(gameModeConfiguration);
        SceneLoader.Instance.LoadSceneAsync(gameModeConfiguration.GameScene);
    }
}
