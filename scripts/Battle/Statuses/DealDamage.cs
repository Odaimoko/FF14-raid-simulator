using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DealDamage : SingleStatus
{
    private int damage;
    public DealDamage(GameObject from, GameObject target, int dmg, string name = "AutoAttack") :
        base(from, target, 1)
    {
        this.statusName = name;
        damage = dmg;
        showIcon = false;
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
