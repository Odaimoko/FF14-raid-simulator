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
        effectiveAtOnce=false;
    }

    protected override void NormalEffect()
    {
        base.NormalEffect();
        Debug.Log($"DealDamage Normal.", target);
    }

    protected override void ExpireEffect()
    {
        base.ExpireEffect();

        Vector3 towards = target.transform.position - from.transform.position;
        towards.y = 0;
        Entity e = target.GetComponent<Entity>();
        if (towards.magnitude <= Constants.Battle.minAtkDistance)
        {
            Debug.Log($"DealDamage {damage}!!! {e}", target);
            e.GotDamage(damage);
        }
        else
        {
            Debug.Log($"DealDamage: {target.name} too far from {from.name}..", target);
        }
    }
}
