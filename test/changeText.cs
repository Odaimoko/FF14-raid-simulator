using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class changeText : MonoBehaviour
{

    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeText(float value)
    {
        text.text = value.ToString();
    }

    public void ChangeText(){
        Debug.Log("Doing nothing.");
    }
}
