using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public List<StatusSet> statusSets = new List<StatusSet>();
    void Start()
    {
        RegisterEntities();
        InitPartylist();
        InitStatusList();
    }

    void Update()
    {
        UpdateStatusList();
    }

    public void InitPartylist(int numPlayers = 8)
    {
        // partylistItems[i] is according to the order in default
        // change the position of the item, not the assignment
        int controlled_player = (int)controlledPlayer.stratPosition;
        Debug.Log($"UIManager InitPartylist: Finding {Constants.UI_PartyListItemPrefix + controlled_player}...");
        int pos = 0;
        for (int i = 0; i < numPlayers; i++)
        {
            if (i < controlled_player)
                pos = i + 1;
            else if (i == controlled_player) pos = 0;
            else pos = i;

            float offset = Constants.UI_PartyListYStart - pos * Constants.UI_PartyListYInterval;
            Debug.Log($"UIManager InitPartylist: player {i}, should be at pos {pos}, offset {offset}");
            RectTransform t = partyList.transform.Find(Constants.UI_PartyListItemPrefix + i).GetComponent<RectTransform>();
            // Set position on Canvas
            t.anchoredPosition = new Vector2(t.anchoredPosition.x, offset);
            // set name
            TextMeshProUGUI text = t.Find("member name").GetComponent<TextMeshProUGUI>();
            text.text = ((SinglePlayer.StratPosition)i).ToString();
            partylistItems.Add(t.gameObject);
        }
    }

    public void InitStatusList()
    {
        int i = 0;
        foreach (StatusGroup statusGroup in controlledPlayer.statusGroups)
        {

            Debug.Log($"UIManager InitStatusList {statusGroup}", this.gameObject);
            foreach (SingleStatus status in statusGroup.statuses)
            {
                Vector2 offset = GetIconOffset(i);
                GameObject icon = Instantiate(statusIconPrefab, statusListGO.transform.position, statusListGO.transform.rotation);
                // set its parent to Status list or it won not appear
                icon.transform.SetParent(statusListGO.transform);
                RectTransform rt = icon.GetComponent<RectTransform>();
                rt.anchoredPosition = offset;
                // set scale
                Vector3 scale = rt.localScale;
                scale.y = scale.x = Constants.UI_StatusIconScale;
                rt.localScale = scale;
                // set countdown
                TextMeshProUGUI cdText = icon.transform.Find("countdown").GetComponent<TextMeshProUGUI>();
                cdText.text = Mathf.CeilToInt(status.countdown).ToString();
                statusSets.Add(new StatusSet(icon, status));
                i++;
            }
        }
    }

    public void UpdateStatusList()
    {
        // update current status
        int numStatuses = statusSets.Count;
        List<StatusSet> toRemove = new List<StatusSet>();
        foreach (StatusSet s in statusSets)
        {
            if (s.singleStatus.expired)
            {

                Debug.Log($"UIManager UpdateStatusList: {s} has expired.", this.gameObject);
                toRemove.Add(s);
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
        foreach (StatusSet s in toRemove)
        {
            Debug.Log($"UIManager UpdateStatusList: Removing {s}.", this.gameObject);
            Destroy(s.icon);
            statusSets.Remove(s);
        }
        for (int i = 0; i < statusSets.Count; i++)
        {
            // reset position
            StatusSet s = statusSets[i];
            RectTransform rt = s.icon.GetComponent<RectTransform>();
            rt.anchoredPosition = GetIconOffset(i);
        }
    }

    Vector2 GetIconOffset(int i)
    {
        Vector2 offset = new Vector2(i * Constants.UI_StatusListXInterval + Constants.UI_StatusListXStart,
                                     Constants.UI_StatusListYStart);
        return offset;
    }

    void AddIconToStatusList(SingleStatus status)
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
