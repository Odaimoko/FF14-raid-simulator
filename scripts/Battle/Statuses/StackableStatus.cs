using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableStatus : SingleStatus
{
    protected int maxStacks, numStacks = 1;
 
    public StackableStatus(GameObject from, GameObject target, float dur, int maxStacks) : base(from, target, dur)
    {
        this.maxStacks = maxStacks;
    }
 
}
