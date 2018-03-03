using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : Photon.PunBehaviour {


    private GameObject roomManager;
    public Text roomActvies;
    public Text createRoomText;
    public Text numberText;
    public Text joinRoomText;
    public string LevelToLoad = "SceneTest";
    public int nbPlayers = 6;

    public void Start()
    {
        roomManager = this.gameObject;
    }


    public void Update()
    {
        
  
    }

    public void CreateRoom()
    {
        string name = createRoomText.text;
        RoomOptions ro = new RoomOptions();

        int i;
        System.Int32.TryParse(numberText.text, out i);
        ro.MaxPlayers = (byte)i;
        PhotonNetwork.JoinOrCreateRoom(name, ro, TypedLobby.Default);
    }

    public void JoinRoom()
    {
        string name = joinRoomText.text;
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 4;
        PhotonNetwork.JoinRoom(name);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.LoadLevel(LevelToLoad);
        }
    }


    public void RefreshRoom()
    {
        roomActvies.text = "Active Rooms";
        foreach (RoomInfo game in PhotonNetwork.GetRoomList())
        {
            roomActvies.text = roomActvies.text + "\n Room name : " + game.Name + "  Player :  " + game.PlayerCount + "/" + game.MaxPlayers;
        }
    }

    void OnLevelWasLoaded(int levelNumber)
    {
        if (!PhotonNetwork.inRoom) return;
        if (LevelToLoad.Equals("Playground") && PhotonNetwork.isMasterClient)
        {
            ArenaManager.instance.photonView.RPC("CreateArena", PhotonTargets.AllBufferedViaServer, nbPlayers);
        }
    }


}
