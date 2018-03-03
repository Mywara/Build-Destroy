﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : Photon.PunBehaviour {

    static public ArenaManager instance;
    public int distanceBetweenPlayers = 40;
    public int nbPlayers = 6;

    public List<GameObject> playerZonesList;

    void Awake() {
        if(instance != null && instance != this)
        {
            Debug.Log("There already is a RoomManager");
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        if (!PhotonNetwork.connected)
        {
            Debug.Log("Not connected to Photon!");
            return;
        }

        CreateArena();
    }

	// Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //[PunRPC]
    public void CreateArena() {

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

            // ajout de la zone de jeu créée à la liste du manager
            playerZonesList.Add(newPlayerZone);
        }
    }

    public GameObject GetPlayerZone(int id) {
        
        if(id <= PhotonNetwork.room.MaxPlayers && id > 0)
        {
            return playerZonesList[id - 1];
        }
        else
        {
            return null;
        }
    }
}
