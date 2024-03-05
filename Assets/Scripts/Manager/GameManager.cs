using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    static GameModeConfiguration selectedConfiguration;

    ControllerManager controllerManager;
    [SerializeField] GameObject controllerManagerPrefab;
    
    SceneLoader sceneLoader;
    [SerializeField] GameObject sceneLoaderPrefab; 


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        const string prefabPath = "Manager/GameManager";

        if (Instance)
            return;

        GameManager gameManagerPrefab = Resources.Load<GameManager>(prefabPath);

        if (gameManagerPrefab == null)
            throw new Exception($"[Platform Manager] Platform manager prefab not found at {prefabPath}");

        Instance = Instantiate(gameManagerPrefab);
        Instance.gameObject.SetActive(true);
        DontDestroyOnLoad(Instance.gameObject);
        Instance.sceneLoader = Instantiate(Instance.sceneLoaderPrefab).GetComponent<SceneLoader>();
        Instance.controllerManager = Instantiate(Instance.controllerManagerPrefab).GetComponent<ControllerManager>();
        Instance.controllerManager.AddPlayerInputManager();
        Instance.Invoke("LoadScene", 1f);
    }

    void LoadScene()
    {
        SceneLoader.Instance.LoadSceneAsync(SceneName.MainMenu);
    }

    public void SetSelectedConfiguration(GameModeConfiguration newGameModeConfiguration)
    {
        selectedConfiguration = newGameModeConfiguration;
        Debug.Log($"selectedConfiguration : {selectedConfiguration}");
    }

    public GameModeConfiguration GetSelectedConfiguration()
    {
        return selectedConfiguration;
    }
}