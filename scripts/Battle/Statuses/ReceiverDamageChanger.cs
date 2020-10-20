using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverDamageChanger : DamageChanger
{
    public ReceiverDamageChanger(GameObject fr, GameObject target, float dur, float multi, int maxStacks) : base(fr, target, dur, multi, maxStacks)
    {
    }

}
