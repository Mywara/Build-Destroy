using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{

    // Variable declarations
    public Card card;

    public Text costText;
    public Image image;

    // Use this for initialization
    private void Start()
    {
        SetCard();
    }

    public void SetCard()
    {
        costText.text = card.cost;
        image.sprite = card.image;
    }

    // When the mouse is clicked
    public void OnMouseDown()
    {
        // Instantiate the GameObject stored in card.cardPrefab
    }
}
