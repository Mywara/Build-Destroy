using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    // Variable declarations
    public Card card;
    public GameObject blockPrefab;
    public Text costText;
    public Button b;
    public Vector3 mousePos;

    /// <summary>
    /// Sets the cost text and the image to the correct values
    /// </summary>
    /// <param name="card">The parameters is the card you want to use for this CardDisplay</param>
    public void SetCard(Card card)
    {
        this.card = card;
        this.blockPrefab = card.blockPrefab;
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
