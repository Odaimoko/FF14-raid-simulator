using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseStance : SingleStatus
{
    public BaseStance(GameObject from, GameObject target) :
        base(from, target, 1000)
    {
        statusEffectType = EffectType.LongLasting;
    }

}
