using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardManager : MonoBehaviour
{
    // Singleton Structure
    #region singleton
    public static CardManager _instance;

    private static CardManager instance
    {
        get
        {
            if (_instance == null)
            {
                if (GameObject.Find("MoneySystem"))
                {
                    GameObject g = GameObject.Find("MoneySystem");
                    if (g.GetComponent<CardManager>())
                    {
                        _instance = g.GetComponent<CardManager>();
                    }
                    else
                    {
                        _instance = g.AddComponent<CardManager>();
                    }
                }
                else
                {
                    GameObject g = new GameObject();
                    g.name = "MoneySystem";
                    _instance = g.AddComponent<CardManager>();
                }
            }

            return _instance;
        }

        set
        {
            _instance = value;
        }
    }
    #endregion singleton

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

        foreach (var obj in cardInHand)
        {
            CardDisplay _cd = obj.GetComponent<CardDisplay>();

            // Randomly select a card from the possible card list
            UnityEngine.Random.InitState(Time.frameCount);
            var ranNum = UnityEngine.Random.Range(0, cardList.Count);
            Card selectedCard = cardList[ranNum];
            _cd.card = selectedCard;
            _cd.SetCard();
        }
    }

    /// <summary>
    /// Method to be called when only one card needs to be drawn
    /// </summary>
    public void DrawCard(GameObject obj)
    {
        CardDisplay _cd = obj.GetComponent<CardDisplay>();

        // Randomly select a card from the possible card list
        UnityEngine.Random.InitState(Time.frameCount);
        var ranNum = UnityEngine.Random.Range(0, cardList.Count);
        Card selectedCard = cardList[ranNum];
        _cd.card = selectedCard;
        _cd.SetCard();
    }
}
