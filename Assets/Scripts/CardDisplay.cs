using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

    // Variable declarations
    public Card card;

    public Text costText;
    public Image image;

	// Use this for initialization
	void Start () {
        costText.text = card.cost;
        image.sprite = card.image;
	}
}
