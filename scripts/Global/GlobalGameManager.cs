using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GlobalGameManager : MonoBehaviour
{
    public string nextSceneToLoad, strat;
    public Constants.GameSystem.SupportedBoss boss = Constants.GameSystem.SupportedBoss.Shiva_Unreal;
    public SinglePlayer.StratPosition playerPos;
    public BattlePhase phase;
    // Start is called before the first frame update
    void Start()
    {
        // SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }
 
    // Update is called once per frame
    void Update()
    {

    }
}
