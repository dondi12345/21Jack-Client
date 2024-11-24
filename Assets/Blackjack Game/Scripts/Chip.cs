using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace BlackJackGame
{
    /// <summary>
    /// Chip class manage the chips player bets.
    /// </summary>
    public class Chip : RefSingleton<Chip>
    {

        public Image image;
        public Text text;
        public Image flyChip,doubleChip;


        Vector3 initPos;
        // Use this for initialization
        void Start()
        {
            this.Clear();
            initPos = this.transform.localPosition;
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Show the chips.
        /// </summary>
        /// <param name="image">Image.</param>
        /// <param name="from">From.</param>
        public void ShowChips(Image image, Vector3 from)
        {
            this.transform.localPosition = Vector3.zero;
            this.gameObject.SetActive(true);
            this.flyChip.transform.position = from;
            if (image != null)
                this.flyChip.sprite = image.sprite;
            this.flyChip.enabled = true;
            this.flyChip.gameObject.transform.DOMove(this.image.transform.position, 0.5f).OnComplete(()=> {
                this.image.enabled = true;
                this.image.sprite = this.flyChip.sprite;
                this.flyChip.enabled = false;
                this.text.enabled = true;
                this.text.text = Game.Instance.chips.ToString();
            });
            //LeanTween.move(this.flyChip.gameObject, this.image.transform, 0.3f).setOnComplete(() =>
            //{
            //    this.image.enabled = true;
            //    this.image.sprite = this.flyChip.sprite;
            //    this.flyChip.enabled = false;
            //    this.text.enabled = true;
            //    this.text.text = Game.Instance.chips.ToString();

            //});
        }

        /// <summary>
        /// Clear chips display.
        /// </summary>
        public void Clear()
        {
            this.image.enabled = false;
            this.flyChip.enabled = false;
            this.text.enabled = false;
            doubleChip.gameObject.SetActive(false);
        }


        /// <summary>
        /// Fly to side.
        /// </summary>
        public void FlyToSide()
        {
            //LeanTween.moveLocal(this.gameObject, new Vector3(300, 0, 0), 0.3f).setEase(LeanTweenType.easeInSine);
            this.transform.DOLocalMove(new Vector3(0, -780, 0), 0.3f).SetEase(Ease.InSine);
        }
        public void SplitChipToDoubleSide()
        {
            //LeanTween.moveLocal(this.gameObject, new Vector3(300, 0, 0), 0.3f).setEase(LeanTweenType.easeInSine);
            doubleChip.transform.localPosition = transform.localPosition;
            doubleChip.sprite = flyChip.sprite;
            doubleChip.GetComponentInChildren<Text>().text = text.text;
            Game.Instance.AddChipsDouble(Game.Instance.chips);
            doubleChip.gameObject.SetActive(true);
            doubleChip.transform.DOLocalMove(new Vector3(200, -780, 0), 0.3f).SetEase(Ease.InSine);
            this.transform.DOLocalMove(new Vector3(-200, -780, 0), 0.3f).SetEase(Ease.InSine);
        }

        /// <summary>
        /// Fly to banker.
        /// </summary>
        public void FlyToBanker()
        {
            //LeanTween.moveLocal(this.gameObject, new Vector3(0, 500, 0), 0.3f).setEase(LeanTweenType.easeInSine);
            this.transform.DOLocalMove(new Vector3(0, 500, 0), 0.3f).SetEase(Ease.InSine);
            if (doubleChip.gameObject.activeSelf)
            {
               
                doubleChip.transform.DOLocalMove(new Vector3(0, 500, 0), 0.3f).SetEase(Ease.InSine).OnComplete(() => {
                    doubleChip.gameObject.SetActive(false);
                    text.text = (int.Parse(text.text) * 2).ToString();
                    
                });
            }
        }

        /// <summary>
        /// Fly to player.
        /// </summary>
        public void FlyToPlayer()
        {
            //LeanTween.moveLocal(this.gameObject, new Vector3(0, -500, 0), 0.3f).setEase(LeanTweenType.easeInSine);
            this.transform.DOLocalMove(new Vector3(0, -500, 0), 0.3f).SetEase(Ease.InSine);
            if (doubleChip.gameObject.activeSelf)
            {
                doubleChip.transform.DOLocalMove(new Vector3(0, -500, 0), 0.3f).SetEase(Ease.InSine).OnComplete(()=> {
                    doubleChip.gameObject.SetActive(false);
                    text.text = (int.Parse(text.text)*2).ToString();
                    Game.Instance.chips = Game.Instance.chips / 2;
                });
            }
        }

        /// <summary>
        /// Reset the position.
        /// </summary>
        public void ResetPosition()
        {
            this.transform.localPosition = initPos;
            doubleChip.transform.localPosition = transform.localPosition;
            doubleChip.gameObject.SetActive(false);
        }
    }
}