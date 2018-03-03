using UnityEngine;
using System.Collections;

public class PoutreHorizontaleGen : MonoBehaviour
{
    private const int MIN_LONGUEUR = 2;
    private const int MAX_LONGUEUR = 9;
    private const int MIN_LARGEUR = 0;
    private const int MAX_LARGEUR = 2;
    public GameObject poutre;
    // Use this for initialization
    void Start()
    {
        RandomGen longueurScaler = new RandomGen(MIN_LONGUEUR, MAX_LONGUEUR);
        int longueur = longueurScaler.GetNbr();
        RandomGen hauteurScaler = new RandomGen(MIN_LARGEUR, MAX_LARGEUR);
        int hauteur = hauteur = hauteurScaler.GetNbr();
        RandomGen largueurScaler = new RandomGen(MIN_LARGEUR, MAX_LARGEUR);
        int largeur = largueurScaler.GetNbr();

        Vector3 scaler = new Vector3(longueur, hauteur, largeur);
        GameObject go = Instantiate(poutre, new Vector3(-1000000, 0, 0), Quaternion.identity);
        go.transform.localScale += scaler;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
