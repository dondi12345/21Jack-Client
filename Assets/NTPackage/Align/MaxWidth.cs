using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace NTFunctions
{
    public class MaxWidth : NTBehaviour {
        
        public float _MinWidth;
        public float _MinHeight;
        public float _MaxWidth;
        public float _MaxHeight;

        public float _OldWidth = 0;
        public float _OldHeight = 0;

        public RectTransform textTransform;
        public LayoutElement layoutElement;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void Update(){
            base.Update();
            checkWidth();
        }

        [Button]
        public void checkWidth(){
            if(this._OldWidth == textTransform.rect.width && this._OldHeight == textTransform.rect.height) return;
            this.layoutElement.minWidth = this._MinWidth;
            this.layoutElement.minHeight = this._MinHeight;
            if(textTransform.rect.width < this._MaxWidth){
                layoutElement.preferredWidth = -1f;
            }
            else{
                layoutElement.preferredWidth = this._MaxWidth;
            }
            if(textTransform.rect.height < this._MaxHeight){
                layoutElement.preferredHeight = -1f;
            }
            else{
                layoutElement.preferredHeight = this._MaxHeight;
            }
            this._OldWidth = this.textTransform.rect.width;
            this._OldHeight = this.textTransform.rect.height;
        }
    
    }
}
