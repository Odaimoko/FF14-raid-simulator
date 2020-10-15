using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Volpi.ObjectyPool;

public class DamageTextFollower : MonoBehaviour
{
    private Vector3 movementY = Vector3.zero;
    private GameObject damageGO, statusGO;
    private GameObject icon, description;
    public bool isDamageInfo = true;
    private bool faded = false, startMoving = false;
    private CanvasGroup canvasGroup;
    void Start()
    {


    }

    public void Init(Transform par, int dmg = -1, SingleStatus status = null)
    {
        Debug.Log($"DamageTextFollower Init: {gameObject.name}. Damage: {dmg}. Status: {status}");
        // RectTransform rect = GetComponent<RectTransform>();

        //
        // ─── GAMEOBJECT INIT ─────────────────────────────────────────────
        //
        damageGO = transform.Find("damage text").gameObject;
        statusGO = transform.Find("status attach").gameObject;
        Debug.Log($"DamageTextFollower Start: {damageGO.name}. ");
        icon = statusGO.transform.Find("icon").gameObject;
        description = statusGO.transform.Find("description").gameObject;
        //
        // ─── COMPONENT INIT ─────────────────────────────────────────────────────────────
        //

        canvasGroup = GetComponent<CanvasGroup>();
        ResetStatus();
        startMoving = true;
        // reset anchor position and anchored position of rect transform
        RectTransform rect = GetComponent<RectTransform>();
        rect.SetParent(par.transform, false);
        rect.anchorMax = Constants.UI.MiddleCenterAnchor;
        rect.anchorMin = Constants.UI.MiddleCenterAnchor;
        rect.anchoredPosition = Vector3.zero;
        rect.localEulerAngles = Vector3.zero;

        if (isDamageInfo)
        {
            // Set damage text  
            Debug.Log($"DamageTextFollower Init: damageGO: {damageGO}. statusGO: {statusGO}.");
            damageGO.SetActive(true);
            statusGO.SetActive(false);
            TextMeshProUGUI dmgText = damageGO.GetComponent<TextMeshProUGUI>();
            if (dmg > 0)
            {
                // red font
                dmgText.text = "-";
                dmgText.color = Constants.UI.DamageColor;
            }
            else
            {
                dmgText.text = "+";
                dmgText.color = Constants.UI.HealingColor;
            }
            dmgText.text += Mathf.Abs(dmg);
        }
        else
        {
            statusGO.SetActive(true);
            damageGO.SetActive(false);
            RectTransform rt = icon.GetComponent<RectTransform>();
            Image image = icon.GetComponent<Image>();
            image.sprite = status.icon;
            TextMeshProUGUI statusText = damageGO.GetComponent<TextMeshProUGUI>();
            if (status.expired)
            {
                statusText.text = "-";
            }
            else
                statusText.text = "+";
            statusText.text += status.statusName;
        }
    }

    private void Update()
    {
        if (startMoving)
        {
            if (Mathf.Abs(movementY.y) < Constants.UI.DamageMovementYLimit)
            {
                Vector3 movementPerFrame = Vector3.down * Time.deltaTime * Constants.UI.DamageMovementSpeed;
                movementY += movementPerFrame;
                // Debug.Log($"DamageTextFollower Update: Total Movement {movementY}.");
                transform.Translate(movementPerFrame, Space.Self);
            }
            if (Mathf.Abs(movementY.y) > Constants.UI.DamageStartFade && !faded && canvasGroup.alpha == 1)
                StartCoroutine("Fade");
        }
    }


    IEnumerator Fade()
    {
        while (true)
        {
            // Debug.Log($"DamageTextFollower: {Constants.UI.DamageFadeInterval} Reduce Alpha to {canvasGroup.alpha - Constants.UI.DamageFadeSpeed}");
            canvasGroup.alpha -= Constants.UI.DamageFadeSpeed;
            if (canvasGroup.alpha <= 0)
            {
                faded = true;
                StopCoroutine("Fade");
                ObjectyManager.Instance.ObjectyPools[Constants.UI.DamageInfoPoolName].Despawn(Constants.UI.DamageInfoPoolSpawningName, gameObject);

            }
            yield return new WaitForSeconds(Constants.UI.DamageFadeInterval);
        }
    }

    public void ResetStatus()
    {
        Debug.Log($"DamageTextFollower Reset: {gameObject.name}.");
        movementY = Vector3.zero;
        Transform rect = GetComponent<Transform>();
        rect.position = Vector3.zero;
        canvasGroup.alpha = 1;
        faded = false;
        startMoving = false;
    }
}
