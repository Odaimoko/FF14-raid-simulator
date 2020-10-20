using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShivaUnreal_Shiva : Enemy
{
    Animator animator;
    SpriteRenderer tachieSpriteRenderer;
    private int hashChange = Animator.StringToHash("Change");
    private int hashTargetStance = Animator.StringToHash("TargetStance");
    List<Sprite> sprites = new List<Sprite>();
    private int currentStance;
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        tachieSpriteRenderer = transform.Find("tachie").GetComponent<SpriteRenderer>();
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
    }
}

