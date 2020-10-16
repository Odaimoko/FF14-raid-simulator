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
        public const float inBattleDistance = 10f;
        public const float minAtkDistance = 3f;
        public const float autoAtkInterval = 3f;
        public const float raidWideDistance = 80f;

    }


    public static class GameSystem
    {

        private static string menuTachiePrefix = "menu";
        public class ScenarioDictStruct
        {
            public SupportedBoss boss;
            public System.Type scenarioType;
            public List<Strategy> strats;
            public string tachieFileName, headFileName;
            public Dictionary<SupportedLang, string> bossNameMultiLanguage;
            public ScenarioDictStruct(SupportedBoss boss, System.Type type, List<Strategy> s, Dictionary<SupportedLang, string> namePairs)
            {
                this.boss = boss;
                scenarioType = type;
                strats = s;
                tachieFileName = $"{menuTachiePrefix}/{boss.ToString()}_paint";
                headFileName = $"{menuTachiePrefix}/{boss.ToString()}_head_paint";
                Debug.Log($"ScenarioDictStruct: {tachieFileName}, {headFileName}");
                bossNameMultiLanguage = namePairs;
            }
        }

        public const string GMObjectName = "Global Manager GO";

        public static Dictionary<SupportedBoss, ScenarioDictStruct> boss2meta = new Dictionary<SupportedBoss, ScenarioDictStruct>()
        {
           { SupportedBoss.Shiva_Unreal, new ScenarioDictStruct(
               SupportedBoss.Shiva_Unreal,
           typeof(ShivaUnrealScenario),
           new List<Strategy>(){
               new ShivaUnrealZiyanStrat()
           },
           new Dictionary<SupportedLang,string>(){
               {SupportedLang.CHN, "幻希瓦"}
           })
           },
        };



        public static Sprite GetStratPosIconSprite(SinglePlayer.StratPosition position)
        {
            string s = $"job_icon/{position.ToString()}_icon";
            Sprite sprite = Resources.Load<Sprite>(s);
            return sprite;
        }

    }
}

