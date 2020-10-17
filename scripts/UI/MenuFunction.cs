using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class MenuFunction : MonoBehaviour
{
    private GlobalGameManager gameManager;

    public Canvas uiCanvas;
    // Start is called before the first frame update
    public TMP_Dropdown bossDropdown, posDropdown, stratDropdown, phaseDropdown;
    public Animator canvasAnimator;

    private Image tachieImage;
    void Start()
    {
        gameManager = GameObject.Find("Global Manager GO").GetComponent<GlobalGameManager>();
        tachieImage = GameObject.Find("boss tachie").GetComponent<Image>();
        InitDropdown();
    }


    void InitDropdown()
    {
        // bosses
        InitBossDropdown();
        // Position
        InitPositionDropDown();
        // Strategies
        InitStratDropdown();
        // Phases
        InitPhaseDropdown();
    }

    public void OnBossChange(int bossCode)
    {
        ChangeBossTachie(bossCode);
        InitStratDropdown();
        InitPhaseDropdown();
    }

    void InitBossDropdown()
    {

        bossDropdown.ClearOptions();
        List<string> bossCodes = new List<string>(System.Enum.GetNames(typeof(SupportedBoss)));
        List<string> bossNames = new List<string>();
        for (int i = 0; i < bossCodes.Count; i++)
        {
            var dictStruct = Constants.GameSystem.boss2meta[(SupportedBoss)i];
            bossNames.Add(dictStruct.bossNameMultiLanguage[gameManager.lang]);
        }
        bossDropdown.AddOptions(bossNames);
        for (int i = 0; i < bossDropdown.options.Count; i++)
        {
            TMP_Dropdown.OptionData option = bossDropdown.options[i];
            var dictStruct = Constants.GameSystem.boss2meta[(SupportedBoss)i];
            Sprite sprite = Resources.Load<Sprite>(dictStruct.headFileName);
            option.image = sprite;
        }
    }

    void InitPositionDropDown()
    {
        posDropdown.ClearOptions();
        List<string> stratPoses = new List<string>(System.Enum.GetNames(typeof(SinglePlayer.StratPosition)));
        posDropdown.AddOptions(stratPoses);
        for (int i = 0; i < posDropdown.options.Count; i++)
        {
            TMP_Dropdown.OptionData option = posDropdown.options[i];
            // Sprite sprite = Constants.GameSystem.GetStratPosIconSprite((SinglePlayer.StratPosition)i);
            // option.image = sprite;
        }
    }

    void InitStratDropdown()
    {
        stratDropdown.ClearOptions();
        SupportedBoss boss = (SupportedBoss)bossDropdown.value;
        var dictStruct = Constants.GameSystem.boss2meta[boss];
        var strats = dictStruct.strats;
        List<string> stratNames = new List<string>();
        foreach (Strategy strategy in strats)
        {
            stratNames.Add(strategy.name);
        }
        stratDropdown.AddOptions(stratNames);
    }

    void InitPhaseDropdown()
    {
        phaseDropdown.ClearOptions();
        var dictStruct = Constants.GameSystem.boss2meta[(SupportedBoss)bossDropdown.value];
        Strategy s = dictStruct.strats[stratDropdown.value];
        Debug.Log($"InitPhaseDropdown: {s} has {s.supportedPhases.Count} phases.");
        List<string> phaseNames = new List<string>();
        for (int i = 0; i < s.supportedPhases.Count; i++)
        {

            BattlePhase phase = s.supportedPhases[i];
            Debug.Log($"InitPhaseDropdown: Check phase info, {phase.ToString()}");
            if (phase.chosableInMenu)
            {
                string name = $"P{i}： {phase.name}";
                phaseNames.Add(name);
            }
        }
        phaseDropdown.AddOptions(phaseNames);
    }

    public void FadeOutUI()
    {
        Debug.Log($"MenuFunction FadeOutUI");
        CanvasGroup uiCanvasGroup = uiCanvas.GetComponent<CanvasGroup>();
        uiCanvasGroup.interactable = false;
        canvasAnimator.SetTrigger("FadeOut");
    }

    public void LoadBattleScene()
    {
        gameManager.LoadScene(gameObject.scene, "Battle");

    }

    private void ChangeBossTachie(int bossCode)
    {
        // show boss tachie
        // change dropdown options
        SupportedBoss boss = (SupportedBoss)bossCode;
        tachieImage.sprite = Resources.Load<Sprite>(Constants.GameSystem.boss2meta[boss].tachieFileName);
        tachieImage.SetNativeSize();
        tachieImage.transform.localScale = Constants.GameSystem.boss2meta[boss].tachieLocalScale;
    }
}
