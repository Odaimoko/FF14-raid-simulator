using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast : SingleStatus
{ 
    public Cast(GameObject from, GameObject target, float dur) :
        base(from, target, dur)
    {
        effectiveAtOnce = true;
        showIcon = false;
        name = "EnemyCast"; 
    }

    protected override void NormalEffect()
    {
        base.NormalEffect();
        Entity e = target.GetComponent<Entity>();

    }
    protected override void ExpireEffect()
    {
        base.ExpireEffect();
        
    }
}
