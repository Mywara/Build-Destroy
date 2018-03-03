using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    // Variable declarations
    public Card card;
    public GameObject blockPrefab;
    public Text costText;
    public Image image;
    public Button b;
    public Vector3 mousePos;

    // Use this for initialization
    private void Start()
    {
        SetCard();
        b.GetComponentInChildren<Button>();
        blockPrefab = card.blockPrefab;
    }

    /// <summary>
    /// Sets the cost texte and the image to the correct values
    /// </summary>
    public void SetCard()
    {
        costText.text = card.cost;
        b.image.sprite = card.image;
    }

    /// <summary>
    /// Method to be called when the card button is clicked
    /// It will instantiate the block prefab in the game world at the mouse position
    /// </summary>
    public void CardClicked()
    {
        //obtenir la souris
        //GameObject prefab = cardPrefab.getComponent<CardDisplay>().cardObject;
        //GameObject item = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        //mettre la souris en enfant
        //Destroy(cardPrefab);

        mousePos = Input.mousePosition;
        GameObject item = Instantiate(blockPrefab, Vector3.zero, Quaternion.identity);
        item.transform.position = mousePos;
    }
}
