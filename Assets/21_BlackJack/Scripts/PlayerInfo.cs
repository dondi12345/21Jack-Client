using System.Collections;
using System.Collections.Generic;
using Rubik._21J;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInfo : MonoBehaviour
{
    public Image avatar;
    public Text nameTxt, scoreTxt;
    public GameObject[] lifes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetupInfo(string name,string ava, PlayerState_21J player)
    {
        nameTxt.text = name;
        scoreTxt.text = player.score.ToString();
        for(int i = 0; i < lifes.Length; i++)
        {
            if (i < player.health)
            {
                lifes[i].SetActive(true);
            }
            else
            {
                lifes[i].SetActive(false);
            }
        }
    }
}
