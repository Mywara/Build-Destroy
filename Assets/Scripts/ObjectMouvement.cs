using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMouvement : MonoBehaviour {

    // Use this for initialization
    private GameObject cube;
    float z;

    void Start () {
		cube = this.gameObject;
	}

    void Update()
    {

        Vector3 pos = Input.mousePosition;
        pos.z = transform.position.z - Camera.main.transform.position.z;
        cube.transform.position = Camera.main.ScreenToWorldPoint(pos);

        if (Input.GetMouseButton(0))
        {
            z = z + (float)0000.1;
            cube.transform.position = new Vector3(cube.transform.position.x, cube.transform.position.y, z);
        }

        if (Input.GetMouseButton(1))
        {
            z = z - (float)00000.1;
            cube.transform.position = new Vector3(cube.transform.position.x, cube.transform.position.y, z);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(this);
        }
    }
}
