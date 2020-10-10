using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiva_ex : Scenario
{
    private GameObject Shiva;
    [SerializeField]
    private SinglePlayer controlledPlayer;
    public override void Init()
    {
        base.Init();
        Shiva = GameObject.Find("Shiva");
        controlledPlayer.target = Shiva;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void StartScenario(int phase = 0)
    {
        base.StartScenario(phase);
    }

    public override void GenerateEntities()
    {
        base.GenerateEntities();
    }

    protected override void RegisterEntities()
    {
        base.RegisterEntities();
    }

    protected override void SetAggro()
    {
        base.SetAggro();
    }

    public override void Stop()
    {
        base.Stop();
    }

    public void SlowDown()
    {
        Debug.Log("Shiva_ex: SlowDown!!!!!");
        foreach (SinglePlayer singlePlayer in players)
        {
            singlePlayer.AddStatusGroup(new SlowdownGroup(Shiva, singlePlayer.gameObject, 7f));
        }
    }

    public void Absolute_Zero()
    {
        Debug.Log("Shiva_ex: Absolute_Zero!!!!!");
        foreach (SinglePlayer singlePlayer in players)
        {
            singlePlayer.AddStatusGroup(new StunGroup(Shiva, singlePlayer.gameObject, 4f));
        }
    }
}
