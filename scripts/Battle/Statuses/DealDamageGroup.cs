﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DealDamageGroup : StatusGroup
{
    // Wrapper
    // Start is called before the first frame update
    public DealDamageGroup(GameObject from, GameObject target, int dmg) :
        base(from, target)
    {
        Add(new DealDamage(from, target, dmg));
    }

    public override void RegisterEffect()
    {
        Debug.Log("DealDamageGroup: RegisterEffect", this.target);
        base.RegisterEffect();
    }
}