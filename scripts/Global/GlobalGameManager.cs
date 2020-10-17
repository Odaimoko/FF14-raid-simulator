using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameManager : MonoBehaviour
{
    public string nextSceneToLoad, strat;
    public SupportedBoss boss = SupportedBoss.Shiva_Unreal;
    public SinglePlayer.StratPosition playerPos;
    public BattlePhase phase;
    public SupportedLang lang = SupportedLang.CHN;
    AsyncOperation loadOperation = null;


    void Start()
    {
#if !UNITY_EDITOR
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
#endif
        Debug.Log($"GlobalGameManager Start: Active Scene: {SceneManager.GetActiveScene().name}"); // will be global manager scene
    }

    public void LoadScene(Scene prevScene, string sceneName, LoadSceneMode mode = LoadSceneMode.Additive)
    {
        nextSceneToLoad = sceneName;
        Debug.Log($"GlobalGameManager LoadScene: {sceneName}.");
        // Loading view
        SceneManager.LoadScene("LoadingView", LoadSceneMode.Additive);
        // Unload pre scene
        Debug.Log($"GlobalGameManager LoadScene: UnLoad {prevScene.name}.");
        SceneManager.UnloadSceneAsync(prevScene);
        // load new scene
        Debug.Log($"GlobalGameManager LoadScene: Load new Scene {sceneName}.");
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
            }
        }
    }
}
