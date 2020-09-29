using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusGroup
{
    // The Buff/Debuff Manager to handle the status expiration and effect
    // Shoul dbe attached to every player or enemy
    protected List<SingleStatus> statuses = new List<SingleStatus>(); // Should be overridden, with a fixed size?
    public GameObject from, target;
    public bool expired { get; protected set; } = false;
    
    public StatusGroup(GameObject target)
    {
        this.target = target;
    }
    public void Update()
    {
        foreach (SingleStatus s in statuses)
        {
            s.Update();
        }
    }

    public void Add(SingleStatus s)
    {
        statuses.Add(s);
        s.target = target;
    }

    public virtual void ApplyEffect()
    {
        // each group will apply their effects differently
        Debug.Log("apply: Status Group: " + this);
        foreach (SingleStatus s in statuses)
        {
            if (!s.expired)
            {
                Debug.Log("single status name: " + s.statusName);
                s.ApplyEffect();
            }
        }
    }
}
