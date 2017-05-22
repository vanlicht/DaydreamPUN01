using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class IconBehaviour : Photon.MonoBehaviour
{

    //public static IconBehaviour Instance;
    public GameObject targetIcon;
    private Vector3 startPosition;
    public Text textLog;

    bool isVisible;

    // Use this for initialization
    void Start ()
    {
        //Instance = this;
    }
	
	// Update is called once per frame
	void Update ()
    {
        isVisible = (bool)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_bool];
        textLog.text = "isVisible: " + isVisible;
    }

    [PunRPC]
    public void RPCShowHideIcon()
    {
        isVisible = (bool)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_bool];
        Debug.Log("...............bool value inside ShowHideIcon method: " + isVisible);
        if (isVisible)
        {
            //Turn on the target icon
            targetIcon.SetActive(isVisible);
            Debug.Log("...............IconBehaviour 01");
        }
        else
        {
            //Turn off the target icon
            targetIcon.GetComponentInChildren<IconMovement>().disableSelf();
            Debug.Log("...............IconBehaviour 02");
        }
    }
}
