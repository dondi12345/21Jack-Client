using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colyseus.Schema;

namespace Rubik._BJ
{
    [System.Serializable]
    public class PlayerState_BJ{
        public string SessionId = "";
        public PlayerStatus_BJ status = 0;
        public float  gold = 0;
    }

    public class Player_BJ : Schema
    {
        [Type(0, "string")]
        public string SessionId = "";
        [Type(1, "number")]
        public float status = 0;
        [Type(2, "number")]
        public float  gold = 0;

        public override string ToString(){    
            return JsonUtility.ToJson(this);
        }
    }
}
