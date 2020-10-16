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
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
