using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCast : SingleStatus
{
    // Start is called before the first frame update
    public EnemyCast(GameObject from, GameObject target, float time) :
        base(from, target)
    {
        duration = time;
        countdown = time;
        effectiveAtOnce = true;
    }

    protected override void NormalEffect()
    {
        base.NormalEffect();
        Entity e = target.GetComponent<Entity>();
        e.casting = true;
    }
    protected override void ExpireEffect()
    {
        base.ExpireEffect();
        Entity e = target.GetComponent<Entity>();
        e.casting = false;
    }
}
