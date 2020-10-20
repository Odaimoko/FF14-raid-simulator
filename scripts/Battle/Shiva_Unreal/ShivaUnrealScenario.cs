using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ShivaUnrealScenario : Scenario
{
    private ShivaUnreal_Shiva Shiva;
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
    private int hashPhase = Animator.StringToHash("Phase");
    private int hashAddsTime = Animator.StringToHash("AddsTime");

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

    //
    // ─── BATTLE INFO ─────────────────────────────────────────────────
    //

    private ShivaStanceGroup stanceGroup;

    public override void Init()
    {
        base.Init();
        infoStruct = Constants.GameSystem.boss2meta[SupportedBoss.ShivaUnreal];
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
        Shiva.healthPoint = 60000;
        stanceGroup = new ShivaStanceGroup(Shiva.gameObject, Shiva.gameObject);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        switch (scenarioAnimator.GetInteger(hashPhase))
        {
            case 2:
                CheckShivaHP();
                float addsTime = scenarioAnimator.GetFloat(hashAddsTime);
                scenarioAnimator.SetFloat(hashAddsTime, addsTime + Time.deltaTime);
                break;
            case 3:
                CheckShivaHP();
                break;
        }
    }

    public override void GenerateEntities()
    {
        base.GenerateEntities();
        // gen enemies
        GameObject enemyPrefab = Resources.Load<GameObject>(Constants.GameSystem.boss2meta[SupportedBoss.ShivaUnreal].modelPrefabPath);
        GameObject ShivaParent = Instantiate(enemyPrefab, new Vector3(0, .1f, 6.4f), Quaternion.identity);
        Shiva = ShivaParent.transform.Find("Shiva").GetComponent<ShivaUnreal_Shiva>();
        SceneManager.MoveGameObjectToScene(ShivaParent, SceneManager.GetSceneByName("Battle"));
        enemies.Add(Shiva);
    }

    //
    // ─── MOVES ──────────────────────────────────────────────────────────────────────
    //



    public void Slowdown()
    {
        Debug.Log("Shiva_ex: SlowDown!!!!!");
        controlledPlayer.AddStatusGroup(new SlowdownGroup(Shiva.gameObject, controlledPlayer.gameObject, 6f));
        // foreach (SinglePlayer singlePlayer in players)
        // {
        //     singlePlayer.AddStatusGroup(new SlowdownGroup(Shiva.gameObject, singlePlayer.gameObject, 6f));
        // }
    }



    public void IceBrand()
    {
        // 冰印剑
        Shiva.IceBrand();
    }

    public void HeavenlyStrike()
    {
        // 天降一击 
    }

    public void Whiteout()
    {
        // 白化视界
    }

    public void GlacierBash()
    {
        // 冰河怒击
    }

    //
    // ──────────────────────────────────────────────── II ──────────
    //   :::::: N O N E : :  :   :    :     :        :          :
    // ──────────────────────────────────────────────────────────
    //

    public void DreamsOfIce()
    {
        // 寒冰的幻想
    }

    public void IcicleImpact(int type)
    {
        // 连环
    }

    public void DiamondDust()
    {
        // 钻石星尘
    }

    //
    // ──────────────────────────────────────────────── II ──────────
    //   :::::: W A N D : :  :   :    :     :        :          :
    // ──────────────────────────────────────────────────────────
    //

    public void HailStorm()
    {
        // 冰圈点名
    }


    public void Absolute_Zero()
    {
        // 绝对零度
    }
    //
    // ────────────────────────────────────────────── II ──────────
    //   :::::: B O W : :  :   :    :     :        :          :
    // ────────────────────────────────────────────────────────
    //

    public void GlassDance()
    {
        // 冰雪乱舞
    }

    public void Avalanche()
    {
        // 雪崩击退
    }

    public void Permafrost()
    {
        // 永久冻土 + Frostbite 
    }

    //
    // ─── STATEMACHINE CHANGE ────────────────────────────────────────────────────────
    //



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

    public void Shiva_Unreal_0_AutoAtk_Enter()
    {
        Shiva.AddStatusGroup(stanceGroup);
        stanceGroup.ChangeStance(ShivaStanceGroup.StanceEnum.None);
    }

    public void Shiva_Unreal_1_Sword_sp_Enter()
    {
        Debug.Log("Shiva_Unreal_1_Sword_sp_Enter");
        stanceGroup.ChangeStance(ShivaStanceGroup.StanceEnum.Sword);
        Shiva.ChangeStance((int)ShivaStanceGroup.StanceEnum.Sword);
        new WaitForSeconds(3);
        Debug.Log("Shiva_Unreal_1_Sword_sp_Enter 2");
    }

    public void Shiva_Unreal_1_Wand_sp_Enter()
    {
        Debug.Log("Shiva_Unreal_1_Wand_sp_Enter");
        stanceGroup.ChangeStance(ShivaStanceGroup.StanceEnum.Wand);
        Shiva.ChangeStance((int)ShivaStanceGroup.StanceEnum.Wand);
    }
    public void Shiva_Unreal_1_Normal_Enter()
    {
        Debug.Log("Shiva_Unreal_1_Normal_Enter");
        stanceGroup.ChangeStance(ShivaStanceGroup.StanceEnum.None);
        Shiva.ChangeStance((int)ShivaStanceGroup.StanceEnum.None);
    }

    public void Shiva_Unreal_1_Next()
    {
        CheckShivaHP();
        TriggerNext();
    }

    public void Shiva_Unreal_2_Adds_Enter()
    {
        // Spawn Adds
        // Init Adds time counter
        scenarioAnimator.SetInteger(hashPhase, 2);
        scenarioAnimator.SetFloat(hashAddsTime, 10f);


    }

    public void Shiva_Unreal_Diamond_Dust_Enter()
    {

    }
    public void Shiva_Unreal_3_Start_Enter()
    {
        if (!scenarioAnimator.GetBool(hashSecondPhaseInP3))
        {
            // TODO reset timing
            SetRandomSword();
        }
        audioSource.clip = Resources.Load<AudioClip>(P3MusicPath);
        audioSource.Play();
        scenarioAnimator.SetInteger(hashPhase, 3);
    }

    public void Shiva_Unreal_3_Ring_Bow_Enter()
    {
    }

    public void Shiva_Unreal_3_Ring_Bow_Next()
    {
        Debug.Log("Shiva_Unreal_3_Ring_Bow_Next");
        TriggerNext();
    }

    public void Shiva_Unreal_3_Next()
    {
        Debug.Log("Shiva_Unreal_3_Next");
        if (scenarioAnimator.GetBool(hashSecondPhaseInP3))
        {
            Debug.Log("hashSecondPhaseInP3 the second phase has ended.");
            scenarioAnimator.SetBool(hashSecondPhaseInP3, false);
        }
        else
        {
            Debug.Log("hashSecondPhaseInP3 the first phase has ended.");
            scenarioAnimator.SetBool(hashSecondPhaseInP3, true);

        }
        TriggerNext();
    }
}
