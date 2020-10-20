using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInvul : ReceiverDamageChanger
{
    public EnemyInvul(GameObject fr, GameObject target, float dur) : base(fr, target, dur, 0, 1)
    {
        icon = LoadStatusSprite("status_enemy_invul");
    } 
}
