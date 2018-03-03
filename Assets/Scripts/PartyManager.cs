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
		if(drawPhase)
        {
            if (Time.time > startPhaseTime + drawPhaseTime || nbPlayerReady == nbMaxPlayer)
            {
                nbPlayerReady = 0;
                drawPhase = false;
                StealPhase();
            }
        }
        else if (stealPhase)
        {
            if (Time.time > startPhaseTime + stealPhaseTime || nbPlayerReady == nbMaxPlayer)
            {
                nbPlayerReady = 0;
                stealPhase = false;
                BuildPhase();
            }
        }
        else if (buildPhase)
        {
            if (Time.time > startPhaseTime + buildPhaseTime || nbPlayerReady == nbMaxPlayer)
            {
                nbPlayerReady = 0;
                buildPhase = false;
                DestructionPhase();
            }
        }
        else if (destructionPhase)
        {
            if (Time.time > startPhaseTime + destructionPhaseTime || nbPlayerReady == nbMaxPlayer)
            {
                nbPlayerReady = 0;
                destructionPhase = false;
                UpgradePhase();
            }
        }
        else if (upgradePhase)
        {
            if (Time.time > startPhaseTime + upgradePhaseTime || nbPlayerReady == nbMaxPlayer)
            {
                nbPlayerReady = 0;
                upgradePhase = false;
                Turn();
            }
        }
    }

    private void Turn()
    {
        Debug.Log("Turn Started");
        DrawPhase();
    }

    //[PunRPC]
    private void DrawPhase()
    {
        startPhaseTime = Time.time;
        UpdatePhaseName("Draw Phase");
        drawPhase = true;
    }

    //[PunRPC]
    private void StealPhase()
    {
        startPhaseTime = Time.time;
        UpdatePhaseName("Steal Phase");
        stealPhase = true;
    }

    //[PunRPC]
    private void BuildPhase()
    {
        startPhaseTime = Time.time;
        UpdatePhaseName("Build Phase");
        buildPhase = true;
    }

    //[PunRPC]
    private void DestructionPhase()
    {
        startPhaseTime = Time.time;
        UpdatePhaseName("Destruction Phase");
        destructionPhase = true;
    }

    //[PunRPC]
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
        photonView.RPC("NewPlayerRdy", PhotonTargets.AllViaServer);
    }

    [PunRPC]
    private void NewPlayerRdy()
    {
        nbPlayerReady++;
    }

}
