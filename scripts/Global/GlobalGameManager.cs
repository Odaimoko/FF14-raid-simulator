using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameManager : MonoBehaviour
{
    public string nextSceneToLoad;
    // set by menu
    public SupportedBoss boss = SupportedBoss.ShivaUnreal;
    public SinglePlayer.StratPosition playerPos;
    public Strategy strategy;
    public int phase;

    public SupportedLang lang = SupportedLang.CHN;
    AsyncOperation loadOperation = null;


    void Start()
    {
#if !UNITY_EDITOR
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
#endif
        Debug.Log($"GGM Start: Active Scene: {SceneManager.GetActiveScene().name}"); // will be global manager scene
    }

    public void LoadScene(Scene prevScene, string sceneName, LoadSceneMode mode = LoadSceneMode.Additive)
    {
        nextSceneToLoad = sceneName;
        Debug.Log($"GGM LoadScene: {sceneName}.");
        // Loading view
        SceneManager.LoadScene("LoadingView", LoadSceneMode.Additive);
        // Unload pre scene
        Debug.Log($"GGM LoadScene: UnLoad {prevScene.name}.");
        SceneManager.UnloadSceneAsync(prevScene);
        // load new scene
        Debug.Log($"GGM LoadScene: Load new Scene {sceneName}.");
        loadOperation = SceneManager.LoadSceneAsync(sceneName, mode);
        loadOperation.allowSceneActivation = false;
        // show progress bar
    }

    private void Update()
    {
        if (loadOperation != null)
        {
            if (loadOperation.isDone)
            {
                // Load scene must be active, unload it
                SceneManager.UnloadSceneAsync(3);
                loadOperation = null;
                Scene nextScene = SceneManager.GetSceneByName(nextSceneToLoad);
                Debug.Log($"GGM Update: Set {nextScene.name} as Active");
                SceneManager.SetActiveScene(nextScene);
                Debug.Log($"GGM Update: Active Scene: {SceneManager.GetActiveScene().name}"); // will be global manager scene
            }
        }
    }
}
