using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TouchTest : MonoBehaviour
{
    public TextMeshProUGUI text;
    private float startTime = 0;
    private Vector2 startPos, endPos;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "TT";
    }

    // Update is called once per frame
    void Update()
    {
# if UNITY_ANDROID
        Debug.Log("Android");
# endif
# if UNITY_IOS
        Debug.Log("IOS");
# endif
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            // Debug.Log(t.phase.ToString());
            Debug.Log(t.fingerId);
            Debug.Log(t.radius);
            Debug.Log(t.tapCount);
            switch (t.phase)

            {
                case TouchPhase.Began:
                    startPos = t.position;
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    endPos = t.position;
                    break;
                default: break;
            }
            // text.text = "Direction Vector: " + (endPos - startPos).ToString();

            ShowMultiFingerInfo();
        }
    }

    void ShowMultiFingerInfo()
    {
        string multiTouchInfo = "";
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch theTouch = Input.GetTouch(i);
            multiTouchInfo +=
                string.Format("Touch {0} - Position {1} - Tap Count: {2} - Finger ID: {3}\nRadius: {4} ({5}%)\n",
                    i, theTouch.position, theTouch.tapCount, theTouch.fingerId, theTouch.radius,
                    ((theTouch.radius / (theTouch.radius + theTouch.radiusVariance)) * 100f).ToString("F1"));
        }
        text.text = multiTouchInfo;
        Debug.Log(multiTouchInfo);
    }
}
