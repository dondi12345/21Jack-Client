using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik._BJ
{
    [System.Serializable]
    public class PlayerInfo_BJ
    {
        public string ID = "";
        public string SessionId = "";
        public string Name = "";
        public string Avata = "";

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>{
                {"ID", this.ID},
                {"SessionId", this.SessionId},
                {"Name", this.Name},
                {"Avata", this.Avata},
            };
        }
    }

    [System.Serializable]
    public class PlayerData_BJ
    {
        public string SessionId;
        public List<CardData_BJ> CardDatas = new List<CardData_BJ>();
    }

    [System.Serializable]
    public class CardData_BJ
    {
        public List<int> Cards = new();
        public bool Stand = false;
        public int Point;
    }

    [System.Serializable]
    public class PlayerJoinResult_BJ
    {
        public string error = "";
        public PlayerInfo_BJ playerInfo;
    }

    [System.Serializable]
    public class DealCard_BJ
    {
        public DealCardPlayer_BJ[] DealCardPlayers;
    }

    [System.Serializable]
    public class DealCardPlayer_BJ
    {
        public string SessionId;
        public int[] Cards;
    }
}
