using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class IconBehaviour : Photon.MonoBehaviour, IPunObservable
{

    public GameObject targetIcon;
    private Vector3 startPosition;
    public Text textLog;

    bool isVisible;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowHideIcon();
        }

        //this.photonView.RPC("RPCShowHideIcon", PhotonTargets.AllBufferedViaServer);

        textLog.text = "isVisible: " + isVisible;
    }

    public void ShowHideIcon()
    {
        if (photonView.isMine)
        {
            isVisible = !isVisible;
        }
        
    }

    [PunRPC]
    public void RPCShowHideIcon()
    {
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(isVisible);
            this.photonView.RPC("RPCShowHideIcon", PhotonTargets.All);
            Debug.Log("...............IconBehaviour 03");
        }
        else
        {
            this.isVisible = (bool)stream.ReceiveNext();
        }
    }
}
