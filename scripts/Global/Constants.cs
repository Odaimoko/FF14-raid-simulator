using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{

    public static class BM
    {
        public static string Tag = "BattleManager";
        public static string EnemyTag = "Enemy";
        public static string PlayerTag = "Player";
    }

    public static class UI
    {
        public const string StatusPrefabDir = "battle_status";

        public const string IconPoolGOName = "icon pool";
        public const string IconPoolSpawningName = "status icon";
        public static float StatusListXInterval = 17;
        public static float StatusListXStart = -48;
        public static float StatusListYStart = 3.3f;
        public static float StatusIconScale = 0.7f;

        public static string PartyListItemPrefix = "Party list item ";
        public static float PartyListYStart = -25;
        public static float PartyListYInterval = 35;

        public const string DamageInfoPoolName = "damage info pool";
        public const string DamageInfoPoolSpawningName = "move info";
        public const float DamageStartFade = 1.5f;
        public const float DamageMovementSpeed = .7f;
        public const float DamageFadeSpeed = 0.1f;
        public const float DamageFadeInterval = .1f;
        public const float DamageMovementYLimit = DamageStartFade + 1 / DamageFadeSpeed * DamageFadeInterval * DamageMovementSpeed;
        public static Color32 DamageColor = new Color32(255, 150, 150, 255);
        public static Color32 HealingColor = new Color32(150, 255, 150, 255);
        public static Vector2 MiddleCenterAnchor = new Vector2(.5f, .5f);

    }

    public static class Battle
    {
        public const string PlayerPrefabPath = "battle_misc/playerPrefab";
        public const string PlayerGOPrefix = "Player";
        public const int NumPlayers = 8;
        public const float InBattleDistance = 10f;
        public const float MinAtkDistance = 3f;
        public const float AutoAtkInterval = 3f;
        public const float RaidWideDistance = 80f;
        public const float StatusRegisterInterval = 3f;
        public const float DestinationThreshold = .1f;
        public static Color32 GetPosColor(SinglePlayer.StratPosition pos)
        {
            switch (pos)
            {
                case SinglePlayer.StratPosition.MT:
                case SinglePlayer.StratPosition.ST:
                    return new Color32(100, 100, 255, 255);
                case SinglePlayer.StratPosition.H1:
                case SinglePlayer.StratPosition.H2:
                    return new Color32(100, 255, 100, 255);
                default:
                    return new Color32(255, 100, 100, 255);
            }
        }

        public enum DamageType
        {
            Blant, // Auto attack
            Magical,
            Physical,
            Ice,
            Fire,
            Slash,
            Hit,
        }
    }


    public static class GameSystem
    {

        private static string menuTachiePrefix = "menu";
        public class ScenarioDictStruct
        {
            public SupportedBoss boss;
            public System.Type scenarioType;
            public List<Strategy> strats;
            public string tachieFileName;
            public string headFileName;
            public Dictionary<SupportedLang, string> bossNameMultiLanguage;
            public string modelPrefabPath, animControllerPath;
            public Vector3 tachieLocalScale;

            public ScenarioDictStruct(SupportedBoss boss, System.Type type, List<Strategy> s, Dictionary<SupportedLang, string> namePairs, Vector3 tachieLocalScale)
            {
                this.boss = boss;
                scenarioType = type;
                strats = s;
                tachieFileName = $"{menuTachiePrefix}/{boss.ToString()}_paint";
                headFileName = $"{menuTachiePrefix}/{boss.ToString()}_head_paint";
                // Debug.Log($"ScenarioDictStruct: {tachieFileName}, {headFileName}");
                bossNameMultiLanguage = namePairs;
                this.tachieLocalScale = tachieLocalScale;
                modelPrefabPath = $"battle_misc/{boss.ToString()}_prefab";
                animControllerPath = $"scenarios/{boss.ToString()}/scenarioController";
                Debug.Log($"ScenarioDictStruct: {animControllerPath}");
            }
        }

        public const string GMObjectName = "Global Manager GO";

        public static Dictionary<SupportedBoss, ScenarioDictStruct> boss2meta = new Dictionary<SupportedBoss, ScenarioDictStruct>()
        {
           { SupportedBoss.ShivaUnreal, new ScenarioDictStruct(
               SupportedBoss.ShivaUnreal,
           typeof(ShivaUnrealScenario),
           new List<Strategy>(){
               new ShivaUnrealZiyanStrat()
           },
           new Dictionary<SupportedLang,string>(){
               {SupportedLang.CHN, "幻希瓦"}
           },
           Vector3.one)
           },
           { SupportedBoss.TitanUnreal, new ScenarioDictStruct(
               SupportedBoss.TitanUnreal,
           typeof(TitanUnrealScenario),
           new List<Strategy>(){
               new TitanUnrealZiyanStrat()
           },
           new Dictionary<SupportedLang,string>(){
               {SupportedLang.CHN, "幻泰坦"}
           },
           Vector3.one*.7f)
           },
        };



        public static Sprite GetSpriteByStratPos(SinglePlayer.StratPosition position)
        {
            string s = $"job_icon/{position.ToString()}_icon";
            Sprite sprite = Resources.Load<Sprite>(s);
            return sprite;
        }

    }
}

