using UnityEngine;
using System.Collections;

public class TorusGen : MonoBehaviour
{
    private const int MIN_HAUTEUR = 2;
    private const int MAX_HAUTEUR = 9;
    private const int MIN_RADIUS = 0;
    private const int MAX_RADIUS = 2;

    public GameObject torus;
    // Use this for initialization
    void Start()
    {
        RandomGen radiusScaler = new RandomGen(MIN_RADIUS, MAX_HAUTEUR);
        int radius = radiusScaler.GetNbr();
        RandomGen hauteurScaler = new RandomGen(MIN_HAUTEUR, MAX_HAUTEUR);
        int hauteur = hauteurScaler.GetNbr();

        Vector3 scaler = new Vector3(radius, hauteur, radius);
        GameObject go = Instantiate(torus, new Vector3(100000, 1, 0), Quaternion.identity);
        go.transform.localScale += scaler;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDrag()
    {
        Vector3 cursorPoint = Input.mousePosition;
        Vector3 current = torus.transform.position;
        Vector3 movement = cursorPoint - current;

        torus.transform.position += movement;

    }
}
