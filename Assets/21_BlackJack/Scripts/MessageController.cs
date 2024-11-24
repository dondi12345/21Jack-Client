using System.Collections;
using System.Collections.Generic;
using DamageNumbersPro;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rubik.BlackJackMiniGame
{
    public class MessageController : MonoBehaviour
    {
        public static MessageController Instance;
        [SerializeField] DamageNumber damageNumber;
        [SerializeField] DamageNumber notiText;
  
        

        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
        }

        public void ShowFx()
        {
            
        }

        public void DamageNumberSpawn(Vector2 pos, string damage)
        {
            damageNumber.Spawn(pos, damage);
        }
        [Button]
        public void ShowNotiText(string _text)
        {
            notiText.Spawn(new Vector3(0,0,0), _text ).SetAnchoredPosition(GameObject.FindObjectOfType<Canvas>().transform, Vector2.zero);
            notiText.transform.localScale = new Vector3(2, 2);
            //notiText.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
        }
        public void ShowNotiText(Vector2 pos,string _text)
        {
            notiText.Spawn(pos, _text).SetAnchoredPosition(GameObject.FindObjectOfType<Canvas>().transform, Vector2.zero);
            notiText.transform.localScale = new Vector3(2, 2);
            //notiText.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
        }

    }
}