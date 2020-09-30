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

    public SingleStatus(GameObject from, GameObject target)
    {
        this.from = from;
        this.target = target;
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
            Debug.Log($"SingleStatus ({this} Expired. Registering.");
            RegisterEffect();
        }
    }
    protected virtual void NormalEffect()
    {
        Debug.Log($"SingleStatus ({this}) Normal: From {from} to {target}", this.target);
    }

    protected virtual void ExpireEffect()
    {

        Debug.Log($"SingleStatus ({this}) Expire: From {from} to {target}", this.target);
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

    // Called per period (3 secs)
    public void RegisterEffect()
    {
        Debug.Log($"SingleStatus ({this}) RegisterEffect: " + this, this.target);
        BattleManager bm = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        bm.AddEvent(this);
    }
}
