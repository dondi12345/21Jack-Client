using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityEngine.UI
{
    public class UIToggleSpriteSwap : InteractiveUI, IPointerClickHandler
    {
        [SerializeField]
        private Image avatar;
        [SerializeField]
        private Sprite spriteOn;
        [SerializeField]
        private Sprite spriteOff;
        [SerializeField]
        private bool isOn = true;
        private float pressedY { get; set; }
        public Toggle.ToggleEvent OnValueChange;

        private void Start()
        {
            if (avatar == null)
            {
                avatar = GetComponent<Image>();
            }
            avatar.sprite = isOn ? spriteOn : spriteOff;
            onFingerDown.AddListener(OnFingerDown);
            onFingerUp.AddListener(OnFingerUp);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            IsOn = !IsOn;
            OnValueChange.Invoke(IsOn);
        }

        public bool IsOn
        {
            get { return isOn; }
            set
            {
                isOn = value;
                if (avatar != null)
                {
                    avatar.sprite = value ? spriteOn : spriteOff;
                }
            }
        }

        private void OnFingerDown()
        {
            avatar.transform.DOKill(true);
            avatar.DOKill(true);
            avatar.DOColor(new Color(0.8f, 0.8f, 0.8f), 0.1f);
            pressedY = avatar.transform.localPosition.y;
            avatar.transform.DOLocalMoveY(-5, .1f).SetRelative().SetEase(Ease.Linear);
            avatar.transform.DOScale(.98f, .1f).SetEase(Ease.Linear);
        }

        private void OnFingerUp()
        {
            avatar.transform.DOKill(true);
            avatar.DOKill(true);
            avatar.DOColor(Color.white, 0.1f);
            avatar.transform.DOLocalMoveY(pressedY, .1f).SetEase(Ease.Linear);
            avatar.transform.DOScale(1, .1f).SetEase(Ease.Linear);
        }
    }
}
