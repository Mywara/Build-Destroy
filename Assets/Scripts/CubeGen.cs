using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGen : MonoBehaviour {
    private const int MIN_SIDE = 0;
    private const int MAX_SIDE = 2;
    public GameObject cube;
	// Use this for initialization
	void Start () {


        
       // RandomGen sideScaler = new RandomGen(MIN_SIDE, MAX_SIDE);
       // int side = sideScaler.GetNbr();

        GameObject go = Instantiate(cube,new Vector3(-1000000,0,0), Quaternion.identity);
       // go.transform.localScale += new Vector3(side, side, side);
        RandomGen sideScaler = new RandomGen(MIN_SIDE, MAX_SIDE);
        int side = sideScaler.GetNbr();

        Vector3 origin = Input.mousePosition;
        GameObject go2 = Instantiate(cube, origin, Quaternion.identity);
        go.transform.localScale += new Vector3(side, side, side);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDrag()
    {
        Vector3 cursorPoint = Input.mousePosition;
        Vector3 current = cube.transform.position;
        Vector3 movement = cursorPoint - current;

        cube.transform.position += movement;

    }
}
