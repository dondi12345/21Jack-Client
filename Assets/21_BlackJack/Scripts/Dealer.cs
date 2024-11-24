using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Rubik.BlackJackMiniGame
{
    public class Dealer : MonoBehaviour
    {

        public GameObject cardPrefab;

        int dealer = 52;
        public Dictionary<int, Card> cardsMap = new Dictionary<int, Card>();
        public List<Card> lsCards;

        System.Random random = new System.Random();
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Deal card.
        /// </summary>
        /// <returns>The deal.</returns>
        public Card Deal()
        {
            //SoundManager.Instance.PlayFlip();
            Card card = this.GetRandomCard();
            //Debug.LogFormat("{0}:{1}, {2} :{3}", id, card.color, card.number,card.point);
            return card;
        }
       public Card DealCard()
        {
            return GetCurrentCard();
        }

        public void Recycle(Card card)
        {
            card.gameObject.SetActive(false);
            card.transform.SetParent(this.transform, false);
            card.transform.localPosition = Vector3.zero;
        }
        /// <summary>
        /// Get the random card index.
        /// </summary>
        /// <returns>The random identifier.</returns>
        Card GetRandomCard()
        {
            Card card = null;
            int id = random.Next(1, 53);
            if (cardsMap.ContainsKey(id))
            {
                if (cardsMap[id].isActiveAndEnabled)
                    card = this.GetRandomCard();
                else
                {
                    card = cardsMap[id];
                    card.gameObject.SetActive(true);
                    return card;
                }
            }
            else
            {
                GameObject go = Instantiate(cardPrefab, this.transform);
                card = go.GetComponent<Card>();
                card.ID = id;
                this.cardsMap[id] = card;
            }
            return card;
        }

        Card GetCurrentCard()
        {
            Card card = null;
            
            int id = GameController.instance.GetCurrentCardID() ;
            if (id == -1)
            {
                return null;
            }
                card = lsCards[lsCards.Count-1];
            card.ID = id;
            card.face = AssetLoader.instance.GetCardSprite(id);
            dealer--;
            lsCards.Remove(card);
            if (lsCards.Count == 0)
            {
                SpawnCard();
            }

            return card;
        }
        void SpawnCard()
        {
            Card card = null;
            int temp = 11;
            if (dealer > 11)
            {
                temp = 11;
            }
            else
            {
                temp = dealer;
            }
            for(int i = 0; i < temp; i++)
            {
                GameObject go = Instantiate(cardPrefab, this.transform);
                card = go.GetComponent<Card>();
                lsCards.Add(card);
            }
         
        }
    }
}