using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DealDamageGroup : StatusGroup
{
    // Wrapper
    // Start is called before the first frame update
    public DealDamageGroup(GameObject from, GameObject target, float dmg) :
        base(from, target)
    {
        name = "DealDamage";
        Add(new DealDamage(from, target, dmg));
    }
 
}
