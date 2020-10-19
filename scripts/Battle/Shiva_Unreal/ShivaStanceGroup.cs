using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShivaStanceGroup : StatusGroup
{
    private bool castFinished = false;
    private SingleStatus timer, _actual;
    public SingleStatus actual { get => _actual; }
    private BaseStance bow, sword, wand, none;
    public ShivaStanceGroup(GameObject from, GameObject target) :
        base(from, target)
    {
        name = "ShivaStanceGroup";
        bow = new ShivaStanceBow(from, target);
        sword = new ShivaStanceSword(from, target);
        wand = new ShivaStanceWand(from, target);
        none = new ShivaStanceNone(from,target);
    }

    

    private void OnStanceChange()
    {

    }
}
