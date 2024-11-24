using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colyseus;
using System.Linq;
using NTPackage;
using Sirenix.OdinInspector;
using NTPackage.EventDispatcher;
using System;
using UnityEngine.Events;
using NTPackage.Functions;
using Rubik.BlackJackMiniGame;
namespace Rubik._21J
{

    public class Room_21J : MonoBehaviour
    {
        public const string Local_Port = "ws://localhost:5007";
        // public const string URL = "ws://15.235.180.137:3007";
        public const string Admy_Port = "ws://103.167.89.114:5007";

        public string URL
        {
            get
            {
                // return Local_Port;
                return Admy_Port;
            }
        }

        public ColyseusClient Client;
        public ColyseusRoom<State_21J> Room;
        public string SessionId;
        public string OpSessionId;

        public NTDictionary<string, PlayerInfo_21J> PlayerInfoDic;
        public NTDictionary<string, PlayerState_21J> PlayerStateDic;

        public PlayerData_21J PlayerData;
        public StateData StateData;

        public static Room_21J instance;
        protected void Awake()
        {
            if (Room_21J.instance != null)
            {
                Debug.LogWarning("Only 1 instance allow");
                return;
            }
            Room_21J.instance = this;
        }

        private void Start()
        {

        }

        private void OnApplicationQuit()
        {
            this.LeaveRoom();
        }

        public void ResetRoom()
        { 
            this.PlayerInfoDic = new NTDictionary<string, PlayerInfo_21J>();
            this.PlayerStateDic = new NTDictionary<string, PlayerState_21J>();
            this.LeaveRoom();
        }

        public void LeaveRoom()
        {
            try
            {
                _ = this.Room.Leave();
                
            }
            catch (System.Exception e)
            {
                NTLog.LogError(e.ToString(), gameObject);
            }
        }

