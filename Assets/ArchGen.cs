using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchGen : MonoBehaviour {

    private const int MIN_DEPTH = 1;
    private const int MAX_DEPTH = 5;
    private const int MIN_RADIUS = 1;
    private const int MAX_RADIUS = 5;

	// Use this for initialization
	void Start () {
        RandomGen depthScaler = new RandomGen(MIN_DEPTH, MAX_DEPTH);
        int depth = depthScaler.GetNbr();
        RandomGen radiusScaler = new RandomGen(MIN_RADIUS, MAX_RADIUS);
        int radius = radiusScaler.GetNbr();
       // Instantiate(Arch, new Vector3(radius, 1, depth, 6), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
