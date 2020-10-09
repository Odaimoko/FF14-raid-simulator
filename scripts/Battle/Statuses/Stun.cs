using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : SingleStatus
{
    public Stun(GameObject fr, GameObject target, float dur) : base(fr, target, dur)
    {
        effectiveAtOnce = true;
        statusName = "Stun";
        icon = Resources.Load<Sprite>("battle_status/status_stun");
    }

    protected override void NormalEffect()
    {
        base.NormalEffect();
        SinglePlayer sp = target.GetComponent<SinglePlayer>();
        sp.controllable = false;
    }

    protected override void ExpireEffect()
    {
        base.ExpireEffect();
        SinglePlayer sp = target.GetComponent<SinglePlayer>();
        sp.controllable = true;
    }

}
