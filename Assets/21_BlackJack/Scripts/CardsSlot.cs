using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Rubik.BlackJackMiniGame
{
    /// <summary>
    /// Card slot is a management class used to manage the cards.
    /// </summary>
    public class CardsSlot : MonoBehaviour
    {


        public List<Card> cards = new List<Card>();
        private List<Card> aList = new List<Card>();
        public Transform[] posTrans;
        int count = 0;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Adds the card.
        /// </summary>
        /// <param name="card">Card.</param>
        /// <param name="flip">If set to <c>true</c> flip.</param>
        public void AddCardToXPos(Card card, bool flip = true)
        {
            this.cards.Add(card);
            if (card.number == 1)
                this.aList.Add(card);
            card.transform.SetParent(this.transform, true);

            int i = cards.Count - 1;
            float angel = i * -3f;
            float x = i * 20f;
            float y = 30f * Mathf.Cos(Mathf.Deg2Rad * angel * 5);

            //cards[i].transform.localPosition = new Vector3(x, y, 0f);
            //cards[i].transform.localRotation = Quaternion.Euler(0, 0, angel);

            //LeanTween.moveLocal(card.gameObject, new Vector3(x, y, 0f), 0.5f);
            //LeanTween.rotateLocal(card.gameObject, new Vector3(0, 0, angel), 0.5f).setOnComplete(Layout);

            if (flip)
            {
                //LeanTween.delayedCall(card.gameObject, 0.25f, () => { card.Flip(); });
                card.Invoke("Flip", 0.25f);
            }
            card.transform.DOLocalMove(new Vector3(0, 0, 0f), 0.3f);
            //card.transform.DOLocalRotate(new Vector3(0, 0, angel), 0.5f).OnComplete(() =>
            //{
            //    Layout();
            //});
            if (flip)
            {

            }
        }
        public Ease easetween = Ease.InOutSine;
        public void AddCardToYPos(Card card, bool flip = true,UnityEngine.Events.UnityAction callback = null)
        {
            this.cards.Add(card);
            if (card.number == 1)
                this.aList.Add(card);

            int i = cards.Count - 1;
            float y = posTrans[i].position.y;
            float x = posTrans[i].position.x;
            
            //cards[i].transform.localPosition = new Vector3(x, y, 0f);
            //cards[i].transform.localRotation = Quaternion.Euler(0, 0, angel);

            //LeanTween.moveLocal(card.gameObject, new Vector3(x, y, 0f), 0.5f);
            //LeanTween.rotateLocal(card.gameObject, new Vector3(0, 0, angel), 0.5f).setOnComplete(Layout);

            if (flip)
            {
                //LeanTween.delayedCall(card.gameObject, 0.25f, () => { card.Flip(); });
                card.Invoke("Flip", 0.25f);
            }
            card.transform.DOMove(new Vector3(x, y, 0f), 0.3f).SetEase(easetween).OnComplete(()=> {
                if (callback != null)
                {
                    callback();
                }
                card.transform.SetParent(this.transform, true);
                //this.cards.Clear();
            });
            //card.transform.DOLocalRotate(new Vector3(0, 0, angel), 0.5f).OnComplete(() =>
            //{
            //    Layout();
            //});
            if (flip)
            {

            }
        }
        void FlipCard()
        {

        }
        /// <summary>
        /// Layout card slot.
        /// </summary>
        void Layout()
        {
            int num = cards.Count;
            //LeanTween.rotateLocal(this.gameObject, new Vector3(0, 0, num / 2 * 3f), 0.1f);
            gameObject.transform.DOLocalRotate(new Vector3(0, 0, num / 2 * 3f), 0.1f);
        }

        /// <summary>
        /// Get current point.
        /// </summary>
        /// <value>The point.</value>
        public int Point
        {
            get
            {
                return this.GetPoint();
            }
        }

        /// <summary>
        /// Get current point.
        /// </summary>
        /// <returns>The point.</returns>
        int GetPoint()
        {
            int point = this.CalcPoint();
            if (point > 21 && aList.Count > 0)
            {
                for (int i = 0; i < aList.Count; i++)
                {
                    Card card = aList[i];

                    card.point = 1;

                    point = this.CalcPoint();
                    if (point <= 21)
                        break;
                }
            }
            return point;
        }


        int CalcPoint()
        {
            int point = 0;
            for (int i = 0; i < cards.Count; i++)
            {
                Card card = cards[i];
                point += card.point;
            }
            return point;
        }

        /// <summary>
        /// Flip all cards.
        /// </summary>
        public void FlipAll()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Card card = cards[i];
                card.Flip();
            }
        }

        /// <summary>
        /// Remove all cards.
        /// </summary>
        public void RemoveAll()
        {
            float temp = 1;
            for (int i = cards.Count-1; i >=0; i--)
            {
                Card card = cards[i];

                //LeanTween.move(card.gameObject, new Vector3(800, 1000, 0), 0.5f).setOnComplete(() =>
                //{
                //    Game.Instance.RecycleCard(card);
                //});
                int tempCard = i;
                card.transform.DOMove(GameController.instance.scoreItem[transform.GetSiblingIndex()].transform.position, 0.5f)
                    .SetEase(Ease.InOutSine)
                    .SetDelay(i*0.1f)
                    .OnComplete(() =>
                    {
                        //Game.Instance.RecycleCard(card);
                        cards[tempCard].gameObject.SetActive(false);
                        if (tempCard == cards.Count - 1)
                        {
                            this.cards.Clear();
                        }
                    });
                card.transform.DOScale(new Vector2(.5f, .5f), 0.5f);
                card.GetComponent<CanvasGroup>().DOFade(.5f, 0.5f).SetUpdate(true);
                //card.image.DOFade(Vector2.zero, .5f);
                //Vector2 rd = new Vector2(temp * Random.Range(0, 5) * 0.1f, Random.Range(0, 5) * 0.1f);
                //card.transform.DOMove(rd, 0.1f).OnComplete(() =>
                //{

                //});

                temp = (-1)*temp;
            }
            //this.cards.Clear();
        }
        public Card GetCard(int index)
        {
            if (cards.Count > 0)
            {
                Card card = cards[index];
                cards.Remove(card);
                return card;
            }
            return null;
            
        }
        public bool CheckSlotEmpty()
        {
            if (cards == null || cards.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}