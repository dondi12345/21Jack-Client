using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NTPackage.Functions;
using System;

namespace NTPackage.UI
{
    public class PopupManager : NTBehaviour
    {
        public float currentLvUI = 0;
        public NTDictionary<PopupCode, PopupUI> PopupDic = new NTDictionary<PopupCode, PopupUI>();

        public static PopupManager instance;
        protected override void Awake()
        {
            base.Awake();
            if (PopupManager.instance != null) Debug.LogWarning("Only 1 UIManager allow");
            PopupManager.instance = this;
            this.PopupDic = new NTDictionary<PopupCode, PopupUI>();
        }

        public override void LoadComponents()
        {
            base.LoadComponents();
        }
        //Function

        protected override void Start()
        {
            base.Start();
            this.OffAllPopupUI();
        }

        public virtual void OffAllPopupUI()
        {
            foreach (PopupUI popupUI in this.PopupDic.ToList())
            {
                popupUI.ActionOffUI = null;
                popupUI.OffUI();
            }
        }

        public PopupUI GetPopupUIByCode(PopupCode popupCode)
        {
            return this.PopupDic.Get(popupCode);
        }

        public void OnUI(PopupCode popupCode, object data = null)
        {
            try
            {
                NTPackage.Functions.NTLog.LogMessage("OnUI:" + popupCode.ToString(), gameObject);
                this.GetPopupUIByCode(popupCode).OnUI(data);
            }
            catch (System.Exception e)
            {
                NTPackage.Functions.NTLog.LogError(e.ToString(), gameObject);
            }
        }
        public void OffUI(PopupCode popupCode)
        {
            try
            {
                NTPackage.Functions.NTLog.LogMessage("OffUI:" + popupCode.ToString(), gameObject);
                this.GetPopupUIByCode(popupCode).OffUI();
            }
            catch (System.Exception e)
            {
                NTPackage.Functions.NTLog.LogError(e.ToString(), gameObject);
            }
        }
        public void UpdateDataUI(PopupCode popupCode, object data = null)
        {
            try
            {
                NTPackage.Functions.NTLog.LogMessage("UpdateData:" + popupCode.ToString(), gameObject);
                this.GetPopupUIByCode(popupCode).UpdateData(data);
            }
            catch (System.Exception e)
            {
                NTPackage.Functions.NTLog.LogError(e.ToString(), gameObject);
            }
        }
    }
}
