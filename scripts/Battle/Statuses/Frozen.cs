using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Frozen : SingleStatus
{
    public Frozen(GameObject from, GameObject target, float dur) :
        base(from, target, dur)
    {
        icon = LoadStatusSprite("status_frozen");
        name = "冻结";
    }

    protected override void NormalEffect()
    {
        // TODO
        base.NormalEffect();
    }


}
