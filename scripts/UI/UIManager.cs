using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Volpi.ObjectyPool;
public class UIManager : MonoBehaviour
{
    public class PartyListItem
    {
        public SinglePlayer player;
        public GameObject partyListItem;
        public GameObject statuslist;
        public Dictionary<int, StatusSet> statusSets = new Dictionary<int, StatusSet>();
        public TextMeshProUGUI hpValue;
        public RectTransform hpFiller;
        public Image jobIcon;
        public GameObject hpGauge, hpShield;
        public PartyListItem(GameObject self, SinglePlayer p)
        {
            player = p;
            partyListItem = self;

            jobIcon = partyListItem.transform.Find("job icon").gameObject.GetComponent<Image>();
            if (player != null)
            {
                jobIcon.sprite = Constants.GameSystem.GetSpriteByStratPos(player.stratPosition);
            }

            hpGauge = partyListItem.transform.Find("hp gauge").gameObject;
            hpFiller = hpGauge.transform.Find("filler").gameObject.GetComponent<RectTransform>();
            hpValue = hpGauge.transform.Find("value").gameObject.GetComponent<TextMeshProUGUI>();
            hpShield = hpGauge.transform.Find("shield").gameObject;
            statuslist = partyListItem.transform.Find("status").gameObject;
        }

        public void Update(bool update = true)
        {
            if (player)
            {
                // Debug.Log($"PartyListItem Update {update}: {player.stratPosition}");
                // HP
                // TODO: SHIELD
                hpValue.text = player.healthPoint.ToString();
                Vector3 scale = hpFiller.localScale;
                scale.x = (float)player.healthPoint / player.maxHP;
                hpFiller.localScale = scale;
                // Status
                if (update)
                    UpdateStatusList();
                else
                    OnStatusListChange();
            }
            else
            {
                // Debug.Log($"PartyListItem Update {update}: {partyListItem.name}. No Player Attached.");
            }
        }

        public void UpdateStatusList()
        {
            UIManager.UpdateStatusList(statusSets);
        }

        public void OnStatusListChange()
        {
            if (player)
                UIManager.OnStatusListChange(statuslist, player, statusSets, 5, false);
        }
    }
    public List<Enemy> enemies = new List<Enemy>();
    public List<SinglePlayer> players = new List<SinglePlayer>();
    public SinglePlayer controlledPlayer;

    //
    // ─── PARTY LIST ─────────────────────────────────────────────────────────────────
    //
    public GameObject partyListGO;
    public List<PartyListItem> partylistItems = new List<PartyListItem>();


    //
    // ─── SELF STATUS LIST ───────────────────────────────────────────────────────────
    //
    public GameObject statusListGO;
    [SerializeField]
    public static GameObject statusIconPrefab;
    public Dictionary<int, StatusSet> selfStatusSets = new Dictionary<int, StatusSet>();
    //
    // ─── TARGET INFO ────────────────────────────────────────────────────────────────
    //

    public class TargetInfoClass
    {
        private TextMeshProUGUI bossName, hpPercent, moveName;
        private SinglePlayer controlledPlayer;
        private GameObject targetInfoGO, hpMask, castMask, castFrame, statusListGO;
        public Dictionary<int, StatusSet> statusSets = new Dictionary<int, StatusSet>();

        public TargetInfoClass(GameObject targetInfoGO, SinglePlayer controlledPlayer)
        {
            this.controlledPlayer = controlledPlayer;

            this.targetInfoGO = targetInfoGO;

            GameObject hpFrame = targetInfoGO.transform.Find("hp frame").gameObject;
            hpMask = hpFrame.transform.Find("hp mask").gameObject;
            bossName = hpFrame.transform.Find("boss name").GetComponent<TextMeshProUGUI>();
            hpPercent = hpFrame.transform.Find("hp percent").GetComponent<TextMeshProUGUI>();

            statusListGO = targetInfoGO.transform.Find("boss status list").gameObject;

            castFrame = targetInfoGO.transform.Find("cast frame").gameObject;
            castMask = castFrame.transform.Find("cast mask").gameObject;
            moveName = castFrame.transform.Find("move name").GetComponent<TextMeshProUGUI>();
            Update();
        }

