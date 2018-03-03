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
        GameObject go = Instantiate(Arch, new Vector3(100000, 1, 0), Quaternion.identity);
        go.transform.localScale += scaler;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
