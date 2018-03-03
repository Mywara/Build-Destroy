using UnityEngine;
using System.Collections;

public class WallGen : MonoBehaviour
{

    private const int MIN_LONGUEUR = 0;
    private const int MAX_LONGUEUR = 9;
    private const int MIN_LARGEUR = 0;
    private const int MAX_LARGEUR = 9;

    public GameObject wall;

    // Use this for initialization
    void Start()
    {
        RandomGen largeurScaler = new RandomGen(MIN_LARGEUR, MAX_LARGEUR);
        int largeur = largeurScaler.GetNbr();
        RandomGen longueurScaler = new RandomGen(MIN_LONGUEUR, MAX_LONGUEUR);
        int longueur = longueurScaler.GetNbr();

        Vector3 scaler = new Vector3(longueur, largeur, 0);
        GameObject go = Instantiate(wall, new Vector3(-1000000, 0, 0), Quaternion.identity);
        go.transform.localScale += scaler;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
