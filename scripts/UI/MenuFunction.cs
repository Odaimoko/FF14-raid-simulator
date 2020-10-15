using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuFunction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBossButtonClicked()
    {
        // show boss tachie
        // change dropdown options
        
    }

    public void LoadBattleScene()
    {
        SceneManager.LoadScene("Battle", LoadSceneMode.Additive);

    }

    public void ChangeDropdownOptions()
    {

    }
}
