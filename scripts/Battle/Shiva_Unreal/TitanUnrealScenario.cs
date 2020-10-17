using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanUnrealScenario : Scenario
{
    private GameObject Titan;
    [SerializeField]
 
    public override void Init()
    {
        base.Init();
        Titan = GameObject.Find("Titan");
        controlledPlayer.target = Titan;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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

    public void SlowDown()
    {
        Debug.Log("Titan_ex: SlowDown!!!!!");
        foreach (SinglePlayer singlePlayer in players)
        {
            singlePlayer.AddStatusGroup(new SlowdownGroup(Titan, singlePlayer.gameObject, 6f));
        }
    }

    public void Absolute_Zero()
    {
        Debug.Log("Titan_ex: Casting Absolute Zero...");
        Titan.GetComponent<Enemy>().AddStatusGroup(new CastGroup(Titan, Titan, 4f,
         new DealDamage(Titan, players[0].gameObject, 100, "Absolute Zero", Constants.Battle.RaidWideDistance)));
    }
}
