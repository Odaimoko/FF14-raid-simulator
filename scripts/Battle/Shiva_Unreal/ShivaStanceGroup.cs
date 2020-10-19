using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShivaStanceGroup : StatusGroup
{
    public enum StanceEnum
    {
        Bow,
        Sword,
        Wand,
        None
    }
    private bool castFinished = false;
    private SingleStatus timer, _actual;
    public SingleStatus actual { get => _actual; }
    public override bool expired { get => false; }
    private BaseStance bow, sword, wand, none;
    Dictionary<StanceEnum, BaseStance> stanceDict = new Dictionary<StanceEnum, BaseStance>();

    BattleManager bm = GameObject.FindGameObjectWithTag(Constants.BM.Tag).GetComponent<BattleManager>();
    public ShivaStanceGroup(GameObject from, GameObject target) :
        base(from, target)
    {
        name = "ShivaStanceGroup";
        stanceDict.Add(StanceEnum.Bow, new ShivaStanceBow(from, target));
        stanceDict.Add(StanceEnum.Sword, new ShivaStanceSword(from, target));
        stanceDict.Add(StanceEnum.Wand, new ShivaStanceWand(from, target));
        stanceDict.Add(StanceEnum.None, new ShivaStanceNone(from, target));
    }


    public void ChangeStance(StanceEnum newStance)
    {
        if (statuses.Count > 0)
        {
            SingleStatus prev = statuses[0];
            prev.expired = true; //  remove from ui status list 
            Debug.Log($"ShivaStanceGroup ChangeStance: From {prev.name} to {newStance}");
        }
        else
        {
            Debug.Log($"ShivaStanceGroup ChangeStance: Init to {newStance}");
        }
        statuses.Clear();
        Add(stanceDict[newStance]);
        if (stanceDict[newStance].showIcon)
        {
            stanceDict[newStance].expired = false;
            Debug.Log($"ShivaStanceGroup ChangeStance: Show {stanceDict[newStance].name}'s icon on TargetInfo.");
            bm.AddStatusIconToUI();
        }
    }
}
