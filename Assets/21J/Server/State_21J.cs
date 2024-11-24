using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colyseus.Schema;

namespace Rubik._21J
{
    [System.Serializable]
    public class StateData{
        public GameState_21J status;
        public int timeTurn;
    }

    public class State_21J : Schema
    {
        [Type(0, "number")]
        public float status = 0;
        [Type(1, "map", typeof(MapSchema<Player_21J>))]
        public MapSchema<Player_21J> players = new MapSchema<Player_21J>();
        [Type(2, "number")]
        public float timeTurn = 0;

        public override string ToString(){    
            return JsonUtility.ToJson(this);
        }
    }
}
