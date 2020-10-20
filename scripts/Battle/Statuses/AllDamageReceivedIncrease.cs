using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDamageReceivedIncrease : ReceiverDamageChanger 
{
    private int _numStacks;
    public AllDamageReceivedIncrease(GameObject fr, GameObject target, float dur, float multi, int maxStacks) : base(fr, target, dur, multi, maxStacks)
    {
        name = "受伤加重";
        icon = LoadStatusSprite("status_damage_vulnerable");
    }


}
