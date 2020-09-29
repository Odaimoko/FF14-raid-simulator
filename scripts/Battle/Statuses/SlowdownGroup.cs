using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlowdownGroup : StatusGroup
{
    // Wrapper
    // Start is called before the first frame update
    public SlowdownGroup(GameObject target) :
        base(target)
    {
        Slowdown slowdown = new Slowdown(target);
        Add(slowdown);
    }

    public override void ApplyEffect()
    {
        Debug.Log("APPLIED SLOWDOWNGROUP");
        base.ApplyEffect();
    }
}
