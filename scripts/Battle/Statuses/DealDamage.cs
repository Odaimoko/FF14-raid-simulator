using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DealDamage : SingleStatus
{
    public Constants.Battle.DamageType damageType = Constants.Battle.DamageType.Blant;
    private float _damage;
    private float _range;
    public DealDamage(GameObject from, GameObject target, float dmg, string name = "AutoAttack", float range = Constants.Battle.MinAtkDistance) :
        base(from, target, .1f)
    {
        this.name = name;
        _damage = dmg;
        showIcon = false;
        effectiveAtOnce = false;
        this._range = range;
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
        if (towards.magnitude <= _range)
        {
            Debug.Log($"DealDamage {_damage}!!! {e}", target);
            e.GotDamage(_damage);
        }
        else
        {
            Debug.Log($"DealDamage: {target.name} too far from {from.name}..", target);
        }
    }
}
