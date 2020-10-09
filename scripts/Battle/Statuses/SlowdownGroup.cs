using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlowdownGroup : StatusGroup
{
    // Wrapper
    // Start is called before the first frame update
    public SlowdownGroup(GameObject from, GameObject target, float dur) :
        base(from, target)
    {
        name = "Slowdown";
        Slowdown slowdown = new Slowdown(from, target, dur);
        Add(slowdown);
    } 
}
