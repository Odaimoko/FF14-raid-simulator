using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlowdownGroup : StatusGroup
{
    // Wrapper
    // Start is called before the first frame update
    public SlowdownGroup(GameObject from, GameObject target) :
        base(from, target)
    {
        Slowdown slowdown = new Slowdown(from, target);
        Add(slowdown);
    }

    public override void RegisterEffect()
    {
        base.RegisterEffect();
    }
}
