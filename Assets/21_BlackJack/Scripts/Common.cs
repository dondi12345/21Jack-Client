using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine.UI;

public class Common : MonoBehaviour
{
    
    public static void MoveObjectToPosition(GameObject go, GameObject desPos,float speed,UnityAction callbaclk = null)
    {
        go.transform.DOKill();
        go.transform.DOMove( desPos.transform.position, speed)
           .SetEase(Ease.Linear)
           .OnComplete(() => {
               callbaclk();
           });
    }
    public static void MoveObjectToPosition(GameObject go, Vector2 desPos, float speed, UnityAction callbaclk = null)
    {
        go.transform.DOKill();
        go.transform.DOMove(desPos, speed)
           .SetEase(Ease.Linear)
           .OnComplete(() => {
               callbaclk();
           });
    }
  
    //public static GameObject SpawnCoin( Vector2 spawnPos,int number =1)
    //{
    //    GameObject go = ObjectPool.Spawn("CoinDrop", spawnPos);
    //    go.transform.DOMove(new Vector2(spawnPos.x + Random.Range(-.5f, .5f), spawnPos.y + Random.Range(-.5f, .5f)), .3f);
    //    go.transform.localScale = 0.1f * Vector2.one;
    //    return go;
    //}

   
 
    public static Sprite GetAvatar(string id)
    {
        return Resources.Load<Sprite>("IconCard/" + id);
    }
    public static IEnumerator CurrencyChange(Text textToDisplay, double orgNum, double desNum, float timePlay, bool DisplayZero)
    {
        float timeSinceStarted = 0f;
        while (true)
        {
            timeSinceStarted += Time.deltaTime;
            double fracJourney = (double)(timeSinceStarted / timePlay);
            if (fracJourney > 1)
                fracJourney = 1;
            double curNum = (desNum - orgNum) * fracJourney + orgNum;
            textToDisplay.text = System.Math.Truncate(curNum).ToString();// GetFriendlyShortNumber(System.Math.Truncate(curNum));
            // If the object has arrived, stop the coroutine 
            if (curNum == desNum)
            {
                if (desNum == 0 && !DisplayZero)
                    textToDisplay.text = "0";
                textToDisplay.text = desNum.ToString();// GetFriendlyShortNumber(System.Math.Truncate(desNum));
                yield break;
            }
            yield return null;
        }

    }
    public static string GetFriendlyShortNumber(double inputNumber)
    {
        string retval = "";
        if (inputNumber < 1000)
        {
            retval = inputNumber.ToString("0.##");
        }
        else if (inputNumber < 1000000)
        {
            retval = (inputNumber / 1000).ToString("0.##") + "K";
        }
        else if (inputNumber < 1000000000)
        {
            retval = (inputNumber / 1000000).ToString("0.##") + "M";
        }
        else
        {
            retval = (inputNumber / 1000000000).ToString("0.##") + "B";
        }
        return retval;
    }
   
    public static void DoScale(Transform go)
    {
        go.transform.localScale = Vector3.zero;
        go.DOScale(1, .3f);
    }
   
    public static Color GetColerByIndex(int index, List<string> lsStringColor)
    {
        Color myColor = new Color();
        ColorUtility.TryParseHtmlString(lsStringColor[index], out myColor);
        return myColor;
    }
    
}