        public void Update(bool update = false)
        {
            if (this.controlledPlayer.target == null)
            {
                targetInfoGO.SetActive(false);
            }
            else
            {
                targetInfoGO.SetActive(true);
                Entity target = controlledPlayer.target.GetComponent<Entity>();
                // Status
                if (!update)
                {
                    // init
                    foreach (StatusSet item in statusSets.Values)
                    {
                        UIManager.DestroyIconGO(item.icon);
                    }
                    statusSets.Clear();
                    UIManager.OnStatusListChange(statusListGO, target, statusSets, 5);
                }
                else
                {
                    UIManager.UpdateStatusList(statusSets);
                }
                // HP
                // Debug.Log($"UIM InitTargetInfo {target.name} ");

                bossName.text = target.name;

                RectTransform rect = hpMask.GetComponent<RectTransform>();
                Vector3 scale = rect.localScale;
                scale.x = (float)target.healthPoint / target.maxHP;
                rect.localScale = scale;
                float percent = (Mathf.CeilToInt(scale.x * 1000)) / 10;
                // Debug.Log($"UIM InitTargetInfo: Set HP percent {percent} ");
                hpPercent.text = percent.ToString() + "%"; // CastBar
                if (target.casting)
                {
                    castFrame.SetActive(true);
                    CastGroup sg = target.castingStatus;
                    // Debug.Log($"TargetInfoClass Init: StatusGroup has {sg.statuses.Count} statuses.");
                    SingleStatus s = sg.statuses[0];
                    RectTransform castRect = castMask.GetComponent<RectTransform>();
                    Vector3 castScale = castRect.localScale;
                    castScale.x = (float)s.countdown / s.duration;
                    castRect.localScale = castScale;

                    moveName.text = sg.actual.name;
                }
                else
                {
                    castFrame.SetActive(false);
                }
            }
        }

        public void OnStatusListChange()
        {

            if (this.controlledPlayer.target != null)
            {
                UIManager.OnStatusListChange(statusListGO, this.controlledPlayer.target.GetComponent<Entity>(), statusSets, 5);
            }
        }
    }
    public GameObject targetInfoGO;
    private TargetInfoClass targetInfo;
    void Start()
    {
        statusIconPrefab = Resources.Load<GameObject>("battle_status/Status_self");
        RegisterEntities();
        InitPartylist();
        InitTargetInfo();
        OnStatusListChange();
    }

    void Update()
    {
        UpdateStatusList(selfStatusSets);
        UpdatePartyList();
        UpdateTargetInfo();
    }

    public void InitPartylist(int numPlayers = 8)
    {
        // partylistItems[i] is according to the order in default
        // change the position of the item, not the assignment
        int controlled_player = (int)controlledPlayer.stratPosition;
        Debug.Log($"UIM InitPartylist: Finding {Constants.UI.PartyListItemPrefix + controlled_player}...");
        // Position
        int pos = 0;
        for (int i = 0; i < numPlayers; i++)
        {
            if (i < controlled_player)
                pos = i + 1;
            else if (i == controlled_player) pos = 0;
            else pos = i;

            float offset = Constants.UI.PartyListYStart - pos * Constants.UI.PartyListYInterval;
            Debug.Log($"UIM InitPartylist: player {i}, should be at pos {pos}, offset {offset}");
            RectTransform t = partyListGO.transform.Find(Constants.UI.PartyListItemPrefix + i).GetComponent<RectTransform>();
            // Set position on Canvas
            t.anchoredPosition = new Vector2(t.anchoredPosition.x, offset);
            // set name
            TextMeshProUGUI text = t.Find("member name").GetComponent<TextMeshProUGUI>();
            text.text = ((SinglePlayer.StratPosition)i).ToString();
            // other info
            GameObject partylistitem = t.gameObject;
            SinglePlayer pl = FindPlayerByStratPos(i); // can be null
            PartyListItem item = new PartyListItem(partylistitem, pl);
            item.Update(false);
            partylistItems.Add(item);
        }
    }

    public void UpdatePartyList()
    {
        foreach (PartyListItem item in partylistItems)
        {
            item.Update();
        }
    }

    public static void OnStatusListChange(GameObject statusList, Entity en, Dictionary<int, StatusSet> sets, int maxIcons, bool showCDText = true)
    {
        int i = 0;
        Debug.Log($"UIM OnStatusListChange: {statusList.transform.parent.name} Has {en.statusGroups.Count} Groups.");
        foreach (StatusGroup statusGroup in en.statusGroups)
        {
            foreach (SingleStatus status in statusGroup.statuses)
            {
                Debug.Log($"UIM OnStatusListChange: {statusList.transform.parent.name} Has Key {status.name}/{status.GetHashCode()}: {sets.ContainsKey(status.GetHashCode())}");
                if (!status.showIcon || status.expired)
                    continue;
                if (!sets.ContainsKey(status.GetHashCode()))
                {
                    Debug.Log($"UIM OnStatusListChange: {statusList.transform.parent.name}'s {status.name}");
                    Vector2 offset = GetIconOffset(i);
                    // GameObject icon = Instantiate(statusIconPrefab, statusList.transform.position, statusList.transform.rotation);
                    GameObject icon = GetNewIconGO();
                    // Object pool does not support custom GO name
                    // icon.name = status.statusName; 
                    // set its parent to Status list or it won not appear
                    icon.transform.SetParent(statusList.transform);
                    RectTransform rt = icon.GetComponent<RectTransform>();
                    // set position
                    rt.anchoredPosition = offset;
                    // set icon
                    Image image = icon.GetComponent<Image>();
                    image.sprite = status.icon;
                    // Debug.Log($"UIM OnStatusListChange: Set icon {status.icon} to {image}");
                    // set scale
                    Vector3 scale = rt.localScale;
                    scale.y = scale.x = Constants.UI.StatusIconScale;
                    rt.localScale = scale;
                    // set countdown
                    GameObject cdGO = icon.transform.Find("countdown").gameObject;
                    if (showCDText)
                    {
                        cdGO.SetActive(true);
                        TextMeshProUGUI cdText = cdGO.GetComponent<TextMeshProUGUI>();
                        cdText.text = Mathf.CeilToInt(status.countdown).ToString();
                    }
                    else
                        cdGO.SetActive(false);
                    sets.Add(status.GetHashCode(), new StatusSet(icon, status));
                }
                i++;
                if (i == maxIcons) return;
            }
        }
    }

