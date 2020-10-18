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
        supportedPhases.Add(new BattlePhase(SupportedBoss.Titan_Unreal, "在？", false));
        supportedPhases.Add(new BattlePhase(SupportedBoss.Titan_Unreal, "啊？", true));
        supportedPhases.Add(new BattlePhase(SupportedBoss.Titan_Unreal, "吧？", false));
        supportedPhases.Add(new BattlePhase(SupportedBoss.Titan_Unreal, "从？", true));
    }
}
