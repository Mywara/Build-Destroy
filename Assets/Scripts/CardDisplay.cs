using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{

    // Variable declarations
    public Card card;

    public Text costText;
    public Image image;

    public GameObject cardSlotPrefab;
    public GridLayoutGroup grid;

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

    public void AddCard()
    {
        GameObject item = Instantiate(cardSlotPrefab, Vector3.zero, Quaternion.identity);
        item.transform.SetParent(grid.transform, false);
        item.transform.localScale = new Vector3(1, 1, 1);
        item.transform.localPosition = Vector3.zero;

        CardManager.DrawCard(item);
    }

    // When the mouse is clicked
    public void OnMouseDown()
    {
        // Instantiate the GameObject stored in card.cardPrefab
        // Call the instantiation from the prefab script
        // Use the buyItem method from the moneySystem
    }
}
