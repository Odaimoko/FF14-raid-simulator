using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShivaStanceBow : BaseStance
{
    private float prevTargetAtkPoint = 0;
    [SerializeField]
    private const float CriticalMultiplier = 1.4f;
    public ShivaStanceBow(GameObject from, GameObject target) :
        base(from, target)
    {
        icon = LoadStatusSprite("status_shiva_bow");
        name = "冰霜之弓";
    }

}
