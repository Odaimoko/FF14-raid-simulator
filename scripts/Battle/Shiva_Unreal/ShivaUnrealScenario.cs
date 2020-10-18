using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        foreach (Enemy enemy in enemies)
        {
            enemy.aggro = new Dictionary<GameObject, int>(aggro);
        }

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!playersArrived[4])
        {
            Debug.Log($"ShivaUnrealScenario Update: Move {players[4].name} to Desti. {playersArrived[4]}.");
            playersArrived[4] = MovePlayerToDestination(players[4], new Vector3(-5, 0, 6));
        }
    }

    public override void GenerateEntities()
    {
        base.GenerateEntities();
        // gen enemies
        GameObject enemyPrefab = Resources.Load<GameObject>(Constants.GameSystem.boss2meta[SupportedBoss.Shiva_Unreal].modelPrefabPath);
        GameObject ShivaParent = Instantiate(enemyPrefab, new Vector3(0, .1f, 6.4f), Quaternion.identity);
        Shiva = ShivaParent.transform.Find("Shiva").gameObject;
        SceneManager.MoveGameObjectToScene(ShivaParent, SceneManager.GetSceneByName("Battle"));
        enemies.Add(Shiva.GetComponent<Enemy>());
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
