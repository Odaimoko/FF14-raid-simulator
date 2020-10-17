using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanUnrealScenario : Scenario
{
    private GameObject Titan;
    [SerializeField]
 
    public override void Init()
    {
        base.Init();
        Titan = GameObject.Find("Titan");
        controlledPlayer.target = Titan;
    }
 
}
