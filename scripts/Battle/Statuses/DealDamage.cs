using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DealDamage : SingleStatus
{
    private int damage;
    public DealDamage(GameObject from, GameObject target, int dmg) :
        base(from, target)
    {
        duration = .1f;
        countdown = .1f;
        damage = dmg;
    }

    // public override void Apply()
    // {
    //     base.Apply();
    //     if (!expired)
    //     {
    //     }
    //     else
    //     {
    //         Debug.Log($"Deal {damage} Damage to {target}", target);
    //     }
    // }
    protected override void NormalEffect()
    {
        base.NormalEffect();
    }

    protected override void ExpireEffect()
    {
        base.ExpireEffect();
    }
}
