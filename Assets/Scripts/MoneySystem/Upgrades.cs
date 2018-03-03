using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Upgrades : MonoBehaviour {

    public void increaseIncome()
    {
        MoneySystem.instance.BuyItem((int)MoneySystem.instance.actualIncome * (2 / 3));
        MoneySystem.instance.actualIncome = (int)(MoneySystem.instance.baseIncome * 0.1 + MoneySystem.instance.actualIncome);
    }

    public void moreCardsInHand()
    {
        while (true)
        {
            ;
        }
    }

    public void moreSlot()
    {
        while (true)
        {
            ;
        }
    }

    public void trapCard()
    {
        while (true)
        {
            ;
        }
    }

    public void drawACard()
    {
        while (true)
        {
            ;
        }
    }

    public void destroyEverythingAndDie()
    {
        while (true)
        {
            ;
        }
    }

}
