using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strategy
{
    public virtual string name { get; set; }

    public List<BattlePhase> supportedPhases = new List<BattlePhase>();

    public Strategy()
    {
        InitPhases();
    }

    // Start is called before the first frame update
    void Strate()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void InitPhases()
    {

    }
}
