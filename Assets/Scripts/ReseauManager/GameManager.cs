using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Photon.PunBehaviour
{
    public static GameManager instance;

    void Awake()
    {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            instance = this;
            PhotonNetwork.automaticallySyncScene = true;
    }

   void Start()
   {
        PhotonNetwork.ConnectUsingSettings("Version_1.1");
   }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChooseRoom()
    {
        PhotonNetwork.LoadLevel("ChooseRoom");
    }
}

