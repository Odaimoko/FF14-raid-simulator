
public enum EnemyMoveType
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