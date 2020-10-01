using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusGroup
{
    // The Buff/Debuff Manager to handle the status expiration and effect
    // Shoul dbe attached to every player or enemy
    protected List<SingleStatus> statuses = new List<SingleStatus>(); // Should be overridden, with a fixed size?
    public GameObject from, target;
    public bool expired
    {
        get
        {
            bool isExpire = true;
            foreach (SingleStatus s in statuses)
            {
                if (!s.expired)
                {
                    return false;
                }
            }
            return isExpire;
        }
    }
    public string name;


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
        // Assume s' from and target is properly set
        statuses.Add(s);
        // this will not have effect since the default behaviour 
        // is not to take effect on attach
        s.OnAttachedToEntity();
    }

    public virtual void RegisterEffect()
    {
        // each group will apply their effects differently
        Debug.Log($"Status Group ({this}) RegisterEffect.", this.target);
        foreach (SingleStatus s in statuses)
        {
            if (!s.expired)
            {
                s.RegisterEffect();
            }
        }
    }

    public override string ToString()
    {
        return $"StatusGroup: {name} ({from}->{target}).";
    }

    public override int GetHashCode()
    {
        return (name + from.ToString()).GetHashCode();
    }
}
