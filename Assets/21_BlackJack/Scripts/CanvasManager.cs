using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.orientation = ScreenOrientation.Portrait;
        GetComponent<CanvasScaler>().matchWidthOrHeight = ratio();

    }

    float ratio()
    {
        float rScene = (float)Screen.width / (float)Screen.height;
        if (rScene > (float)1028 / (float)1920) rScene = 1;
        else rScene = 0;
        return rScene;
    }
}
