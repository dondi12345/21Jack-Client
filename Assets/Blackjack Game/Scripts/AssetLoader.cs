using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AssetLoader : MonoBehaviour
{

    public Sprite[] lsCardSprs;
    public static AssetLoader instance;
    private void Awake()
    {
        instance = this;
        //foreach (BaseCharacterDataSO data in ListHeroDatas)
        //{
        //    heroCollection.Add(data.baseData.Index, data);
        //}
        //foreach (BaseCharacterDataSO data in ListEnemyDatas)
        //{
        //    enemyCollection.Add(data.baseData.Index, data);
        //}
        //for(int i = 0;i< assets[3].prefabs.Length; i++)
        // {
        //     Debug.Log(assets[3].prefabs[i].name);
        // }
    }
   
    public Sprite GetCardSprite(int type)
    {
        return this.lsCardSprs[type];
    }

}
