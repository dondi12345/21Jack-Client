using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text timeTxt;
    public Image process;
    void Start()
    {
        
    }
    public void SetTime(int timeCount)
    {
        timeTxt.text = timeCount.ToString();
        process.fillAmount = (float)(timeCount) / 180;
    }
   
}
