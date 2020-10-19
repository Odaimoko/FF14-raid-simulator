using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableStatus : SingleStatus
{
    protected int maxStacks, numStacks = 0;
    private GameObject fr;
    private float dur;
 
    public StackableStatus(GameObject from, GameObject target, float dur, int maxStacks) : base(from, target, dur)
    {
        this.maxStacks = maxStacks;
    }
 
}
