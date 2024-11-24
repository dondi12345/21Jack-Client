using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colyseus.Schema;

namespace Rubik._BJ
{
    [System.Serializable]
    public class StateData{
        public StatusStatus_BJ status;
        public int timeTurn;
    }

    public class State_BJ : Schema
    {
        [Type(0, "number")]
        public float status = 0;
        [Type(1, "map", typeof(MapSchema<Player_BJ>))]
        public MapSchema<Player_BJ> players = new MapSchema<Player_BJ>();
        [Type(2, "number")]
        public float timeTurn = 0;

        public override string ToString(){    
            return JsonUtility.ToJson(this);
        }
    }
}
