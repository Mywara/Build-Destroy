using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyManager : Photon.PunBehaviour {

    public float drawPhaseTime = 5f;
    public float stealPhaseTime = 5f;
    public float buildPhaseTime = 5f;
    public float destructionPhaseTime = 5f;
    public float upgradePhaseTime = 5f;
    public Text phaseName;
    public Button readyForNewPhase;
    public ArenaManager arenaManager;

    private GameObject playerZone;
    private int PlayerID = 1;
    private int nbMaxPlayer;
    private int nbPlayerReady;
    private float startPhaseTime;
    private bool drawPhase = false;
    private bool stealPhase = false;
    private bool buildPhase = false;
    private bool destructionPhase = false;
    private bool upgradePhase = false;
    private bool iAmReady = false;

    private void Awake()
    {
        //set le nombre de joueur max ie nb joueur necessaire rdy pour changer de phase
        if(PhotonNetwork.connected)
        {
            nbMaxPlayer = PhotonNetwork.room.MaxPlayers;
            PlayerID = PhotonNetwork.player.ID;
        }
        else
        {
            nbMaxPlayer = 1;
        }

        nbPlayerReady = 0;
        readyForNewPhase.GetComponent<Image>().color = Color.red;
        UpdateReadyButtonText();
    }

    // Use this for initialization
    void Start () {
        playerZone = arenaManager.GetPlayerZone(PlayerID);

        //si on est pas le masterclient, on ne fait rien
        if (!PhotonNetwork.isMasterClient && PhotonNetwork.connected)
        {
            return;
        }
        Turn();
    }
	
	// Update is called once per frame
	void Update () {
        if(!PhotonNetwork.isMasterClient && PhotonNetwork.connected)
        {
            return;
        }
		if(drawPhase)
        {
            //si on est dans la phase de pioche et que le timer à fini, ou que tous les joueurs sont rdy
            //on change de phase
            if (Time.time > startPhaseTime + drawPhaseTime || nbPlayerReady == nbMaxPlayer)
            {
                drawPhase = false;
                photonView.RPC("ResetPhase", PhotonTargets.AllViaServer);
                if (PhotonNetwork.connected)
                {
                    photonView.RPC("StealPhase", PhotonTargets.AllViaServer);
                }
                else
                {
                    StealPhase();
                } 
            }
        }
        else if (stealPhase)
        {
            if (Time.time > startPhaseTime + stealPhaseTime || nbPlayerReady == nbMaxPlayer)
            {
                stealPhase = false;
                photonView.RPC("ResetPhase", PhotonTargets.AllViaServer);
                if (PhotonNetwork.connected)
                {
                    photonView.RPC("BuildPhase", PhotonTargets.AllViaServer);
                }
                else
                {
                    BuildPhase();
                }
            }
        }
        else if (buildPhase)
        {
            if (Time.time > startPhaseTime + buildPhaseTime || nbPlayerReady == nbMaxPlayer)
            {
                buildPhase = false;
                photonView.RPC("ResetPhase", PhotonTargets.AllViaServer);
                if (PhotonNetwork.connected)
                {
                    photonView.RPC("DestructionPhase", PhotonTargets.AllViaServer);
                }
                else
                {
                    DestructionPhase();
                }
            }
        }
        else if (destructionPhase)
        {
            if (Time.time > startPhaseTime + destructionPhaseTime || nbPlayerReady == nbMaxPlayer)
            {
                destructionPhase = false;
                photonView.RPC("ResetPhase", PhotonTargets.AllViaServer);
                if (PhotonNetwork.connected)
                {
                    photonView.RPC("UpgradePhase", PhotonTargets.AllViaServer);
                }
                else
                {
                    UpgradePhase();
                }
            }
        }
        else if (upgradePhase)
        {
            if (Time.time > startPhaseTime + upgradePhaseTime || nbPlayerReady == nbMaxPlayer)
            {
                upgradePhase = false;
                photonView.RPC("ResetPhase", PhotonTargets.AllViaServer);
                if (PhotonNetwork.connected)
                {
                    photonView.RPC("Turn", PhotonTargets.AllViaServer);
                }
                else
                {
                    Turn();
                }
            }
        }
    }
    [PunRPC]
    private void Turn()
    {
        Debug.Log("Turn Started");
        DrawPhase();
    }

    [PunRPC]
    private void DrawPhase()
    {
        startPhaseTime = Time.time;
        UpdatePhaseName("Draw Phase");
        drawPhase = true;
    }

    [PunRPC]
    private void StealPhase()
    {
        startPhaseTime = Time.time;
        UpdatePhaseName("Steal Phase");
        stealPhase = true;
    }

    [PunRPC]
    private void BuildPhase()
    {
        startPhaseTime = Time.time;
        UpdatePhaseName("Build Phase");
        buildPhase = true;
    }

    [PunRPC]
    private void DestructionPhase()
    {
        startPhaseTime = Time.time;
        UpdatePhaseName("Destruction Phase");
        destructionPhase = true;
    }

    [PunRPC]
    private void UpgradePhase()
    {
        startPhaseTime = Time.time;
        UpdatePhaseName("Upgrade Phase");
        upgradePhase = true;
    }

    private void UpdatePhaseName(string newText)
    {
        if(phaseName != null)
        {
            phaseName.text = newText;
        }
        else
        {
            Debug.Log("No UI.text linked to the partyManager");
        }
    }

    
    public void Ready()
    {
        if(iAmReady)
        {
            return;
        }
        iAmReady = true;
        //Debug.Log("New player ready");
        //augmente le nombre de joueur pret pour la prochaine phase (en reseau et en local)
        if(PhotonNetwork.connected)
        {
            photonView.RPC("NewPlayerRdy", PhotonTargets.AllViaServer);
        }
        else
        {
            NewPlayerRdy();
        }
        //modification visuel du bouton pret -> rouge = pas pret / vert = pret
        if(readyForNewPhase != null)
        {
            if(iAmReady)
            {
                readyForNewPhase.GetComponent<Image>().color = Color.green;
            }
        }
        else
        {
            Debug.Log("Party manage miss the button ready for new phase");
        }
    }

    [PunRPC]
    private void NewPlayerRdy()
    {
        nbPlayerReady++;
        //Debug.Log("nb player ready / max player : " + nbPlayerReady + " / " + nbMaxPlayer);
        UpdateReadyButtonText();
    }

    //reset la parametre au changement de phase
    [PunRPC]
    private void ResetPhase()
    {
        iAmReady = false;
        nbPlayerReady = 0;
        readyForNewPhase.GetComponent<Image>().color = Color.red;
        UpdateReadyButtonText();
    }

    private void UpdateReadyButtonText()
    {
        readyForNewPhase.GetComponentInChildren<Text>().text = "Ready ( " + nbPlayerReady + " / " + nbMaxPlayer + " )";
    }
}
