using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGroup : StatusGroup
{
    // Start is called before the first frame update
    public StunGroup(GameObject from, GameObject target, float dur) :
        base(from, target)
    {
        name = "Stun";
        Add(new Stun(from, target, dur));
    }


}
