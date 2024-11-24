using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class TextEffect : MonoBehaviour
{
    public Text txtContent;
    public Animator anim;
    List<string> lsStringColor;
    // Start is called before the first frame update
    void Start()
    {
        lsStringColor = new List<string>() { "#5b5b5b", "#9d12b5", "#983607", "#0f860c", "#125abb", "#b00807", "#d22320 ", "#d22320 ", "#d22320 ", "#d22320 ", "#d22320 ", "#d22320 ", "#d22320 " };

    }
    public void SetUpText(Color color,int number, float delayTime = 0,UnityEngine.Events.UnityAction callback = null)
    {
        txtContent.color = color;
        StartCoroutine(InActive(number, delayTime,callback));
        //Invoke("InActive", 1);
    }
    IEnumerator InActive(int number,float delayTime, UnityEngine.Events.UnityAction callback = null)
    {
        txtContent.text = "";
        yield return new WaitForSeconds(delayTime);
        anim.Play("TextAnim");
        txtContent.text = number.ToString();
        //Color myColor = new Color();
        //ColorUtility.TryParseHtmlString(lsStringColor[1], out myColor);
        //txtContent.color = myColor;
        transform.DOMoveY(transform.position.y + Random.Range(1,1.5f), .6f)
            .OnComplete(() => {
                if(callback!=null)
                    callback();
                gameObject.SetActive(false);
            });
    }
}
