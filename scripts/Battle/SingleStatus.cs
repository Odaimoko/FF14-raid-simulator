using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleStatus
{
    protected static int globalStatusID = 0;
    private int statusID;
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
    protected bool lostAfterDeath = true;
    // if this status has effect once attached to an entity
    protected bool effectiveAtOnce = true;
    //
    // ─── METAINFO ───────────────────────────────────────────────────────────────────
    //
    public string statusName, statusDescription;
    private bool _showIcon = true;
    public bool showIcon
    {
        get
        {
            // dont show icon if expired
            return _showIcon && !expired;
        }
        set { _showIcon = value; }
    } // should we show icon on ui   
    public Sprite icon; // prefab
    // TODO: Effect variable
    protected BattleManager bm;


    public SingleStatus(GameObject from, GameObject target, float dur)
    {
        this.from = from;
        this.target = target;
        duration = countdown = dur;
        bm = GameObject.FindGameObjectWithTag(Constants.BM.Tag).GetComponent<BattleManager>();
        statusID = globalStatusID;
        globalStatusID++;
    }

    public void Update()
    {
        if (countdown >= 0)
        {
            countdown -= Time.deltaTime;
            // Debug.Log($"SingleStatus ({this}) Countdown: {countdown}");
        }
        if (countdown < 0 && !expired)
        {
            expired = true;
            Debug.Log($"SingleStatus ({this.statusName}) Expired. Registering.");
            RegisterEffect();
        }
    }
    protected virtual void NormalEffect()
    {
        Debug.Log($"SingleStatus ({this.statusName}) Normal: From {from.name} to {target.name}", this.target);
    }

    protected virtual void ExpireEffect()
    {
        Debug.Log($"SingleStatus ({this.statusName}) Expire: From {from.name} to {target.name}", this.target);
        // TODO: show remove status on target
    }


    public void Apply()
    {
        if (!expired)
        {
            NormalEffect();
        }
        else
        {
            ExpireEffect();
        }
    }

    // called when the status is first attached to an entity
    public virtual void OnAttachedToEntity()
    {
        // TODO: Show add status on target
        if (effectiveAtOnce)
        {
            bm.AddEvent(this);
        }
    }

    // Called per period (3 secs)
    public void RegisterEffect()
    {
        Debug.Log($"SingleStatus ({this.statusName}) RegisterEffect.", this.target);
        bm.AddEvent(this);
    }

    public override int GetHashCode()
    {
        return (statusName + statusID).GetHashCode();
    }

    public override string ToString()
    {
        return (statusName + statusID);
    }
}
