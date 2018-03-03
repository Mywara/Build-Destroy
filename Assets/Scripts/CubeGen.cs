using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGen : MonoBehaviour {
    private const int MIN_SIDE = 1;
    private const int MAX_SIDE = 3;
    public GameObject cube;
	// Use this for initialization
	void Start () {


        
        RandomGen sideScaler = new RandomGen(MIN_SIDE, MAX_SIDE);
        int side = sideScaler.GetNbr();

        GameObject go = Instantiate(cube,new Vector3(-1000000,0,0), Quaternion.identity);
        go.transform.localScale += new Vector3(side, side, side);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
