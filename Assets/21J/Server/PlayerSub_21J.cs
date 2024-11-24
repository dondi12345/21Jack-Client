using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Rubik._21J
{
    [System.Serializable]
    public class PlayerInfo_21J
    {
        public string ID = "";
        public string SessionId = "";
        public string Name = "";
        public string Avata = "";
        public int Bet = 0;

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>{
                {"ID", this.ID},
                {"SessionId", this.SessionId},
                {"Name", this.Name},
                {"Avata", this.Avata},
                {"Bet", this.Bet},
            };
        }
    }

    [System.Serializable]
    public class PlayerData_21J
    {
        public string SessionId;
        public int[] Cards;
        public int HoldCard = -1;
        public int[] WhiteCard;
        public CardSlot[] Slot;
    }

    [System.Serializable]
    public class CardSlot
    {
        public int[] Cards;
        public int Point;
    }

    [System.Serializable]
    public class PlayerState_21J
    {
        public string SessionId = "";
        public int score = 0;
        public int health = 0;
    }

    [System.Serializable]
    public class PlayerJoinResult_21J
    {
        public string error = "";
        public PlayerInfo_21J playerInfo;
    }

    public class HitCard_21J
    {
        public int slot = 0;
    }
    public class ResultHitCard_21J
    {
        public int slot;
        public CardSlot CardSlot;
    }

    public class HoldCardResult_21J
    {
        public int[] Cards;
        public int HoldCard;
    }

    public class ResultHitHoldCard_21J
    {
        public int slot;
        public CardSlot CardSlot;
    }

}