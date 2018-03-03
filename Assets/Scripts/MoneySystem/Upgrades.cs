using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Upgrades : MonoBehaviour {

    public Text costIncInc;
    public Text costMoreCards;
    public Text costMoreStock;
    public Text costHiddenCard;
    private int cost_inc_inc;
    private int cost_more_cards;     //Depends on number of cards in hands
    private int cost_more_stocks;    //Depends on number of stock
    private int cost_hidden_card;    //Depends on number of cards hidden and number of cards in hands


    public void Awake()
    {
        updateCost();
        updateCostText();
    }

    public void updateCost()
    {
        cost_inc_inc = (int)MoneySystem.instance.actualIncome * 2 / 3;
        cost_hidden_card = (int)1;
        cost_more_cards = (int)1;
        cost_more_stocks = (int)1;
    }

    public void updateCostText()
    {
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
            updateCost();
        }
       
    }

    public void moreCardsInHand()
    {
        if (MoneySystem.instance.BuyItem(cost_more_cards))
        {
           
        }
    }

    public void moreStock()
    {
        if (MoneySystem.instance.BuyItem(cost_more_stocks))
        {
           
        }
    }

    public void trapCard()
    {
        if (MoneySystem.instance.BuyItem(cost_hidden_card))
        {
           
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
