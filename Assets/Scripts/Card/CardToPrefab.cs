using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToPrefab : MonoBehaviour {

    public GameObject detroyObject;
    public GameObject prefab;

    // Use this for initialization
    void Start () {
		
	}

    public void DestroyAndCreate()
    {
        Destroy(this.gameObject);
        Quaternion q = new Quaternion();
        Instantiate(prefab, Vector3.zero, q);
    }
}
