using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NTFunctions
{
    public class NTVertical : AlignFit
    {
        public RectTransform rectTransform;
        public List<AlignFit> BaseOn = new List<AlignFit>();

        public bool AutoGetChild = false;

        public float top = 0;
        public float bottom = 0;

        public float space = 0;

        public override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadRectTransform();
            this.LoadBaseOn();
        }

        protected void LoadRectTransform()
        {
            this.rectTransform = transform.GetComponent<RectTransform>();
        }

        protected void LoadBaseOn()
        {
            this.BaseOn.Clear();
            if (this.BaseOn.Count > 0) return;
            foreach (Transform item in transform)
            {
                if (item.TryGetComponent<AlignFit>(out AlignFit alignFit))
                {
                    alignFit.GetComponent<RectTransform>().pivot = new Vector2(alignFit.GetComponent<RectTransform>().pivot.x, 1);
                    alignFit.FitEvent = this.FitSelf;
                    this.BaseOn.Add(alignFit);
                }
                else item.gameObject.SetActive(false);

            }
        }

        protected override void Update()
        {
            base.Update();
            this.FitSelf();
        }

        [Button]
        public override void FitSelf()
        {
            float height = this.top;
            if (AutoGetChild)
            {
                this.LoadBaseOn();
            }

            foreach (AlignFit alignFit in BaseOn)
            {
                if (!alignFit.gameObject.activeSelf) continue;
                alignFit.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -height);
                height += alignFit.GetComponent<RectTransform>().rect.height + this.space;
            }

            height += this.bottom;
            this.rectTransform.sizeDelta = new Vector2(this.rectTransform.rect.width, height);
            base.FitSelf();
        }

        public void AddElement(AlignFit alignFit)
        {
            this.BaseOn.Add(alignFit);
            alignFit.GetComponent<RectTransform>().pivot = new Vector2(alignFit.GetComponent<RectTransform>().pivot.x, 1);
            alignFit.FitEvent = this.FitSelf;
        }
    }
}
