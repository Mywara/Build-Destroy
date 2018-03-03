using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchGen : MonoBehaviour {

    private const int MIN_DEPTH = 0;
    private const int MAX_DEPTH = 4;
    private const int MIN_RADIUS = 0;
    private const int MAX_RADIUS = 4;

    public GameObject Arch;
	// Use this for initialization
	void Start () {
        RandomGen depthScaler = new RandomGen(MIN_DEPTH, MAX_DEPTH);
        int depth = depthScaler.GetNbr();
        RandomGen radiusScaler = new RandomGen(MIN_RADIUS, MAX_RADIUS);
        int radius = radiusScaler.GetNbr();

        Vector3 scaler = new Vector3(depth, radius, 1);
        Vector3 origin = Input.mousePosition;
        GameObject go = Instantiate(Arch, origin, Quaternion.identity);
        go.transform.localScale += scaler;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnMouseDrag()
    {
        Vector3 cursorPoint = Input.mousePosition;
        Vector3 current = Arch.transform.position;
        Vector3 movement = cursorPoint - current;

        Arch.transform.position += movement;

    }
}
