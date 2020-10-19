using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ShivaUnrealScenario : Scenario
{
    private Enemy Shiva;
    [SerializeField]
    private string P1MusicPath = "music/祖堅正慶 - 雪上の足跡 ～蛮神シヴァ前哨戦～";
    private string P3MusicPath = "music/祖堅正慶 - 忘却の彼方 ～蛮神シヴァ討滅戦～";

    //
    // ─── ANIMATOR PARAMETERS ────────────────────────────────────────────────────────
    //


    private int hashShivaHP = Animator.StringToHash("ShivaHP");
    private int hashIsSword = Animator.StringToHash("isSword");
    private int hashNext = Animator.StringToHash("Next");
    private int hashSecondPhaseInP3 = Animator.StringToHash("SecondPhaseInP3");

    private int hash_Shiva_Unreal_3_Wand = Animator.StringToHash("Base Layer.Shiva_Unreal_3_Wand");
    private int hash_Shiva_Unreal_3_Ring_Bow = Animator.StringToHash("Base Layer.Shiva_Unreal_3_Ring_Bow");
    private int hash_Shiva_Unreal_Enrage = Animator.StringToHash("Base Layer.Shiva_Unreal_Enrage");
    private int hash_Shiva_Unreal_3_Sword = Animator.StringToHash("Base Layer.Shiva_Unreal_3_Sword");
    private int hash_Shiva_Unreal_1_Wand = Animator.StringToHash("Base Layer.Shiva_Unreal_1_Wand");
    private int hash_Shiva_Unreal_2_Adds_Player_Lose = Animator.StringToHash("Base Layer.Shiva_Unreal_2_Adds_Player_Lose");
    private int hash_Shiva_Unreal_2_Adds_Shiva_Invul = Animator.StringToHash("Base Layer.Shiva_Unreal_2_Adds_Shiva_Invul");
    private int hash_Shiva_Unreal_Final_Player_Win = Animator.StringToHash("Base Layer.Shiva_Unreal_Final_Player_Win");
    private int hash_Shiva_Unreal_1_Normal = Animator.StringToHash("Base Layer.Shiva_Unreal_1_Normal");
    private int hash_Shiva_Unreal_1_Sword = Animator.StringToHash("Base Layer.Shiva_Unreal_1_Sword");
    private int hash_Shiva_Unreal_2_Adds = Animator.StringToHash("Base Layer.Shiva_Unreal_2_Adds");
    private int hash_Shiva_Unreal_Diamond_Dust = Animator.StringToHash("Base Layer.Shiva_Unreal_Diamond_Dust");
    private int hash_Shiva_Unreal_0_AutoAtk = Animator.StringToHash("Base Layer.Shiva_Unreal_0_AutoAtk");
    private int hash_Shiva_Unreal_2_Adds_Player_Win = Animator.StringToHash("Base Layer.Shiva_Unreal_2_Adds_Player_Win");
    private int hash_Shiva_Unreal_0 = Animator.StringToHash("Base Layer.Shiva_Unreal_0");

    private Constants.GameSystem.ScenarioDictStruct infoStruct;

    public override void Init()
    {
        base.Init();
        infoStruct = Constants.GameSystem.boss2meta[SupportedBoss.Shiva_Unreal];
        foreach (SinglePlayer singlePlayer in players)
        {
            singlePlayer.target = Shiva.gameObject;
        }
        foreach (Enemy enemy in enemies)
        {
            enemy.aggro = new Dictionary<GameObject, int>(aggro);
        }
        audioSource.clip = Resources.Load<AudioClip>(P1MusicPath);
        audioSource.Play();
        scenarioAnimator = GetComponent<Animator>();
        scenarioAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(infoStruct.animControllerPath);

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // if (!playersArrived[4])
        // {
        //     Debug.Log($"ShivaUnrealScenario Update: Move {players[4].name} to Desti. {playersArrived[4]}.");
        //     playersArrived[4] = MovePlayerToDestination(players[4], new Vector3(-5, 0, 6));
        // } 
    }

    public override void GenerateEntities()
    {
        base.GenerateEntities();
        // gen enemies
        GameObject enemyPrefab = Resources.Load<GameObject>(Constants.GameSystem.boss2meta[SupportedBoss.Shiva_Unreal].modelPrefabPath);
        GameObject ShivaParent = Instantiate(enemyPrefab, new Vector3(0, .1f, 6.4f), Quaternion.identity);
        Shiva = ShivaParent.transform.Find("Shiva").GetComponent<Enemy>();
        SceneManager.MoveGameObjectToScene(ShivaParent, SceneManager.GetSceneByName("Battle"));
        enemies.Add(Shiva);
    }



    public void SlowDown()
    {
        Debug.Log("Shiva_ex: SlowDown!!!!!");
        foreach (SinglePlayer singlePlayer in players)
        {
            singlePlayer.AddStatusGroup(new SlowdownGroup(Shiva.gameObject, singlePlayer.gameObject, 6f));
        }
    }

    public void Absolute_Zero()
    {
        Debug.Log("Shiva_ex: Casting Absolute Zero...");

        foreach (SinglePlayer singlePlayer in players)
        {
            Shiva.GetComponent<Enemy>().AddStatusGroup(new CastGroup(Shiva.gameObject, Shiva.gameObject, 4f,
            new DealDamage(Shiva.gameObject, singlePlayer.gameObject, 100, "Absolute Zero", Constants.Battle.RaidWideDistance)));
        }
    }

    public void TriggerNext()
    {
        Debug.Log($"Next Triggered.");
        scenarioAnimator.SetTrigger(hashNext);

    }

    public void SetRandomSword()
    {
        scenarioAnimator.SetBool(hashIsSword, Random.Range(0f, 1f) > .5);
    }

    public void Shiva_Unreal_0_Next()
    {
        if (Shiva.inBattle)
        {
            TriggerNext();
            SetRandomSword();
        }
    }
    public void CheckShivaHP()
    {
        float hpPercent = Shiva.healthPoint / Shiva.maxHP;
        // Debug.Log($"CheckShivaHP: {hpPercent}");
        scenarioAnimator.SetFloat(hashShivaHP, hpPercent);
    }

    public void Shiva_Unreal_1_Next()
    {
        CheckShivaHP();
        TriggerNext();
    }

    public void Shiva_Unreal_2_Adds_Enter()
    {

    }

    public void Shiva_Unreal_3_Ring_Bow_Enter()
    {
        if (!scenarioAnimator.GetBool(hashSecondPhaseInP3))
        {
            // TODO reset timing
            SetRandomSword();
        }
    }
}
