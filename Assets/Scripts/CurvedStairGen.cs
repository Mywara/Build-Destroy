using UnityEngine;
using System.Collections;

public class CurvedStairGen : MonoBehaviour
{
    private const int MIN_LONGUEUR = 0;
    private const int MAX_LONGUEUR = 9;
    private const int MIN_LARGEUR = 0;
    private const int MAX_LARGEUR = 4;
    private const int MIN_HAUTEUR = 0;
    private const int MAX_HAUTEUR = 9;

    public GameObject stair;
    // Use this for initialization
    void Start()
    {
        RandomGen longueurScaler = new RandomGen(MIN_LONGUEUR, MAX_LONGUEUR);
        int longueur = longueurScaler.GetNbr();
        RandomGen largeurScaler = new RandomGen(MIN_LARGEUR, MAX_LARGEUR);
        int largeur = largeurScaler.GetNbr();
        RandomGen hauteurScaler = new RandomGen(MIN_HAUTEUR, MAX_HAUTEUR);
        int hauteur = hauteurScaler.GetNbr();

        Vector3 scaler = new Vector3(largeur, hauteur, longueur);
        Vector3 origin = Input.mousePosition;
        GameObject go = Instantiate(stair, origin, Quaternion.identity);
        go.transform.localScale += scaler;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDrag()
    {
        Vector3 cursorPoint = Input.mousePosition;
        Vector3 current = stair.transform.position;
        Vector3 movement = cursorPoint - current;

        stair.transform.position += movement;

    }

}
