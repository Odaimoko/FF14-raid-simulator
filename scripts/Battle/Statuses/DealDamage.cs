using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DealDamage : SingleStatus
{
    private int damage;
    public DealDamage(GameObject from, GameObject target, int dmg) :
        base(from, target)
    {
        duration = 1;
        countdown = 1;
        damage = dmg;
    }

    protected override void NormalEffect()
    {
        base.NormalEffect();
        Debug.Log($"DealDamage Normal. {countdown}", target);
    }

    protected override void ExpireEffect()
    {
        base.ExpireEffect();

        Entity e = target.GetComponent<Entity>();
        Debug.Log($"DealDamage {damage}!!! {e}", target);
        e.GotDamage(damage);
    }
}
