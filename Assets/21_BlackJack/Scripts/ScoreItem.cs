using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ScoreItem : MonoBehaviour
{
    public Image scoreProcess;
    public Text scoreTxt;
    int oriScore = 0;
    public void SetScore(int score)
    {
        if (oriScore != score)
        {
            scoreProcess.DOFillAmount((float)score / 21, 0.4f);
            scoreTxt.transform.localScale = new Vector2(2f, 2f);
            scoreTxt.transform.DOScale(Vector2.one, 30)
                .SetSpeedBased(true)
                .SetEase(Ease.InOutSine)
                .SetDelay(.2f)
                ;
            StartCoroutine(Common.CurrencyChange(scoreTxt, oriScore, score, .4f, false));
            oriScore = score;
        }
       
    }
}
