using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Upgrades : MonoBehaviour {

    public Text costIncInc;
    public Text costMoreCards;
    public Text costMoreStock;
    public Text costHiddenCard;
    public int cost_inc_inc;
    public int cost_more_cards = (int)1;     //Depends on number of cards in hands
    public int cost_more_stocks = (int)2;    //Depends on number of stock
    public int cost_hidden_card = (int)3;    //Depends on number of cards hidden and number of cards in hands

   
    public void Awake()
    {
        cost_inc_inc = (int)MoneySystem.instance.actualIncome * 2 / 3;
        Debug.Log(cost_inc_inc);
        Debug.Log(MoneySystem.instance.actualIncome);
        Debug.Log((int)MoneySystem.instance.actualIncome * 2 / 3);

        costIncInc.text = "Cost : " + cost_inc_inc;
        costMoreCards.text = "Cost : " + cost_more_cards;
        costMoreStock.text = "Cost : " + cost_more_stocks;
        costHiddenCard.text = "Cost : " + cost_hidden_card;
    }

    public void increaseIncome()
    {

        if (MoneySystem.instance.BuyItem(cost_inc_inc))
        {
            MoneySystem.instance.actualIncome = (int)(MoneySystem.instance.baseIncome * 0.1 + MoneySystem.instance.actualIncome);
        }
    }

    public void moreCardsInHand()
    {
        while (true)
        {
            ;
        }
    }

    public void moreStock()
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
