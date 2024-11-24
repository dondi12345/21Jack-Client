using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  Rubik._21J;
namespace Rubik.BlackJackMiniGame
{
    public class GameController : MonoBehaviour
    {
        public int[] Cards;

        public CardsSlot currentCard,holdCard;
        public CardsSlot[] ListCards;


        [SerializeField] Sprite buttonOn, buttonOff;
        public Dealer dealer;

        [SerializeField] PlayerInfo player;

        [SerializeField]public  ScoreItem[] scoreItem;
        public static GameController instance;
        [SerializeField]bool isHoldCard = false;
        public Timer timeLeft;
        [SerializeField] GameObject playerResult;
        [SerializeField] GameObject textEffect,startGame;
        [SerializeField] UnityEngine.UI.Text startTxt, scoreTxt;
        private void Awake()
        {
            if (GameController.instance != null)
            {
                Debug.LogWarning("Only 1 instance allow");
                return;
            }
            GameController.instance = this;
        }
        void Start()
        {
            Room_21J.instance.InitClient();
            //StartSpawnCard();
        }
        
        public  void StartSpawnCard()
        {
            StartCoroutine(CoStarGame());
        }
        public void SetStartGame(string txt)
        {
            startTxt.text = txt;
            startGame.SetActive(true);
        }
        IEnumerator CoStarGame()
        {
            startGame.SetActive(false);
            yield return new WaitForSeconds(.1f);
            SetScore();
            Debug.Log(isHoldCard);
            if (!isHoldCard)
            {
                currentCard.AddCardToXPos(dealer.DealCard());
               
                Debug.Log("Spwan new card");
            }
            currentCard.GetComponent<UnityEngine.UI.Image>().sprite = buttonOn;
            holdCard.GetComponent<UnityEngine.UI.Image>().sprite = buttonOff;
            isHoldCard = false;
            
            var temp = Room_21J.instance.PlayerInfoDic.Get(Room_21J.instance.SessionId); 
            player.SetupInfo(temp.Name,temp.Avata, Room_21J.instance.PlayerStateDic.Get(Room_21J.instance.SessionId));
            //Debug.Log("Spawn new card");
        }
        public int GetCurrentCardID()
        {
            if (Room_21J.instance.PlayerData.Cards.Length > 0)
            {
                return Room_21J.instance.PlayerData.Cards[0];
            }
            return -1;
        }
        public void SpawnText()
        {
            GameObject go = Instantiate(textEffect);
            go.transform.position = Vector3.zero;
            go.GetComponent<TextEffect>().SetUpText(Color.cyan,1000);
        }
        public void OnCardSlot_Onclick(int index)
        {
            Debug.Log(isHoldCard);
            if (isHoldCard)
            {
                Debug.Log(!holdCard.CheckSlotEmpty());
                if (!holdCard.CheckSlotEmpty())
                {
                    ListCards[index].AddCardToYPos(holdCard.GetCard(0), true, () => {
                        Room_21J.instance.HitHoldCard(index);
                    });
                }
               
               
            }
            else
            {
                if (!currentCard.CheckSlotEmpty())
                {
                    ListCards[index].AddCardToYPos(currentCard.GetCard(0), true, () => {
                        Room_21J.instance.PlayerHitCard(index);
                    });
                }
                
               
            }
            
          
        }
        public void OnCurrentCard_Onclick()
        {
            isHoldCard = false;
            currentCard.GetComponent<UnityEngine.UI.Image>().sprite = buttonOn;
            holdCard.GetComponent<UnityEngine.UI.Image>().sprite = buttonOff;
        }
        public void OnHoldCard_Onclick()
        {
            if (holdCard.CheckSlotEmpty())
            {
                holdCard.AddCardToXPos(currentCard.GetCard(0));
                Room_21J.instance.HoldCard();
               
            }
            else
            {
                isHoldCard = true;
                holdCard.GetComponent<UnityEngine.UI.Image>().sprite = buttonOn;
                currentCard.GetComponent<UnityEngine.UI.Image>().sprite = buttonOff;
            }
           
        }
        public void SetScore()
        {
            for (int i = 0; i < scoreItem.Length; i++)
            {
                scoreItem[i].SetScore(Room_21J.instance.PlayerData.Slot[i].Point);
            }
        }
        public void ClearSlot(int[] slot)
        {
            ListCards[slot[1]].RemoveAll();
        }
        public void SetResult()
        {
            StartCoroutine(StartEnd());
        }
        IEnumerator StartEnd()
        {
            yield return new WaitForSeconds(.3f);
            playerResult.gameObject.SetActive(true);
            scoreTxt.text = player.scoreTxt.text;
        }

        public void LoadScene()
        {
            Room_21J.instance.LeaveRoom();
            UnityEngine.SceneManagement.SceneManager.LoadScene("21_Blackjack");
        }
    }
}

