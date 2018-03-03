using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Photon.PunBehaviour
{
    public static GameManager instance;
    public string levelToLoad = "Playground";

    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("Version_1.0");
    }

    public void JoinGame()
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 6;
        PhotonNetwork.JoinOrCreateRoom("Default Room", ro, null);
    }


    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.LoadLevel(levelToLoad);
        }
    }

    void OnLevelWasLoaded(int levelNumber)
    {
        if (!PhotonNetwork.inRoom) return;
        if(levelToLoad.Equals("Playground") && PhotonNetwork.isMasterClient)
        {
            //ArenaManager.instance.photonView.RPC("CreateArena", PhotonTargets.AllBufferedViaServer, nbPlayers);
        }
    }
}

