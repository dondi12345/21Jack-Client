using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NTPackage.Functions;
using Sirenix.OdinInspector;
using NTPackage.EventDispatcher;

namespace NTPackage.UI
{
    public enum KindPopup
    {
        none = 0,
        oneStep = 1,
        twoStep = 2,
        threeStep = 3,
        moveTo = 4,
    }

    public class PopupUI : NTBehaviour
    {
        public PopupCode popupCode = PopupCode.Unknown;
        public Transform transPanel;

        public KindPopup show = KindPopup.none;
        public KindPopup hide = KindPopup.none;

        public List<Vector2> ShowPos;
        public float DurationShow = 0.2f;
        public List<Vector2> HidePos;
        public float DurationHide = 0.2f;
        public Coroutine Coroutine;

        public override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadTransPanel();
            this.popupCode = PopupCodeParser.FromString(transform.name);
        }

        protected void LoadTransPanel()
        {
            if (this.transPanel != null) return;
            try
            {
                this.transPanel = transform.Find("Panel");
            }
            catch (System.Exception)
            {
                Debug.LogWarning("Can't LoadImageItem");
            }

        }
        //Function
        protected override void Start()
        {
            base.Start();
            PopupManager.instance.PopupDic.Add(this.popupCode, this);
        }

        public Action ActionOnUI;
        public virtual void OnUI(object data = null)
        {
            this.Show();
            this.UpdateData();
        }

        public Action ActionOffUI;
        public virtual void OffUI()
        {
            try
            {
                this.ActionOffUI.Invoke();
                this.ActionOffUI = null;
            }
            catch (System.Exception) { }
            this.Hide();
        }

        public virtual void UpdateData(object data = null) { }

        public virtual void Show()
        {
            switch (show)
            {
                case KindPopup.oneStep:
                    this.ShowOneDG();
                    break;
                case KindPopup.twoStep:
                    this.ShowDoubleDG();
                    break;
                case KindPopup.threeStep:
                    this.ShowTrippleDG();
                    break;
                case KindPopup.moveTo:
                    this.ShowMoveTo();
                    break;
                default:
                    this.ShowNone();
                    break;
            }
            try
            {
                // UIManager.instance.UpdateData();
            }
            catch (System.Exception) { }
        }

        [Button]
        public virtual void ShowNone(){
            transform.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            this.transPanel.localScale = new Vector3(1,1,1);
            this.transPanel.gameObject.SetActive(true);
        }
        public virtual void ShowOneDG(){
            transform.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            this.transPanel.localScale = new Vector3(0,0,0);
            this.transPanel.gameObject.SetActive(true);
            this.transPanel.DOScale(new Vector3(1f,1f,1f), 0.3f);
        }

        public virtual void ShowDoubleDG(){
            transform.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            this.transPanel.localScale = new Vector3(0,0,0);
            this.transPanel.gameObject.SetActive(true);
            this.transPanel.DOScale(new Vector3(1.1f,1.1f,1.1f), 0.2f).OnComplete(()=>{
                this.transPanel.DOScale(new Vector3(1f,1f,1f), 0.1f);
            });
        }

        public virtual void ShowTrippleDG(){
            transform.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            this.transPanel.localScale = new Vector3(0,0,0);
            this.transPanel.gameObject.SetActive(true);
            this.transPanel.DOScale(new Vector3(1.1f,1.1f,1.1f), 0.2f).OnComplete(()=>{
                this.transPanel.DOScale(new Vector3(0.9f,0.9f,0.9f), 0.06f).OnComplete(()=>{
                    this.transPanel.DOScale(new Vector3(1.1f,1.1f,1.1f), 0.04f);
                });
            });
        }

        public virtual void ShowMoveTo(){
            transform.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            this.transPanel.localScale = new Vector3(1,1,1);
            this.transPanel.gameObject.SetActive(true);
            RectTransform rect = this.transPanel.GetComponent<RectTransform>();
            rect.anchoredPosition = this.HidePos[this.HidePos.Count -1];
            rect.DOComplete();
            if(this.Coroutine != null) StopCoroutine(this.Coroutine);
            this.Coroutine = StartCoroutine(this.MoveTo(this.DurationShow, ShowPos, rect));
        }

        public virtual void  Hide(){
            try
            {
                this.HideSound();   
            }
            catch (System.Exception){}
            switch (hide)
            {
                case KindPopup.oneStep:
                    this.HideOneDG();
                    break;
                case KindPopup.twoStep:
                    this.HideDoubleDG();
                    break;
                case KindPopup.moveTo:
                    this.HideMoveTo();
                    break;
                default:
                    this.HideNone();
                    break;
            }
        }

        protected virtual void HideSound(){
            // Rubik.Common.AudioHelper.AudioCtrl.instance.Play(Rubik.Common.AudioHelper.AudioName.UI_Button_Back_Exit);
        }

        [Button]
        public virtual void HideNone(){
            this.transPanel.gameObject.SetActive(false);
            transform.GetComponent<RectTransform>().localScale = new Vector3(0,0,0);
            this.transPanel.localScale = new Vector3(0,0,0);
        }

        public virtual void HideOneDG(){
            this.transPanel.DOScale(new Vector3(0f,0f,0f), 0.25f).OnComplete(()=>{
                this.transPanel.gameObject.SetActive(false);
                transform.GetComponent<RectTransform>().localScale = new Vector3(0,0,0);
            });
        }

        public virtual void HideDoubleDG(){
            this.transPanel.DOScale(new Vector3(1.1f,1.1f,1.1f), 0.075f).OnComplete(()=>{
                this.transPanel.DOScale(new Vector3(0f,0f,0f), 0.15f).OnComplete(()=>{
                    this.transPanel.gameObject.SetActive(false);
                    transform.GetComponent<RectTransform>().localScale = new Vector3(0,0,0);
                });
            });
        }
        public void HideMoveTo(){
            RectTransform rect = this.transPanel.GetComponent<RectTransform>();
            rect.DOComplete();
            
            if(this.Coroutine != null) StopCoroutine(this.Coroutine);
            this.Coroutine = StartCoroutine(this.MoveTo(this.DurationHide, HidePos, rect, ()=>{
                this.transPanel.gameObject.SetActive(false);
                transform.GetComponent<RectTransform>().localScale = new Vector3(0,0,0);
            }));
        }

        public IEnumerator MoveTo(float duration, List<Vector2> listPos, RectTransform rect, Action done = null){
            int index = 0;
            while (true)
            {
                if(index > listPos.Count -1) break;
                rect.DOAnchorPos(listPos[index], duration);
                yield return new WaitForSeconds(duration);
                index++;
                duration /= 2;
            }
            done?.Invoke();
        }

        public bool IsShow()
        {
            return this.transPanel.gameObject.activeSelf;
        }

        public void Home()
        {
            this.ActionOffUI = null;
            this.ActionOnUI = null;
            PopupManager.instance.OffAllPopupUI();
        }
    }
}
