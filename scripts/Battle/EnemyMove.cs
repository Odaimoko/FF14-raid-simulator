using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private GameObject animationFx;    // Prefab
    private AudioClip soundFx; // sound 
    private float rawDamage;

    public enum MoveType
    {
        Single, // attack the first aggro
        NormalAOE, // aoe with some shape
        RaidAOE, // raid wide aoe
        Share, // damage  = raw / #_players
        TrackTarget, // tracks some player, bait but cannot dodge
        BaitAOE, // Bait and dodge
        DOT, //
        Knockback, //
        DispellableDebuff,//
        UndispellableDebuff,//
        Buff, // buff the enemy, or the player
    }
    private MoveType[] moveTypes; // Might be the combination of these types
    private GameObject areaOfEffect; //  the area of this move

    private GameObject[] target; // who to apply to


    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
