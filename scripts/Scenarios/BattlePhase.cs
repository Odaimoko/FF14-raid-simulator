using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePhase
{
    private SupportedBoss boss;
    // A phase in Scenario 
    public string name;
    public bool chosableInMenu;
    public int code;
    
    public BattlePhase(SupportedBoss boss, string name, bool chosableInMenu)
    {
        this.boss = boss;
        this.name = name;
        this.chosableInMenu = chosableInMenu;
    }

    public override string ToString()
    {
        return $"{boss} Phase: {name}+{chosableInMenu}";
    }

}