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

namespace Rubik._BJ
{
    public class Room_BJ : MonoBehaviour
    {
        public const string Local_Port = "ws://localhost:5007";
        // public const string URL = "ws://15.235.180.137:3007";
        public const string Admy_Port = "ws://103.116.9.104:5007";

        public string URL
        {
            get
            {
                return Local_Port;
                // return Admy_Port;
            }
        }

        public ColyseusClient Client;
        public ColyseusRoom<State_BJ> Room;
        public string SessionId;
        public string OpSessionId;

        public NTDictionary<string, PlayerInfo_BJ> PlayerInfoDic;
        public NTDictionary<string, PlayerState_BJ> PlayerStateDic;
        public NTDictionary<string, PlayerData_BJ> PlayerDataDic;

        public StateData StateData;

        public static Room_BJ instance;
        protected void Awake()
        {
            if (Room_BJ.instance != null)
            {
                Debug.LogWarning("Only 1 instance allow");
                return;
            }
            Room_BJ.instance = this;
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
            this.PlayerInfoDic = new NTDictionary<string, PlayerInfo_BJ>();
            this.PlayerStateDic = new NTDictionary<string, PlayerState_BJ>();
            this.PlayerDataDic = new NTDictionary<string, PlayerData_BJ>();
        }

        public void LeaveRoom()
        {
            _ = this.Room.Leave();
        }

        [Button]
        public async void InitClient()
        {
            this.ResetRoom();
            Debug.Log("InitClient");
            Client = new ColyseusClient(URL);
            Debug.Log("InitRoom");
            this.StateData = new StateData();
            PlayerInfo_BJ playerInfo_BJ = new PlayerInfo_BJ();
            playerInfo_BJ.Name = "Player";
            playerInfo_BJ.Avata = UnityEngine.Random.Range(1, 75).ToString();
            playerInfo_BJ.ID = "123";

            Room = await Client.JoinOrCreate<State_BJ>("BJ", playerInfo_BJ.ToDictionary());
            this.SessionId = Room.SessionId;

            Room.OnMessage<PlayerData_BJ>(Message_Key_Config.UpdatePlayerData, UpdatePlayerData);
            Room.OnMessage<PlayerJoinResult_BJ>(Message_Key_Config.PlayerJoin, PlayerJoin);
            Room.OnMessage<int>(Message_Key_Config.GameStart, GameStart);
            Room.OnMessage<int>(Message_Key_Config.PlayerLose, PlayerLose);
            Room.OnMessage<DealCard_BJ>(Message_Key_Config.DealCard, DealCard);

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
                    this.PlayerDataDic.Remove(player.SessionId);
                });
            });
            Room.State.OnChange += ((data) =>
            {
                // Debug.Log("Update Room : " + Room.State.ToString());
                this.StateData.status = (StatusStatus_BJ)Room.State.status;
                this.StateData.timeTurn = (int)Room.State.timeTurn;
            });
            // this.GetPlayerData(this.SessionId);
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

        public void PlayerJoin(PlayerJoinResult_BJ playerJoinResult)
        {
            NTLog.LogMessage(JsonUtility.ToJson(("PlayerJoin", JsonUtility.ToJson(playerJoinResult)), gameObject));
            this.PlayerInfoDic.Add(playerJoinResult.playerInfo.SessionId, playerJoinResult.playerInfo);
        }

        public void UpdatePlayerState(Player_BJ player)
        {
            PlayerState_BJ playerState = this.PlayerStateDic.Get(player.SessionId);
            if (playerState == null)
            {
                playerState = new PlayerState_BJ();
                playerState.SessionId = player.SessionId;
                this.PlayerStateDic.Add(player.SessionId, playerState);
            }
            playerState.status = (PlayerStatus_BJ)player.status;
            playerState.gold = (int)player.gold;
        }

        public void GetPlayerData(string sessionId)
        {
            this.SendMsg<string>(Message_Key_Config.GetPlayerData, sessionId);
        }

        public void UpdatePlayerData(PlayerData_BJ playerData)
        {
            NTLog.LogMessage(JsonUtility.ToJson(("UpdatePlayerData", JsonUtility.ToJson(playerData)), gameObject));
            PlayerData_BJ player = this.PlayerDataDic.Get(playerData.SessionId);
            if (player == null)
            {
                player = new PlayerData_BJ();
                player.SessionId = playerData.SessionId;
                this.PlayerDataDic.Add(playerData.SessionId, player);
            }
            player.CardDatas = playerData.CardDatas;
        }

        public void GameStart(int result)
        {
            Debug.LogWarning("GameStart");
        }
        public void PlayerLose(int result)
        {
            Debug.LogWarning("PlayerLose");
        }

        public void DealCard(DealCard_BJ dealCard)
        {
            NTLog.LogMessage(JsonUtility.ToJson(("DealCard", JsonUtility.ToJson(dealCard)), gameObject));
        }

        [Button]
        public void HitCard()
        {
            this.SendMsg<int>(Message_Key_Config.HitCard, 1);
        }
        [Button]
        public void Stand()
        {
            this.SendMsg<int>(Message_Key_Config.Stand, 1);
        }
    }
}