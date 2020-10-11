using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CastGroup : StatusGroup
{
    private bool expired;
    private SingleStatus timer, _actual;
    public SingleStatus actual { get => _actual; }
    public CastGroup(GameObject from, GameObject target, float time, SingleStatus actual) :
        base(from, target)
    {
        name = "EnemyCast";
        timer = new Cast(from, target, time);
        Add(timer);
        _actual = actual;
        target.GetComponent<Entity>().castingStatus = this;
    }
    public override void Update()
    {
        base.Update();
        if (timer.expired && !expired)
        {
            expired = true;
            Debug.Log($"CastGroup {target.name} finish casting. Applying {actual.statusName} to {actual.target.name}");
            target.GetComponent<Entity>().castingStatus = null;
            Add(_actual);
        }
    }
}
