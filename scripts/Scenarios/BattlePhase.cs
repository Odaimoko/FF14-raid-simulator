using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePhase
{
    public BattlePhase(string name, bool chosableInMenu)
    {
        this.name = name;
        this.chosableInMenu = chosableInMenu;
    }
    // A phase in Scenario 
    public string name;
    public bool chosableInMenu;

    public override string ToString()
    {
        return $"{name}+{chosableInMenu}";
    }
}