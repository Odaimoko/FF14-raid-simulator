using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public static class BM
    {
        public static string Tag = "BattleManager";
    }

    public static class UI
    {
        public static float StatusListXInterval = 17;
        public static float StatusListXStart = -48;
        public static float StatusListYStart = 3.3f;
        public static float StatusIconScale = 0.7f;

        public static string PartyListItemPrefix = "Party list item ";
        public static float PartyListYStart = -25;
        public static float PartyListYInterval = 35;
    }

    public static class Battle
    {
        public const float inBattleDistance = 10f;
        public const float minAtkDistance = 3f;
        public const float raidWideDistance = 80f;
    }
}

