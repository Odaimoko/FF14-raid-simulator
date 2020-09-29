using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleStatus
{
    public enum EffectType
    {
        EffectOverTime,
        EffectOnExpiration, // Take effect when the count down reaches 0
        LongLasting, // no countdown
    }
    public enum BuffType
    {
        Buff,
        Debuff
    }
    // Status on Player or Enemy
    public GameObject from, target; //From whom this status comes
    protected float startTime = 0;
    public float duration { get; protected set; } // how long it will last
    public float countdown { get; protected set; } // remaining time
    public bool expired { get; protected set; } = false;
    public string statusName, statusDescription;
    public bool showIcon; // should we show icon on ui
    protected GameObject icon; // prefab
    // TODO: Effect variable
    protected bool lostAfterDeath = true;

    public SingleStatus(GameObject target)
    {
        this.target = target;
    }
    // Update is called once per frame
    public void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0 && !expired)
        {
            expired = true;
            OnStatusExpire();
        }
    }

    // Called per period (3 secs)
    public virtual void ApplyEffect()
    {
        Debug.Log("apply: Single Status " + this);
        //TODO which will be called?
    }
    // As the name says
    public virtual void OnStatusExpire()
    {
        Debug.Log("expired: base class " + this);
    }
}
