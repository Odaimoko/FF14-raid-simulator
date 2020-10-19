using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVulnerableGroup : StatusGroup
{
    // Start is called before the first frame update
    public HitVulnerableGroup(GameObject from, GameObject target, float dur, float multi, int maxStacks) :
        base(from, target)
    {
        name = "打击耐性降低";
        Add(new HitVulnerable(from, target, dur, multi, maxStacks));
    }


}
