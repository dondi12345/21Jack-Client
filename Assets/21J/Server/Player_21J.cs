using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colyseus.Schema;

namespace Rubik._21J
{
    public class Player_21J : Schema
    {
        [Type(0, "string")]
        public string SessionId = "";
        [Type(1, "number")]
        public float score = 0;
        [Type(2, "number")]
        public float  health = 0;

        public override string ToString(){    
            return JsonUtility.ToJson(this);
        }
    }
}
