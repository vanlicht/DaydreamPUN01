using UnityEngine;
using System.Collections;
using System;

public class IconBehaviour : Photon.MonoBehaviour, IPunObservable
{

    public GameObject targetIcon;
    private Vector3 startPosition;

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
    }

    public void ShowHideIcon()
    {
        if (!isVisible)
        {
            targetIcon.SetActive(true);
            isVisible = true;
        }
        else
        {
            targetIcon.GetComponentInChildren<IconMovement>().disableSelf();
            isVisible = false;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (photonView.isMine)
        {
            stream.SendNext(isVisible);
        }
        else
        {
            this.isVisible = (bool)stream.ReceiveNext();
        }
    }
}
