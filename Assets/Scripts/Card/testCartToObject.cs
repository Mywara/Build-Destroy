using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCartToObject : MonoBehaviour {


    public GameObject detroyObject;
    public GameObject prefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void TesttoDestroy()
    {
        Destroy(detroyObject);
        Quaternion q = new Quaternion();
        Instantiate(prefab, Vector3.zero,q);
    }
}