        [Button]
        public async void InitClient()
        {
            this.ResetRoom();
            Debug.Log("InitClient");
            Client = new ColyseusClient(URL);
            Debug.Log("InitRoom");
            this.StateData = new StateData();
            PlayerInfo_21J playerInfo_21J = new PlayerInfo_21J();
            playerInfo_21J.Name = "Player";
            playerInfo_21J.Avata = UnityEngine.Random.Range(1, 75).ToString();
            playerInfo_21J.ID = "123";
            playerInfo_21J.Bet = 3;

            Room = await Client.JoinOrCreate<State_21J>("21J", playerInfo_21J.ToDictionary());

            this.SessionId = Room.SessionId;

            Room.OnMessage<PlayerData_21J>(Message_Key_Config.UpdatePlayerData, UpdatePlayerData);
            Room.OnMessage<PlayerJoinResult_21J>(Message_Key_Config.PlayerJoin, PlayerJoin);
            Room.OnMessage<ResultHitCard_21J>(Message_Key_Config.PlayerHitCard, PlayerHitCardResult);
            Room.OnMessage<ResultHitHoldCard_21J>(Message_Key_Config.PlayerHitHoldCard, PlayerHitHoldCardResult);
            Room.OnMessage<HoldCardResult_21J>(Message_Key_Config.HoldCard, HoldCardResult);
            Room.OnMessage<int[]>(Message_Key_Config.UpdatePlayerCards, UpdatePlayerCards);
            Room.OnMessage<int>(Message_Key_Config.GameStart, GameStart);
            Room.OnMessage<int>(Message_Key_Config.PlayerLose, PlayerLose);
            Room.OnMessage<int[]>(Message_Key_Config.BlackJack, BlackJack);
            Room.OnMessage<int[]>(Message_Key_Config.Burst, Burst);
            Room.OnMessage<int[]>(Message_Key_Config.Clear, Clear);
            Room.OnMessage<int[]>(Message_Key_Config.Perfect, Perfect);
            Room.OnMessage<int[]>(Message_Key_Config.Combo, Combo);

            Room.State.players.OnAdd += ((key, player) =>
            {
                Debug.Log("Join: " + player.ToString());
                player.OnChange += ((data) =>
                {
                    Debug.Log("Update: " + player.ToString());
                    this.UpdatePlayerState(player);
                });
                player.OnRemove += (() =>
                {
                    this.PlayerStateDic.Remove(player.SessionId);
                });
            });
            Room.State.OnChange += ((data) =>
            {
                // Debug.Log("Update Room : " + Room.State.ToString());
                this.StateData.status = (GameState_21J)Room.State.status;
                this.StateData.timeTurn = (int)Room.State.timeTurn;
                if(StateData.status == GameState_21J.Waiting)
                {
                    GameController.instance.SetStartGame(this.StateData.timeTurn.ToString());
                }
                else
                {
                    GameController.instance.timeLeft.SetTime(this.StateData.timeTurn);
                }
                
                
            });
            this.GetPlayerData();
        }
        public void SendMsg<T>(string key, T data)
        {
            try
            {
                _ = this.Room.Send(key, data);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        public void PlayerJoin(PlayerJoinResult_21J playerJoinResult)
        {
            NTLog.LogMessage(JsonUtility.ToJson(("PlayerJoin", JsonUtility.ToJson(playerJoinResult)), gameObject));
            this.PlayerInfoDic.Add(playerJoinResult.playerInfo.SessionId, playerJoinResult.playerInfo);
        }

        public void UpdatePlayerState(Player_21J player)
        {
            PlayerState_21J playerState = this.PlayerStateDic.Get(player.SessionId);
            if (playerState == null)
            {
                playerState = new PlayerState_21J();
                playerState.SessionId = player.SessionId;
                this.PlayerStateDic.Add(player.SessionId, playerState);
            }
            playerState.score = (int)player.score;
            playerState.health = (int)player.health;
          

        }

        public void GetPlayerData()
        {
            this.SendMsg<int>(Message_Key_Config.GetPlayerData, 1);
        }

        public void UpdatePlayerData(PlayerData_21J playerData)
        {
            NTLog.LogMessage(JsonUtility.ToJson(("UpdatePlayerData", JsonUtility.ToJson(playerData)), gameObject));
            this.PlayerData = playerData;
           
        }

        [Button]
        public void PlayerHitCard(int slot)
        {
            HitCard_21J hitCard = new HitCard_21J();
            hitCard.slot = slot;
            this.SendMsg<HitCard_21J>(Message_Key_Config.PlayerHitCard, hitCard);
        }

        [Button]
        public void HoldCard()
        {
            this.SendMsg<int>(Message_Key_Config.HoldCard, 1);
        }

        public void HoldCardResult(HoldCardResult_21J holdCardResult)
        {
            this.PlayerData.Cards = holdCardResult.Cards;
            this.PlayerData.HoldCard = holdCardResult.HoldCard;
            GameController.instance.StartSpawnCard();
        }

        [Button]
        public void HitHoldCard(int slot)
        {
            HitCard_21J hitCard = new HitCard_21J();
            hitCard.slot = slot;
            this.SendMsg<HitCard_21J>(Message_Key_Config.HitHoldCard, hitCard);
           // GameController.instance.StartSpawnCard();
        }

        public void GameStart(int result)
        {
            Debug.LogWarning("GameStart");
            GameController.instance.StartSpawnCard();
        }
        public void PlayerLose(int result)
        {
            Debug.LogWarning("PlayerLose");
            //MessageController.Instance.ShowNotiText("You don't have turn !!!");
            GameController.instance.SetResult();
        }
        public void BlackJack(int[] result)
        {
            Debug.LogWarning("BlackJack:" + result);
            MessageController.Instance.ShowNotiText("BlackJack + " + result[0]);
            GameController.instance.SpawnText();
            GameController.instance.ClearSlot(result);
        }
        public void Burst(int[] result)
        {
            Debug.LogWarning("Burst");
            MessageController.Instance.ShowNotiText("Burst");
            GameController.instance.SpawnText();
            GameController.instance.ClearSlot(result);
        }
        public void Clear(int[] result)
        {
            GameController.instance.SpawnText();
            Debug.LogWarning("Clear");
            MessageController.Instance.ShowNotiText("5 Card + "+ result[0]);
            GameController.instance.ClearSlot(result);
        }
        public void Perfect(int[] result)
        {
            Debug.LogWarning("Perfect:"+ result);
            MessageController.Instance.ShowNotiText("Perfect + " + result[0]);
            GameController.instance.ClearSlot(result);
        }
        public void Combo(int[] result)
        {
            Debug.LogWarning("Combo:"+ result);

        }

        public void PlayerHitCardResult(ResultHitCard_21J result)
        {
            NTLog.LogMessage(JsonUtility.ToJson(("PlayerHitCardResult", JsonUtility.ToJson(result)), gameObject));

            //this.PlayerData.Slot[result.slot].Cards = result.Cards;
          

            this.PlayerData.Slot[result.slot] = result.CardSlot;
            GameController.instance.StartSpawnCard();

        }
        public void PlayerHitHoldCardResult(ResultHitHoldCard_21J result)
        {
            NTLog.LogMessage(JsonUtility.ToJson(("PlayerHitHoldCardResult", JsonUtility.ToJson(result)), gameObject));
            this.PlayerData.Slot[result.slot] = result.CardSlot;
            this.PlayerData.HoldCard = -1;
            GameController.instance.StartSpawnCard();
        }

        public void UpdatePlayerCards(int[] cards)
        {
            NTLog.LogMessage(JsonUtility.ToJson(("UpdatePlayerCards", JsonUtility.ToJson(cards)), gameObject));
            this.PlayerData.Cards = cards;
        }

    }
}