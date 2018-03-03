using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchViewBetweenPlayers : MonoBehaviour {

    public Text playerName;
    public Transform playerList;
    public GameObject playerButtonPrefab;
    static public SwitchViewBetweenPlayers instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("There already is an SwitcbViewBetweenPlayers");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        playerName.text = PlayerPrefs.GetString("PlayerName");
    }

    public void AddAButton(string playerName, int playerPhotonId)
    {
        GameObject buttonCreated = Instantiate(playerButtonPrefab, Vector3.zero, Quaternion.identity);
        buttonCreated.transform.SetParent(playerList);
        buttonCreated.GetComponentInChildren<Text>().text = playerName;
        buttonCreated.GetComponent<KeepPhotonPlayerId>().PlayerPhotonId = playerPhotonId;
        buttonCreated.GetComponent<Button>().onClick.AddListener(delegate { SwitchView(buttonCreated.GetComponent<KeepPhotonPlayerId>().PlayerPhotonId); });
    }

    void SwitchView(int playerID)
    {
        GameObject playerZone = ArenaManager.instance.GetPlayerZone(playerID);
        Debug.Log("PlayerID : " + playerID);
        CameraController camController = Camera.main.GetComponent<CameraController>();
        if (playerZone == null)
        {
            Debug.Log("La zone joueur récupérée est nulle...");
        }
        else
        {
            Transform focusPoint = playerZone.transform.GetChild(1);
            camController.SetTarget(focusPoint);
            playerName.text = ArenaManager.instance.playerNamesList[playerID-1];
        }
    }
}
