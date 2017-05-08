using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Photon.MonoBehaviour
{
    static public GameObject localPlayerInstance;

    // Use this for initialization
    void Awake ()
    {
        if (photonView.isMine)
        {
            localPlayerInstance = this.gameObject;
        }
        //DontDestroyOnLoad(this.gameObject);
        //DoNotDestroy seems to make daydream controller not responding when loading to new scene or reloading scene
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
