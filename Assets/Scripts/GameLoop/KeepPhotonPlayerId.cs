using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPhotonPlayerId : MonoBehaviour {

    private int playerPhotonId;
	
    public int PlayerPhotonId
    {
        get
        {
            return this.playerPhotonId;
        }
        set
        {
            this.playerPhotonId = value;
        }
    }
}
