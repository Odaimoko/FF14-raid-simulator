using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleStatus : MonoBehaviour
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
    protected GameObject from; //From whom this status comes
    protected float startTime = 0;
    public float duration { get; set; } // how long it will last
    [SerializeField]
    protected float countdown; // remaining time
    protected string statusName, statusDescription;
    protected GameObject icon; // prefab
    // TODO: Effect variable
    protected bool lostAfterDeath = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected void Update()
    {
        countdown -= Time.deltaTime;
    }

    // Called per period (3 secs)
    public virtual void ApplyEffect()
    {
        Debug.Log("apply: Single Status " + this);
        if (countdown < 0)
            OnStatusExpire();
        //TODO which will be called?
    }
    // As the name says
    public virtual void OnStatusExpire()
    {
        Debug.Log("expired: base class " + this);
    }
}
