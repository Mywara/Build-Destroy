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
    public float waittingToStartTime = 5f;
    public Text phaseName;
    public Button readyForNewPhase;
    public Button readyForNewPhaseUpgrade;
    public Text timer;
    public ArenaManager arenaManager;
    public Canvas basicUI;
    public Canvas upgradeUI;
    public Text phaseNameUpgrade;
    public Text timerUpgrade;
    public Text money;
    public Text moneyUpgrade;
    public Upgrades upgcost;
    public Button drawButton;

    private GameObject playerZone;
    private CameraController camController;
    public int PlayerID = 1;
    private int nbMaxPlayer;
    private int nbPlayerReady;
    private float startPhaseTime;
    private bool drawPhase = false;
    private bool stealPhase = false;
    private bool buildPhase = false;
    private bool destructionPhase = false;
    private bool upgradePhase = false;
    private bool iAmReady = false;
    private bool gameStarted = false;
    private bool waitingToStart = false;

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
        readyForNewPhaseUpgrade.GetComponent<Image>().color = Color.red;
        UpdateReadyButtonText();
        UpdateReadyButtonTextUpgrade();
        upgradeUI.enabled = false;
        UpdateMoney();
        UpdatePhaseName("Waiting for other players...");
        timer.text = "";
    }

    // Use this for initialization
    void Start () {
        if(arenaManager != null)
        {
            playerZone = arenaManager.GetPlayerZone(PlayerID);
            camController = Camera.main.GetComponent<CameraController>();
            Transform focusPoint = playerZone.transform.GetChild(1);
            camController.SetTarget(focusPoint);
                         
            // Test récupération de la zone de jeu du joueur
            Vector3 offset = new Vector3(0f, 7f, 0f);
            PhotonNetwork.Instantiate("TestCube", playerZone.transform.position + offset, Quaternion.identity, 0);
        }

        //si on est pas le masterclient, on ne fait rien
        if (!PhotonNetwork.isMasterClient && PhotonNetwork.connected)
        {
            return;
        }
        //Turn();
    }
	
	// Update is called once per frame
	void Update () {
        if(!PhotonNetwork.isMasterClient && PhotonNetwork.connected)
        {
            return;
        }
        if(!gameStarted)
        {  
            if (PhotonNetwork.connected)
            {
                if (PhotonNetwork.room.PlayerCount == nbMaxPlayer)
                {
                    photonView.RPC("StartingGame", PhotonTargets.AllViaServer);
                }
            }
            else
            {
                StartingGame();
            }
        }

        if(waitingToStart)
        {
            if (PhotonNetwork.connected)
            {
                photonView.RPC("UpdateTimer", PhotonTargets.AllViaServer, startPhaseTime + waittingToStartTime - Time.time);
            }
            else
            {
                UpdateTimer(startPhaseTime + waittingToStartTime - Time.time);
            }

            if (Time.time > startPhaseTime + waittingToStartTime || nbPlayerReady == nbMaxPlayer)
            {
                waitingToStart = false;
                if (PhotonNetwork.connected)
                {
                    photonView.RPC("ResetPhase", PhotonTargets.AllViaServer);
                    photonView.RPC("Turn", PhotonTargets.AllViaServer);
                }
                else
                {
                    ResetPhase();
                    Turn();
                }
            }
        }
		if(drawPhase)
        {
            if(PhotonNetwork.connected)
            {
                photonView.RPC("UpdateTimer", PhotonTargets.AllViaServer, startPhaseTime + drawPhaseTime - Time.time);
            }
            else
            {
                UpdateTimer(startPhaseTime + drawPhaseTime - Time.time);
            }
            
            //si on est dans la phase de pioche et que le timer à fini, ou que tous les joueurs sont rdy
            //on change de phase
            if (Time.time > startPhaseTime + drawPhaseTime || nbPlayerReady == nbMaxPlayer)
            {
                drawPhase = false;
                if (PhotonNetwork.connected)
                {
                    photonView.RPC("ResetPhase", PhotonTargets.AllViaServer);
                    photonView.RPC("StealPhase", PhotonTargets.AllViaServer);
                }
                else
                {
                    ResetPhase();
                    StealPhase();
                } 
            }
        }
        else if (stealPhase)
        {
            if (PhotonNetwork.connected)
            {
                photonView.RPC("UpdateTimer", PhotonTargets.AllViaServer, startPhaseTime + stealPhaseTime - Time.time);
            }
            else
            {
                UpdateTimer(startPhaseTime + stealPhaseTime - Time.time);
            }
            
            if (Time.time > startPhaseTime + stealPhaseTime || nbPlayerReady == nbMaxPlayer)
            {
                stealPhase = false;
                if (PhotonNetwork.connected)
                {
                    photonView.RPC("ResetPhase", PhotonTargets.AllViaServer);
                    photonView.RPC("BuildPhase", PhotonTargets.AllViaServer);
                }
                else
                {
                    ResetPhase();
                    BuildPhase();
                }
            }
        }
        else if (buildPhase)
        {
            if (PhotonNetwork.connected)
            {
                photonView.RPC("UpdateTimer", PhotonTargets.AllViaServer, startPhaseTime + buildPhaseTime - Time.time);
            }
            else
            {
                UpdateTimer(startPhaseTime + buildPhaseTime - Time.time);
            }
            
            if (Time.time > startPhaseTime + buildPhaseTime || nbPlayerReady == nbMaxPlayer)
            {
                buildPhase = false;
                if (PhotonNetwork.connected)
                {
                    photonView.RPC("ResetPhase", PhotonTargets.AllViaServer);
                    photonView.RPC("DestructionPhase", PhotonTargets.AllViaServer);
                }
                else
                {
                    ResetPhase();
                    DestructionPhase();
                }
            }
        }
        else if (destructionPhase)
        {
            if (PhotonNetwork.connected)
            {
                photonView.RPC("UpdateTimer", PhotonTargets.AllViaServer, startPhaseTime + destructionPhaseTime - Time.time);
            }
            else
            {
                UpdateTimer(startPhaseTime + destructionPhaseTime - Time.time);
            }
            
            if (Time.time > startPhaseTime + destructionPhaseTime || nbPlayerReady == nbMaxPlayer)
            {
                destructionPhase = false;
                if (PhotonNetwork.connected)
                {
                    photonView.RPC("ResetPhase", PhotonTargets.AllViaServer);
                    photonView.RPC("UpgradePhase", PhotonTargets.AllViaServer);
                }
                else
                {
                    ResetPhase();
                    UpgradePhase();
                }
            }
        }
        else if (upgradePhase)
        {
            if (PhotonNetwork.connected)
            {
                photonView.RPC("UpdateTimerUpgrade", PhotonTargets.AllViaServer, startPhaseTime + upgradePhaseTime - Time.time);
            }
            else
            {
                UpdateTimerUpgrade(startPhaseTime + upgradePhaseTime - Time.time);
            }
            
            if (Time.time > startPhaseTime + upgradePhaseTime || nbPlayerReady == nbMaxPlayer)
            {
                upgradePhase = false;
                if (PhotonNetwork.connected)
                {
                    photonView.RPC("ResetPhase", PhotonTargets.AllViaServer);
                    photonView.RPC("Turn", PhotonTargets.AllViaServer);
                }
                else
                {
                    ResetPhase();
                    Turn();
                }
            }
        }
    }

    [PunRPC]
    private void StartingGame()
    {
        gameStarted = true;
        waitingToStart = true;
        startPhaseTime = Time.time;
        UpdatePhaseName("Starting in...");
        drawButton.gameObject.SetActive(false);

    }

    [PunRPC]
    private void Turn()
    {
        Debug.Log("Turn Started");
        if(PhotonNetwork.connected)
        {
            photonView.RPC("ChangeToBasicUI", PhotonTargets.AllViaServer);
            if(PhotonNetwork.isMasterClient)
            {
                photonView.RPC("AddMoney", PhotonTargets.AllViaServer, MoneySystem.instance.actualIncome);
                upgcost.showIncome();
            }
        }
        else
        {
            ChangeToBasicUI();
            AddMoney(MoneySystem.instance.actualIncome);
            upgcost.showIncome();
            drawButton.gameObject.SetActive(true);
        }

        DrawPhase();
    }

    [PunRPC]
    private void DrawPhase()
    {
        
        startPhaseTime = Time.time;
        UpdatePhaseName("Draw Phase");
        drawPhase = true;
        CardManager.instance.DrawHand(CardManager.instance.handSize);
        drawButton.enabled = true;
    }

    [PunRPC]
    private void StealPhase()
    {
        upgcost.eraseIncome();
        startPhaseTime = Time.time;
        UpdatePhaseName("Steal Phase");
        stealPhase = true;
        drawButton.gameObject.SetActive(false);
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
        if (PhotonNetwork.connected)
        {
            photonView.RPC("ChangeToUpgradeUI", PhotonTargets.AllViaServer);
        }
        else
        {
            ChangeToUpgradeUI();
        }
        startPhaseTime = Time.time;
        UpdatePhaseNameUpgrade("Upgrade Phase");
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

    private void UpdatePhaseNameUpgrade(string newText)
    {
        if (phaseName != null)
        {
            phaseNameUpgrade.text = newText;
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
        if(readyForNewPhase != null && readyForNewPhaseUpgrade != null)
        {
            if(iAmReady)
            {
                readyForNewPhase.GetComponent<Image>().color = Color.green;
                readyForNewPhaseUpgrade.GetComponent<Image>().color = Color.green;
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
        UpdateReadyButtonTextUpgrade();
    }

    //reset la parametre au changement de phase
    [PunRPC]
    private void ResetPhase()
    {
        iAmReady = false;
        nbPlayerReady = 0;
        readyForNewPhase.GetComponent<Image>().color = Color.red;
        readyForNewPhaseUpgrade.GetComponent<Image>().color = Color.red;
        UpdateReadyButtonText();
        UpdateReadyButtonTextUpgrade();
    }

    private void UpdateReadyButtonText()
    {
        if(readyForNewPhase != null)
        {
            readyForNewPhase.GetComponentInChildren<Text>().text = "Ready ( " + nbPlayerReady + " / " + nbMaxPlayer + " )";
        }
        else
        {
            Debug.Log("No ready for next phase button");
        }
    }

    private void UpdateReadyButtonTextUpgrade()
    {
        if (readyForNewPhaseUpgrade != null)
        {
            readyForNewPhaseUpgrade.GetComponentInChildren<Text>().text = "Ready ( " + nbPlayerReady + " / " + nbMaxPlayer + " )";
        }
        else
        {
            Debug.Log("No ready for next phase button");
        }
    }

    [PunRPC]
    private void UpdateTimer(float countDown)
    {
        if (readyForNewPhase != null)
        {
            if(countDown>=0)
            {
                timer.GetComponentInChildren<Text>().text = string.Format("{0:0}:{1:00}", Mathf.Floor(countDown / 60), countDown % 60);
            }
        }
        else
        {
            Debug.Log("No ready for next phase button");
        }
        
    }

    [PunRPC]
    private void UpdateTimerUpgrade(float countDown)
    {
        if (readyForNewPhaseUpgrade != null)
        {
            if (countDown >= 0)
            {
                timerUpgrade.GetComponentInChildren<Text>().text = string.Format("{0:0}:{1:00}", Mathf.Floor(countDown / 60), countDown % 60);
            }
        }
        else
        {
            Debug.Log("No ready for next phase button");
        }

    }

    //on désactive l'UI de base, pour mettre l'UI d'upgrade
    [PunRPC]
    private void ChangeToUpgradeUI()
    {
        if(basicUI != null && upgradeUI != null)
        {
            basicUI.enabled = false;
            upgradeUI.enabled = true;
            UpdateMoneyUpgrade();
            upgcost.updateCostText();
        }
        else
        {
            Debug.Log("Missing basicUI or UpgradeUI on partyManager");
        }

    }

    //on désactive l'UI d'upgrade, pour mettre l'UI de base
    [PunRPC]
    private void ChangeToBasicUI()
    {
        if (basicUI != null && upgradeUI != null)
        {
            basicUI.enabled = true;
            upgradeUI.enabled = false;
        }
        else
        {
            Debug.Log("Missing basicUI or UpgradeUI on partyManager");
        }
    }

    //Met à jour le UI Text avec l'argent du joueur
    public void UpdateMoney()
    { 
        money.text = "Money : " + MoneySystem.GetMoney() + " $";
    }

    //Met à jour le UI Text du menu d'upgrade avec l'argent du joueur
    private void UpdateMoneyUpgrade()
    {
        moneyUpgrade.text = "Budget : " + MoneySystem.GetMoney() + " $";
    }

    //A utliser pour ajouter de l'argent, ca met automatique à jour le UI text
    [PunRPC]
    private void AddMoney(int amount)
    {
        //Debug.Log("Amount of money to add : " + amount);
        MoneySystem.instance.AddMoney(amount);
        UpdateMoney();
    }

    //A utliser pour depenser de l'argent, ca met automatique à jour le UI text
    private void BuyItem(int cost)
    {
        MoneySystem.instance.BuyItem(cost);
        UpdateMoney();
    }
}
