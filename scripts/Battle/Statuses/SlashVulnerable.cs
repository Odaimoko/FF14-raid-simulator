using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashVulnerable : ReceiverDamageChanger
{
    private int _numStacks;
    public SlashVulnerable(GameObject fr, GameObject target, float dur, float multi, int maxStacks) : base(fr, target, dur, multi,maxStacks)
    {
        name = "斩击耐性降低";
        icon = LoadStatusSprite("status_hit_vul");
    }
 

    public override float GetMultiplier(List<Constants.Battle.DamageType> types)
    {
        float m = 1;
        if (types.Contains(Constants.Battle.DamageType.Slash))
            m *= multi;
        return m;
    }

}
