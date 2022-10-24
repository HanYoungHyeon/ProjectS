using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    public List<Gem> detectionGems = new List<Gem>();
    public float playerGold;

    //Gem 획득시 이벤트, 이미 얻은 젬이라면 무시하고 처음 얻는 젬이라면 detectionGems랑 encountGems에 추가 (market에서 사용)
    public void TakeGem(Gem gem)
    {
        if (detectionGems.Contains(gem)) return;
        detectionGems.Add(gem);
    }

    public void TakeItem(Item item)
    {
        Gem tempGem = item as Gem;
        if(tempGem!=null)
        {
            TakeGem(tempGem);
        }
    }


}
