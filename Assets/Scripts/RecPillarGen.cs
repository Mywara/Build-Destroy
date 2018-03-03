using UnityEngine;
using System.Collections;

public class RecPillarGen : MonoBehaviour
{
    private const int MIN_HAUTEUR = 2;
    private const int MAX_HAUTEUR = 9;
    private const int MIN_LARGEUR = 0;
    private const int MAX_LARGEUR = 4;

    public GameObject pillar;

    // Use this for initialization
    void Start()
    {
        RandomGen hauteurScaler = new RandomGen(MIN_HAUTEUR, MAX_HAUTEUR);
        int hauteur = hauteurScaler.GetNbr();
        RandomGen LargeurScaler = new RandomGen(MIN_LARGEUR, MAX_LARGEUR);
        int largeur = LargeurScaler.GetNbr();
        RandomGen longueurScaler = new RandomGen(MIN_LARGEUR, MAX_LARGEUR);
        int longueur = longueurScaler.GetNbr();

        Vector3 scaler = new Vector3(longueur, hauteur, largeur);

        GameObject go = Instantiate(pillar, new Vector3(-1000000, 0, 0), Quaternion.identity);
        go.transform.localScale += scaler;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
