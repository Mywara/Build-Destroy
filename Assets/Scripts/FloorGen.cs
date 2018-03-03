using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGen : MonoBehaviour {

    private const int MIN_WINTH = 0;
    private const int MAX_WINTH = 9;
    public GameObject floor;
	// Use this for initialization
	void Start() {
        RandomGen largeurScaler = new RandomGen(MIN_WINTH, MAX_WINTH);
        int largeur = largeurScaler.GetNbr();
        RandomGen longueurScaler = new RandomGen(MIN_WINTH, MAX_WINTH);
        int longueur = longueurScaler.GetNbr();

        Vector3 scaler = new Vector3(largeur, 0, longueur);
        Vector3 origin = Input.mousePosition;
        GameObject go = Instantiate(floor, origin, Quaternion.identity);
        go.transform.localScale += scaler;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDrag()
    {
        Vector3 cursorPoint = Input.mousePosition;
        Vector3 current = floor.transform.position;
        Vector3 movement = cursorPoint - current;

        floor.transform.position += movement;

    }
}
