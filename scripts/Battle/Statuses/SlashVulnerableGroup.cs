using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashVulnerableGroup : StatusGroup
{
    // Start is called before the first frame update
    public SlashVulnerableGroup(GameObject from, GameObject target, float dur, float multi, int maxStacks) :
        base(from, target)
    {
        name = "斩击耐性降低";
        Add(new SlashVulnerable(from, target, dur, multi, maxStacks));
    }


}
