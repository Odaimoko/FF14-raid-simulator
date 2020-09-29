using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusGroup : MonoBehaviour
{
    // The Buff/Debuff Manager to handle the status expiration and effect
    // Shoul dbe attached to every player or enemy
    protected List<SingleStatus> statuses = new List<SingleStatus>(); // Should be overridden, with a fixed size?
    // Start is called before the first frame update
    void Start()
    {

    }
 

    public void Add(SingleStatus s)
    {
        statuses.Add(s);
    }

    public virtual void ApplyEffect()
    {
        // each group will apply their effects differently
        Debug.Log("apply: Status Group" + this);
        foreach(SingleStatus s in statuses){
            s.ApplyEffect();
        }
    }
}
