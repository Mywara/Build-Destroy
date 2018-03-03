using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardToBlock : MonoBehaviour
{
    public void CardToObject(GameObject cardPrefab)
    {

        //obtenir la souris
        //GameObject prefab = cardPrefab.GetComponent<CardDisplay>().blockPrefab;
        //GameObject item = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        //mettre la souris en enfant
        Destroy(cardPrefab);


    }
    // Use this for initialization

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

}

 
