﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardManager : MonoBehaviour
{

    // Variables declarations
    public List<Card> cardList; // List of possible cards
    private List<GameObject> cardInHand; // List of the UI objects in the player's hand

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }

    /// <summary>
    /// Method to be called when you need to draw a hand of card
    /// </summary>
    /// <param name="cards">Int for the number of cards the player is allowed in their hand this turn</param>
    public void DrawHand(int cards)
    {
        // Finds all of the card slots in the player's UI
        foreach (var obj in GameObject.FindObjectsOfType<GameObject>().Where(o => o.tag == "Cards"))
        {
            cardInHand.Add(obj);
        }

        /*
         Needs a randomizer to select the cards from the possible cards list
         and add it to the cardInHand object
        */
        foreach(var obj in cardInHand)
        {
            CardDisplay _cd = obj.GetComponent<CardDisplay>();

            // Randomly select a card from the possible card list, the index 0 should be replaced by the random number
            Card selectedCard = cardList[0];
            _cd.card = selectedCard;
            _cd.SetCard();
        }
    }
}
