using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public List<SinglePlayer> players = new List<SinglePlayer>();
    public SinglePlayer controlledPlayer;

    public GameObject canvas;
    public GameObject partyList;
    public List<GameObject> partylistItems = new List<GameObject>();
    public GameObject statusListGO;
    public GameObject statusIconPrefab;
    public Dictionary<int, StatusSet> statusSets = new Dictionary<int, StatusSet>();
    void Start()
    {
        RegisterEntities();
        InitPartylist();
        OnStatusListChange();
        InitTargetInfo();
    }

    void Update()
    {
        UpdateStatusList();
        UpdatePartyList();
        UpdateTargetInfo();
    }

    public void InitPartylist(int numPlayers = 8)
    {
        // partylistItems[i] is according to the order in default
        // change the position of the item, not the assignment
        int controlled_player = (int)controlledPlayer.stratPosition;
        Debug.Log($"UIManager InitPartylist: Finding {Constants.UI.PartyListItemPrefix + controlled_player}...");
        int pos = 0;
        for (int i = 0; i < numPlayers; i++)
        {
            if (i < controlled_player)
                pos = i + 1;
            else if (i == controlled_player) pos = 0;
            else pos = i;

            float offset = Constants.UI.PartyListYStart - pos * Constants.UI.PartyListYInterval;
            Debug.Log($"UIManager InitPartylist: player {i}, should be at pos {pos}, offset {offset}");
            RectTransform t = partyList.transform.Find(Constants.UI.PartyListItemPrefix + i).GetComponent<RectTransform>();
            // Set position on Canvas
            t.anchoredPosition = new Vector2(t.anchoredPosition.x, offset);
            // set name
            TextMeshProUGUI text = t.Find("member name").GetComponent<TextMeshProUGUI>();
            text.text = ((SinglePlayer.StratPosition)i).ToString();
            partylistItems.Add(t.gameObject);
        }
    }

    public void UpdatePartyList()
    {

    }
    public void OnStatusListChange()
    {
        int i = 0;
        Debug.Log($"UIManager OnStatusListChange Start. {controlledPlayer.statusGroups.Count} Groups.", this.gameObject);
        foreach (StatusGroup statusGroup in controlledPlayer.statusGroups)
        {
            foreach (SingleStatus status in statusGroup.statuses)
            {
                Debug.Log($"UIManager OnStatusListChange Has Key {status.statusName}/{status.GetHashCode()}: {statusSets.ContainsKey(status.GetHashCode())}");
                if (!status.showIcon || statusSets.ContainsKey(status.GetHashCode()))
                    continue;
                Debug.Log($"UIManager OnStatusListChange {status.statusName}", this.gameObject);
                Vector2 offset = GetIconOffset(i);
                GameObject icon = Instantiate(statusIconPrefab, statusListGO.transform.position, statusListGO.transform.rotation);
                icon.name = status.statusName;
                // set its parent to Status list or it won not appear
                icon.transform.SetParent(statusListGO.transform);
                RectTransform rt = icon.GetComponent<RectTransform>();
                // set position
                rt.anchoredPosition = offset;
                // set icon
                Image image = icon.GetComponent<Image>();
                image.sprite = status.icon;
                Debug.Log($"UIManager OnStatusListChange: Set icon {status.icon} to {image}", this.gameObject);
                // set scale
                Vector3 scale = rt.localScale;
                scale.y = scale.x = Constants.UI.StatusIconScale;
                rt.localScale = scale;
                // set countdown
                TextMeshProUGUI cdText = icon.transform.Find("countdown").GetComponent<TextMeshProUGUI>();
                cdText.text = Mathf.CeilToInt(status.countdown).ToString();
                statusSets.Add(status.GetHashCode(), new StatusSet(icon, status));
                i++;
            }
        }
    }

    public void UpdateStatusList()
    {
        // update current status
        List<int> toRemove = new List<int>();
        foreach (StatusSet s in statusSets.Values)
        {
            if (s.singleStatus.expired)
            {
                Debug.Log($"UIManager UpdateStatusList: {s.singleStatus.statusName} has expired.", this.gameObject);
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
            StatusSet s = statusSets[j];
            Debug.Log($"UIManager UpdateStatusList: Removing {s.singleStatus.statusName}.", this.gameObject);
            statusSets.Remove(s.singleStatus.GetHashCode());
            Destroy(s.icon);
        }
        int i = 0;
        foreach (StatusSet s in statusSets.Values)
        {
            // reset position
            RectTransform rt = s.icon.GetComponent<RectTransform>();
            rt.anchoredPosition = GetIconOffset(i);
            i++;
        }
    }


    Vector2 GetIconOffset(int i)
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

    void ResetAll()
    {

    }
}
