using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableStatusGroup : StatusGroup
{
    // Start is called before the first frame update
    public StackableStatusGroup(GameObject from, GameObject target, int maxStacks) :
        base(from, target)
    {
    }
    public override void MergeStatus(StatusGroup another)
    {
        // TODO: Merge 
        base.MergeStatus(another);
    }

}