    public static GameObject GetNewIconGO()
    {
        return ObjectyManager.Instance.ObjectyPools[Constants.UI.IconPoolGOName].Spawn(Constants.UI.IconPoolSpawningName);
    }
    public static void DestroyIconGO(GameObject icon)
    {
        ObjectyManager.Instance.ObjectyPools[Constants.UI.IconPoolGOName].Despawn(Constants.UI.IconPoolSpawningName, icon);
    }

    public void OnStatusListChange()
    {
        OnStatusListChange(statusListGO, controlledPlayer, selfStatusSets, 20);
        foreach (PartyListItem item in partylistItems)
        {
            item.OnStatusListChange();
        }
        targetInfo.OnStatusListChange();
    }

    public static void UpdateStatusList(Dictionary<int, StatusSet> set)
    {
        // update current status
        List<int> toRemove = new List<int>();
        foreach (StatusSet s in set.Values)
        {
            if (s.singleStatus.expired)
            {
                Debug.Log($"UIM UpdateStatusList: {s.singleStatus.name} has expired.");
                toRemove.Add(s.singleStatus.GetHashCode());
            }
            else
            {
                // update countdown text
                GameObject icon = s.icon;
                TextMeshProUGUI cdText = icon.transform.Find("countdown").GetComponent<TextMeshProUGUI>();
                cdText.text = Mathf.CeilToInt(s.singleStatus.countdown).ToString();
            }
        }
        // remove expired status
        foreach (int j in toRemove)
        {
            StatusSet s = set[j];
            Debug.Log($"UIM UpdateStatusList: Removing {s.singleStatus.name}.");
            set.Remove(s.singleStatus.GetHashCode());
            UIManager.DestroyIconGO(s.icon);
        }
        int i = 0;
        foreach (StatusSet s in set.Values)
        {
            // reset position
            RectTransform rt = s.icon.GetComponent<RectTransform>();
            rt.anchoredPosition = GetIconOffset(i);
            i++;
        }
    }


    public static Vector2 GetIconOffset(int i)
    {
        Vector2 offset = new Vector2(i * Constants.UI.StatusListXInterval + Constants.UI.StatusListXStart,
                                     Constants.UI.StatusListYStart);
        return offset;
    }

    // Use Player's enemy info.
    // Fixed: Since player cannot choose target now.
    public void InitTargetInfo()
    {
        targetInfo = new TargetInfoClass(targetInfoGO, controlledPlayer);
    }

    public void UpdateTargetInfo()
    {
        targetInfo.Update(true);
    }

    void RegisterEntities()
    {
        // Find enemies and players in the scene
        enemies.Clear();
        players.Clear();
        controlledPlayer = null;
        foreach (GameObject en in GameObject.FindGameObjectsWithTag(Constants.BM.EnemyTag))
        {
            enemies.Add(en.GetComponent<Enemy>());
        }
        foreach (GameObject pl in GameObject.FindGameObjectsWithTag(Constants.BM.PlayerTag))
        {
            SinglePlayer sp = pl.GetComponent<SinglePlayer>();
            if (sp.controller.controllable)
            {
                controlledPlayer = sp;
            }
            players.Add(sp);
        }
        Debug.Log("UIM register enemy " + enemies.Count, this.gameObject);
        Debug.Log("UIM register players " + players.Count, this.gameObject);
    }

    SinglePlayer FindPlayerByStratPos(int i)
    {
        SinglePlayer player = null;
        foreach (SinglePlayer pl in players)
        {
            if ((int)pl.stratPosition == i)
            {
                player = pl;
                break;
            }
        }
        return player;
    }

    void ResetAll()
    {

    }
}
