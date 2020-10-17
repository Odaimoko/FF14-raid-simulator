using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanUnrealZiyanStrat : Strategy
{
    public TitanUnrealZiyanStrat() : base()
    {
    }

    public override string name { get => "子言"; }
    // Update is called once per frame
    void Update()
    {

    }

    protected override void InitPhases()
    {
        base.InitPhases();
        supportedPhases.Add(new BattlePhase("在？", false));
        supportedPhases.Add(new BattlePhase("啊？", true));
        supportedPhases.Add(new BattlePhase("吧？", false));
        supportedPhases.Add(new BattlePhase("从？", true));
    }
}
