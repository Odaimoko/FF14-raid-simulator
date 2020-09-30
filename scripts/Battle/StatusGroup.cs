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

    public StatusGroup(GameObject from, GameObject target)
    {
        this.from = from;
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
        s.from = from;
        s.target = target;
    }

    public virtual void RegisterEffect()
    {
        // each group will apply their effects differently
        Debug.Log("Status Group: Register " + this, this.target);
        foreach (SingleStatus s in statuses)
        {
            if (!s.expired)
            {
                s.RegisterEffect();
            }
        }
    }
}
