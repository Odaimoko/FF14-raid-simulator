using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShivaUnrealZiyanStrat : Strategy
{
    public ShivaUnrealZiyanStrat() : base()
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
        supportedPhases.Add(new BattlePhase(SupportedBoss.Shiva_Unreal, "", false));
        supportedPhases.Add(new BattlePhase(SupportedBoss.Shiva_Unreal, "剑与杖", true));
        supportedPhases.Add(new BattlePhase(SupportedBoss.Shiva_Unreal, "小怪", false));
        supportedPhases.Add(new BattlePhase(SupportedBoss.Shiva_Unreal, "剑、杖和弓", true));
    }
}
