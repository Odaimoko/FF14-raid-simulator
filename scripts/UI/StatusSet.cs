using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StatusSet
{
    public GameObject icon;
    public SingleStatus singleStatus;
    public bool shown = false;
    public StatusSet(GameObject gameObject, SingleStatus ss)
    {
        icon = gameObject;
        singleStatus = ss;
    }
}