using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    // Singleton Structure
    #region Singleton
    public static CardManager _instance;

    private static CardManager instance
    {
        get
        {
            if (_instance == null)
            {
                if (GameObject.Find("CardManager"))
                {
                    GameObject g = GameObject.Find("CardManager");
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
    #endregion Singleton

    // Variables declarations
    public List<Card> cardList; // List of possible cards
    private List<GameObject> cardInHand; // List of the UI objects in the player's hand
    public GameObject cardSlotPrefab; // Prefab to the card slot, needed to add a card to an existing hand
    public GridLayoutGroup grid; // The gridLayoutGroup is the scalable hand of the player

    private void Start()
    {
        DrawHand();
    }

    /// <summary>
    /// Method to be called when you need to draw a hand of card
    /// </summary>
    public void DrawHand()
    {
        // Finds all of the card slots in the player's UI
        foreach (var obj in GameObject.FindObjectsOfType<GameObject>().Where(o => o.tag == "Cards"))
        {
            cardInHand.Add(obj);
        }

        // Randomly assign a card in each slot
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
    /// <param name="obj">The method need a reference to the cardDisplay item to use for the new card</param>
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

    /// <summary>
    /// Method that allows to add a card to the existing hand
    /// </summary>
    public void AddCard()
    {
        GameObject item = Instantiate(cardSlotPrefab, Vector3.zero, Quaternion.identity);
        item.transform.SetParent(grid.transform, false);
        item.transform.localScale = new Vector3(1, 1, 1);
        item.transform.localPosition = Vector3.zero;

        CardManager._instance.DrawCard(item);
    }
}
