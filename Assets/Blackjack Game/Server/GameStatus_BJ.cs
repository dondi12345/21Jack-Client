using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik._BJ
{
    public enum StatusStatus_BJ
    {
        Waiting,
        DealMoney,
        DealCard,
        PlayerDeal,
        DealerDealing,
        EndGame,
    }

    public enum PlayerStatus_BJ
    {
        None,
        CardDealing,
        CardDealingDone,
    }
}
