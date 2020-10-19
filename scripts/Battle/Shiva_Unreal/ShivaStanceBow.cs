using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShivaStanceBow : BaseStance
{
    public ShivaStanceBow(GameObject from, GameObject target) :
        base(from, target)
    {
        icon = LoadStatusSprite("status_shiva_bow");
        name = "冰霜之弓";
    }

}
