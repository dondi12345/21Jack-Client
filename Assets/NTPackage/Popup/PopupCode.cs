using System;
using UnityEngine;

namespace NTPackage.UI
{
    public class PopupCodeParser
    {
        public static PopupCode FromString(string name)
        {
            //name = name.ToLower();W
            return (PopupCode)Enum.Parse(typeof(PopupCode), name);
        }
    }

    [System.Serializable]
    public enum PopupCode
    {
        Unknown = 0,

        FinlordUI = 1,
        GearUI = 2,
        TorchUI = 3,
        HeroDetailUI = 4,
        IdleLootUI = 5,
        QuestUI = 6,
        LoginUI = 7,
        ChoseServerUI = 8,
        LoadingUI = 9,
        ChatUI,
        DungeonUI,
        FriendUI,
    }
}
