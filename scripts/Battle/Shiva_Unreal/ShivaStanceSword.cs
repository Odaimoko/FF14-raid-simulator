using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShivaStanceSword : BaseStance
{
    public ShivaStanceSword(GameObject from, GameObject target) :
        base(from, target)
    {
        icon = LoadStatusSprite("status_shiva_sword");
        name = "冰霜之剑";
    }

}
