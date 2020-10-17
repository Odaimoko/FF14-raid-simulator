using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShivaUnrealScenario : Scenario
{
    private GameObject Shiva;
    [SerializeField]

    public override void Init()
    {
        base.Init();
        foreach (SinglePlayer singlePlayer in players)
        {
            singlePlayer.target = Shiva;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void GenerateEntities()
    {
        base.GenerateEntities();
        // gen enemies
        GameObject enemyPrefab = Resources.Load<GameObject>(Constants.GameSystem.boss2meta[SupportedBoss.Shiva_Unreal].modelPrefabPath);
        GameObject ShivaParent = Instantiate(enemyPrefab, new Vector3(0, .1f, 6.4f), Quaternion.identity);
        Shiva = ShivaParent.transform.Find("Shiva").gameObject;
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
        Debug.Log("Shiva_ex: SlowDown!!!!!");
        foreach (SinglePlayer singlePlayer in players)
        {
            singlePlayer.AddStatusGroup(new SlowdownGroup(Shiva, singlePlayer.gameObject, 6f));
        }
    }

    public void Absolute_Zero()
    {
        Debug.Log("Shiva_ex: Casting Absolute Zero...");
        Shiva.GetComponent<Enemy>().AddStatusGroup(new CastGroup(Shiva, Shiva, 4f,
         new DealDamage(Shiva, players[0].gameObject, 100, "Absolute Zero", Constants.Battle.RaidWideDistance)));
    }
}
