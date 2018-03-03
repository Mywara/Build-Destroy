using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{

    // Variable declarations
    public Card card;
    public GameObject cardObject;
    public Text costText;
    public Image image;
    public Button b;

    public GameObject cardSlotPrefab;
    public GridLayoutGroup grid;

    // Use this for initialization
    private void Start()
    {
        SetCard();
        b.GetComponentInChildren<Button>();
        cardObject = card.blockPrefab;
    }

    public void SetCard()
    {
        costText.text = card.cost;
        b.image.sprite = card.image;
    }

    public void AddCard()
    {
        GameObject item = Instantiate(cardSlotPrefab, Vector3.zero, Quaternion.identity);
        item.transform.SetParent(grid.transform, false);
        item.transform.localScale = new Vector3(1, 1, 1);
        item.transform.localPosition = Vector3.zero;

        CardManager._instance.DrawCard(item);
    }

}
