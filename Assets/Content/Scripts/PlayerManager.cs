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
            PlayerManager.localPlayerInstance = this.gameObject;
        }
        //DoNotDestroy seems to make daydream controller not responding when loading to new scene or reloading scene
        //DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
