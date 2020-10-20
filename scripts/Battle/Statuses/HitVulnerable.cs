using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVulnerable : ReceiverDamageChanger 
{
    private int _numStacks;
    public HitVulnerable(GameObject fr, GameObject target, float dur, float multi, int maxStacks) : base(fr, target, dur, multi, maxStacks)
    {
        name = "打击耐性降低";
        icon = LoadStatusSprite("status_hit_vul");
    }

 
    public override float GetMultiplier(List<Constants.Battle.DamageType> types)
    {
        // TODO: Set multiplier according to stacks
        float m = 1;
        if (types.Contains(Constants.Battle.DamageType.Hit))
            m *= multi;
        return m;
    }

}
