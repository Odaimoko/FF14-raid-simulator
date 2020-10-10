using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public class PartyListItem
    {
        public SinglePlayer player;
        public GameObject partyListItem;
        public GameObject statuslist;
        public Dictionary<int, StatusSet> statusSets = new Dictionary<int, StatusSet>();
        public GameObject hpGauge, hpFiller, hpValue, hpShield;
        public PartyListItem(GameObject self, SinglePlayer p)
        {
            player = p;
            partyListItem = self;

            hpGauge = partyListItem.transform.Find("hp gauge").gameObject;
            hpFiller = hpGauge.transform.Find("filler").gameObject;
            hpValue = hpGauge.transform.Find("value").gameObject;
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
                TextMeshProUGUI t = hpValue.GetComponent<TextMeshProUGUI>();
                t.text = player.healthPoint.ToString();
                RectTransform rect = hpFiller.GetComponent<RectTransform>();
                Vector3 scale = rect.localScale;
                scale.x = (float)player.healthPoint / player.maxHP;
                rect.localScale = scale;
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
                UIManager.OnStatusListChange(statuslist, player, statusSets, 5);
        }
    }
    public List<Enemy> enemies = new List<Enemy>();
    public List<SinglePlayer> players = new List<SinglePlayer>();
    public SinglePlayer controlledPlayer;

    public GameObject canvas;
    public GameObject partyListGO;
    public List<PartyListItem> partylistItems = new List<PartyListItem>();
    public GameObject statusListGO;
    [SerializeField]
    public static GameObject statusIconPrefab;
    public Dictionary<int, StatusSet> statusSets = new Dictionary<int, StatusSet>();
    void Start()
    {
        statusIconPrefab = Resources.Load<GameObject>("battle_status/Status_self");
        RegisterEntities();
        InitPartylist();
        OnStatusListChange();
        InitTargetInfo();
    }

    void Update()
    {
        UpdateStatusList(statusSets);
        UpdatePartyList();
        UpdateTargetInfo();
    }

    public void InitPartylist(int numPlayers = 8)
    {
        // partylistItems[i] is according to the order in default
        // change the position of the item, not the assignment
        int controlled_player = (int)controlledPlayer.stratPosition;
        Debug.Log($"UIManager InitPartylist: Finding {Constants.UI.PartyListItemPrefix + controlled_player}...");
        // Position
        int pos = 0;
        for (int i = 0; i < numPlayers; i++)
        {
            if (i < controlled_player)
                pos = i + 1;
            else if (i == controlled_player) pos = 0;
            else pos = i;

            float offset = Constants.UI.PartyListYStart - pos * Constants.UI.PartyListYInterval;
            Debug.Log($"UIManager InitPartylist: player {i}, should be at pos {pos}, offset {offset}");
            RectTransform t = partyListGO.transform.Find(Constants.UI.PartyListItemPrefix + i).GetComponent<RectTransform>();
            // Set position on Canvas
            t.anchoredPosition = new Vector2(t.anchoredPosition.x, offset);
            // set name
            TextMeshProUGUI text = t.Find("member name").GetComponent<TextMeshProUGUI>();
            text.text = ((SinglePlayer.StratPosition)i).ToString();
            // other info
            GameObject partylistitem = t.gameObject;
            SinglePlayer pl = FindPlayerByStratPos(i);
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

    public static void OnStatusListChange(GameObject statusList, SinglePlayer pl, Dictionary<int, StatusSet> sets, int maxIcons)
    {
        int i = 0;
        Debug.Log($"UIManager OnStatusListChange Start. {pl.statusGroups.Count} Groups.");
        foreach (StatusGroup statusGroup in pl.statusGroups)
        {
            foreach (SingleStatus status in statusGroup.statuses)
            {
                Debug.Log($"UIManager OnStatusListChange Has Key {status.statusName}/{status.GetHashCode()}: {sets.ContainsKey(status.GetHashCode())}");
                if (!status.showIcon)
                    continue;
                if (!sets.ContainsKey(status.GetHashCode()))
                {
                    Debug.Log($"UIManager OnStatusListChange {status.statusName}");
                    Vector2 offset = GetIconOffset(i);
                    GameObject icon = Instantiate(statusIconPrefab, statusList.transform.position, statusList.transform.rotation);
                    icon.name = status.statusName;
                    // set its parent to Status list or it won not appear
                    icon.transform.SetParent(statusList.transform);
                    RectTransform rt = icon.GetComponent<RectTransform>();
                    // set position
                    rt.anchoredPosition = offset;
                    // set icon
                    Image image = icon.GetComponent<Image>();
                    image.sprite = status.icon;
                    Debug.Log($"UIManager OnStatusListChange: Set icon {status.icon} to {image}");
                    // set scale
                    Vector3 scale = rt.localScale;
                    scale.y = scale.x = Constants.UI.StatusIconScale;
                    rt.localScale = scale;
                    // set countdown
                    TextMeshProUGUI cdText = icon.transform.Find("countdown").GetComponent<TextMeshProUGUI>();
                    cdText.text = Mathf.CeilToInt(status.countdown).ToString();
                    sets.Add(status.GetHashCode(), new StatusSet(icon, status));
                }
                i++;
                if (i == maxIcons) return;
            }
        }
    }

    public void OnStatusListChange()
    {
        OnStatusListChange(statusListGO, controlledPlayer, statusSets, 20);
        foreach (PartyListItem item in partylistItems)
        {
            item.OnStatusListChange();
        }
    }

    public static void UpdateStatusList(Dictionary<int, StatusSet> set)
    {
        // update current status
        List<int> toRemove = new List<int>();
        foreach (StatusSet s in set.Values)
        {
            if (s.singleStatus.expired)
            {
                Debug.Log($"UIManager UpdateStatusList: {s.singleStatus.statusName} has expired.");
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
            Debug.Log($"UIManager UpdateStatusList: Removing {s.singleStatus.statusName}.");
            set.Remove(s.singleStatus.GetHashCode());
            Destroy(s.icon);
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
        GameObject target = controlledPlayer.target;
        if (target != null)
        {
            Debug.Log($"UIManager InitTargetInfo {target} ", this.gameObject);

        }
        else
            Debug.Log($"UIManager InitTargetInfo: Target is Null.", this.gameObject);
    }
    public void UpdateTargetInfo()
    {

    }

    void RegisterEntities()
    {
        // Find enemies and players in the scene
        foreach (GameObject en in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(en.GetComponent<Enemy>());
        }
        foreach (GameObject pl in GameObject.FindGameObjectsWithTag("Player"))
        {
            SinglePlayer sp = pl.GetComponent<SinglePlayer>();
            if (sp.controller.controllable)
            {
                controlledPlayer = sp;
            }
            players.Add(sp);
        }
        Debug.Log("UIManager register enemy " + enemies.Count, this.gameObject);
        Debug.Log("UIManager register players " + players.Count, this.gameObject);
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
