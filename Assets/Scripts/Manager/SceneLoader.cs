using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum SceneName { Logo, MainMenu, Game }

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    float loadingProgress = 0;
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] Image progressBar;

    public void Start()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
            Destroy(this);

        LoadingScreen.SetActive(false);
    }

    #region Load with static loading screen 


    public void LoadSceneAsync(SceneName sceneName)
    {
        GameManager.Instance.StartCoroutine(LoadAsync(sceneName.ToString()));
    }
    public void LoadSceneAsync(string sceneName)
    { Debug.Log($"GameManager.Instance : {GameManager.Instance}");
        GameManager.Instance.StartCoroutine(LoadAsync(sceneName));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        StartLoadingScreen();
        yield return new WaitForSeconds(2f);

        var _asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!_asyncLoad.isDone)
        {
            yield return null;
        }
        FinishLoadingScreen();
        yield return new WaitForSeconds(1f);
        _asyncLoad.allowSceneActivation = true;
    }

    /// <summary>
    /// Load scene async with static loading screen
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="callback"></param>
    public void LoadScene(SceneName sceneName, System.Action callback)
    {
        callback += FinishLoadingScreen;
        GameManager.Instance.StartCoroutine(AsyncLoadNoTransition(sceneName.ToString(), callback));
    }    
    
    public void LoadScene(string sceneName, System.Action callback)
    {
        callback += FinishLoadingScreen;
        GameManager.Instance.StartCoroutine(AsyncLoadNoTransition(sceneName, callback));
    }


    IEnumerator AsyncLoadNoTransition(string sceneName, System.Action callback)
    {
        StartLoadingScreen();
        yield return new WaitForSeconds(1f);

        var _asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!_asyncLoad.isDone)
        {
            yield return null;
        }

        callback?.Invoke();

        _asyncLoad.allowSceneActivation = true;
    }
    #endregion

    #region Load with loading screen transition 
    /// <summary>
    /// Load scene async with transition and progress bar
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="callback"></param>
    public void LoadSceneWithTransition(SceneName sceneName, System.Action callback)
    {
        GameManager.Instance.StartCoroutine(AsyncLoadWithTransition(sceneName.ToString(), callback));
        callback += FinishLoadingScreen;
    }    
    
    public void LoadSceneWithTransition(string sceneName, System.Action callback)
    {
        GameManager.Instance.StartCoroutine(AsyncLoadWithTransition(sceneName, callback));
        callback += FinishLoadingScreen;
    }

    void StartLoadingScreen()
    {
        loadingProgress = 0;
        LoadingScreen.SetActive(true);
        UpdateLoadingScreen();
    }    
    
    void UpdateLoadingScreen()
    {
        //progressBar.fillAmount = loadingProgress / 100;
    }

    void FinishLoadingScreen()
    {
        UpdateLoadingScreen();
        LoadingScreen.SetActive(false);
    }


    IEnumerator AsyncLoadWithTransition(string sceneName, System.Action callback)
    {
        StartLoadingScreen();
        yield return new WaitForSeconds(1f);

        var _asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!_asyncLoad.isDone)
        {
            UpdateLoadingScreen();
            loadingProgress = _asyncLoad.progress;
            yield return null;
        }
        
        yield return new WaitForSeconds(.5f);
        callback?.Invoke();
        yield return new WaitForSeconds(.5f);

        _asyncLoad.allowSceneActivation = true;
    }    

    #endregion
}