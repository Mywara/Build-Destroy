using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : Photon.PunBehaviour {

    private List<GameObject> playersList = new List<GameObject>();
    private List<GameObject> playerZonesList = new List<GameObject>();

    static public ArenaManager instance;

    void Awake() {
        if(instance != null && instance != this)
        {
            Debug.Log("There already is a RoomManager");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [PunRPC]
    public void CreateArena(int nbPlayers, int distanceBetweenPlayers) {

        // calcul de l'angle séparant deux joueurs
        float angleBetweenPlayers = 2 * Mathf.PI / nbPlayers;
        // calcul du rayon de l'aréna
        float arenaRadius = distanceBetweenPlayers / (2 * Mathf.Sin(angleBetweenPlayers));

        Vector3 spawnPosition;
        GameObject newPlayerZone;

        for (int i = 0; i < nbPlayers; i++)
        {
            // calcul de la position de la zone de jeu du joueur i
            spawnPosition = new Vector3(
                Mathf.Cos(i * angleBetweenPlayers) * arenaRadius,
                0f,
                Mathf.Sin(i * angleBetweenPlayers) * arenaRadius
            );

            // instanciation de la zone de jeu
            newPlayerZone = PhotonNetwork.Instantiate("PlayerZone", spawnPosition, Quaternion.identity, 0);

            // la zone de jeu créée est définie comme fille du GameObject Arena auquel est rattaché ce script
            newPlayerZone.transform.parent = gameObject.transform;
        }
    }
}
