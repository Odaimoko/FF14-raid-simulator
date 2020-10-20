using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CastGroup : StatusGroup
{
    // can player see it on gui
    public bool showInCastFrame = true,
    // has cast bar or not 
    stopMoving = true;
    private bool castFinished = false;
    private SingleStatus timer, _actual;
    public SingleStatus actual { get => _actual; }
    public CastGroup(GameObject from, GameObject target, float time, SingleStatus actual = null, bool show = true, bool stop = true) :
        base(from, target)
    {
        if (actual != null)
            name = actual.name;
        else
            name = "EnemyCast";
        timer = new Cast(from, target, time);
        Add(timer);
        _actual = actual;
        target.GetComponent<Entity>().castingStatus = this;
        showInCastFrame = show;
        stopMoving = stop;
    }

    public override void Update()
    {
        base.Update();
        if (timer.expired && !castFinished)
        {
            target.GetComponent<Entity>().castingStatus = null;
            if (_actual != null)
            {
                castFinished = true;
                Debug.Log($"CastGroup {target.name} finish casting. Applying {actual.name} to {actual.target.name}");
                Add(_actual);
            }
        }
    }
}
