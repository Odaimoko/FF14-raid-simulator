using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCastGroup : StatusGroup
{
    // Wrapper
    // Start is called before the first frame update
    public EnemyCastGroup(GameObject from, GameObject target, float time) :
        base(from, target)
    {
        name = "EnemyCast";
        Add(new EnemyCast(from, target, time));
    }
 
}
