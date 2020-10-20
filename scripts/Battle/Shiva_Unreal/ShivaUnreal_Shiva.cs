using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShivaUnreal_Shiva : Enemy
{
    Animator animator;
    SpriteRenderer tachieSpriteRenderer;
    //
    // ─── ANIM ───────────────────────────────────────────────────────────────────────
    //

    private string changeAnimPath = "scenarios/ShivaUnreal/Shiva_Stance_Change";
    private AnimationClip changeStanceClip;
    private int hashChange = Animator.StringToHash("Change");
    private int hashTargetStance = Animator.StringToHash("TargetStance");
    List<Sprite> sprites = new List<Sprite>();
    private int currentStance;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        tachieSpriteRenderer = transform.Find("tachie").GetComponent<SpriteRenderer>();
        changeStanceClip = Resources.Load<AnimationClip>(changeAnimPath);
        foreach (string s in System.Enum.GetNames(typeof(ShivaStanceGroup.StanceEnum)))
        {
            Debug.Log($"ShivaUnreal_Shiva: Load Sprite enemy_tachie/ShivaUnreal_{s}");
            sprites.Add(Resources.Load<Sprite>($"enemy_tachie/ShivaUnreal_{s}"));
        }
    }


    protected override void AA()
    {
        // TODO Change damage and effect according to stance
        base.AA();
    }

    public void ChangeSprite()
    {
        tachieSpriteRenderer.sprite = sprites[currentStance];
    }

    public void ChangeStance(int stanceCode)
    {
        Debug.Log($"ShivaUnreal_Shiva: Change Stance to {stanceCode}");
        currentStance = stanceCode;
        animator.SetTrigger(hashChange);
        animator.SetInteger(hashTargetStance, stanceCode);
        AddStatusGroup(new CastGroup(gameObject, gameObject, changeStanceClip.length, null, false));
    }

    //
    // ─── MOVES ──────────────────────────────────────────────────────────────────────
    //

    //
    // ────────────────────────────────────────────────── II ──────────
    //   :::::: S W O R D : :  :   :    :     :        :          :
    // ────────────────────────────────────────────────────────────
    //


    public void IceBrand()
    {
        // 冰印剑
        // TODO: FX 
        Debug.Log("ShivaUnreal_Shiva IceBrand");
        // duration is animation time
        Move_IceBrand iceBrand = new Move_IceBrand(gameObject, gameObject, 4);
        AddStatusGroup(new CastGroup(gameObject, gameObject, 0.1f, iceBrand, false));
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
        Debug.Log("Shiva_ex: Casting Absolute Zero...");

        foreach (SinglePlayer singlePlayer in players)
        {
            AddStatusGroup(new CastGroup(gameObject, gameObject, 4f,
            new DealDamage(gameObject, singlePlayer.gameObject, 100, "Absolute Zero", Constants.Battle.RaidWideDistance)));
        }
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
}

