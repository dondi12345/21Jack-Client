using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BlackJackGame
{
    /// <summary>
    /// Buttons panel UI logic
    /// </summary>
    public class UIButtonsPanel : MonoBehaviour
    {

        public GameObject buttonHit;
        public GameObject buttonStand;
        public GameObject buttonDouble;
        public GameObject buttonSplit;


        // Use this for initialization
        void Start()
        {
            buttonHit.SetActive(false);
            buttonStand.SetActive(false);
            buttonDouble.SetActive(false);
            buttonSplit.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Hides the buttons.
        /// </summary>
        public void HideButtons()
        {
            buttonHit.transform.localScale = new Vector3(1, 1, 1);
            buttonStand.transform.localScale = new Vector3(1, 1, 1);
            buttonDouble.transform.localScale = new Vector3(1, 1, 1);
            buttonSplit.transform.localScale = new Vector3(1, 1, 1);

            buttonHit.transform.DOScale(0, .2f).OnComplete(()=> {
                buttonHit.SetActive(false);
            });
            buttonStand.transform.DOScale(0, .2f).OnComplete(() => {
                buttonStand.SetActive(false);
            });
            buttonDouble.transform.DOScale(0, .2f).OnComplete(() => {
                buttonDouble.SetActive(false);
            });
            buttonSplit.transform.DOScale(0, .2f).OnComplete(() => {
                buttonSplit.SetActive(false);
            });
            //LeanTween.value(this.gameObject, 1, 0, 0.5f).setOnUpdate((float v) =>
            //{
            //    buttonHit.transform.localScale = Vector3.one * v;
            //    buttonStand.transform.localScale = Vector3.one * v;
            //    buttonDouble.transform.localScale = Vector3.one * v;
            //    buttonSplit.transform.localScale = Vector3.one * v;
            //}).setOnComplete(() =>
            //{
            //    buttonHit.SetActive(false);
            //    buttonStand.SetActive(false);
            //    buttonDouble.SetActive(false);
            //    buttonSplit.SetActive(false);

            //});
        }

        /// <summary>
        /// Shows the buttons.
        /// </summary>
        /// <param name="showDouble">If set to <c>true</c> show double.</param>
        /// <param name="showSplit">If set to <c>true</c> show split.</param>
        public void ShowButtons(bool showDouble = false, bool showSplit = false)
        {
            buttonHit.transform.localScale = Vector3.zero;
            buttonStand.transform.localScale = Vector3.zero;
            buttonDouble.transform.localScale = Vector3.zero;
            buttonSplit.transform.localScale = Vector3.zero;

            buttonHit.SetActive(true);
            buttonStand.SetActive(true);
            if (showDouble) buttonDouble.SetActive(true);
            if (showSplit) buttonSplit.SetActive(true);

            //LeanTween.value(this.gameObject, 0, 1, 0.5f).setOnUpdate((float v) =>
            //{

            //    buttonHit.transform.localScale = Vector3.one * v;
            //    buttonStand.transform.localScale = Vector3.one * v;
            //    if (showDouble) buttonDouble.transform.localScale = Vector3.one * v;
            //    if (showSplit) buttonSplit.transform.localScale = Vector3.one * v;
            //});

            buttonHit.transform.DOScale(1, 0.2f);
            buttonStand.transform.DOScale(1, 0.2f);
            if(showDouble) buttonDouble.transform.DOScale(1, 0.2f);
            if (showSplit) buttonSplit.transform.DOScale(1, 0.2f);
        }
    }
}