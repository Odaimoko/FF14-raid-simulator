using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class MenuFunction : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Dropdown bossDropdown, posDropdown, stratDropdown, phaseDropdown;
    void Start()
    {
        InitDropdown();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBossDropdownChosen()
    {
        // show boss tachie
        // change dropdown options

    }

    void InitDropdown()
    {
        // Position
        List<string> stratPoses = new List<string>(System.Enum.GetNames(typeof(SinglePlayer.StratPosition)));
        posDropdown.AddOptions(stratPoses);
        for (int i = 0; i < posDropdown.options.Count; i++)
        {
            TMP_Dropdown.OptionData option = posDropdown.options[i];
            Sprite sprite = Constants.GameSystem.GetStratPosIconSprite((SinglePlayer.StratPosition)i);
            option.image = sprite;
        }
    }

    void InitStratDropdown()
    {

    }

    void InitPhaseDropdown()
    {

    }

    public void LoadBattleScene()
    {
        SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
    }

    public void ChangeDropdownOptions()
    {

    }
}
