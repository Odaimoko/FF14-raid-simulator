using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IceDoT : SingleStatus
{
    public IceDoT(GameObject from, GameObject target, float dur, float dmg) :
        base(from, target, dur)
    {
        icon = LoadStatusSprite("status_ice_dot");
        name = "冻伤";
    }

    protected override void NormalEffect()
    {
        base.NormalEffect();
        // TODO: Damage over time
    }


}
