using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GlobalGameManager : MonoBehaviour
{
    public string nextSceneToLoad, strat;
    public Constants.GameSystem.SupportedBoss boss = Constants.GameSystem.SupportedBoss.Shiva_Unreal;
    public SinglePlayer.StratPosition playerPos;
    public int phase;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
