using UnityEngine;
using System.Collections;
using System;

public class IconBehaviour : Photon.MonoBehaviour, IPunObservable
{

    public GameObject targetIcon;
    private Vector3 startPosition;
    bool isTriggered;

    bool isIconVisible;
	// Use this for initialization
	void Start ()
    {
        

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isTriggered = true;
        }
        if (isTriggered)
        {
            Debug.Log("........................ShowHideIcon()");
            ShowHideIcon();
        }
    }

    public void OnIconTriggered()
    {
        isTriggered = true;
        Debug.Log("........................OnIconTriggered: True");
    }

    public void ShowHideIcon()
    {
        if (targetIcon.activeSelf)
        {
            isIconVisible = false;
            targetIcon.GetComponentInChildren<IconMovement>().disableSelf();
        }
        else
        {
            isIconVisible = true;
            targetIcon.SetActive(isIconVisible);
        }
        isTriggered = false;
        Debug.Log("................End of ShowHideIcon().....isTriggered = False");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (photonView.isMine)
        {
            stream.SendNext(isTriggered);
        }
        else
        {
            this.isTriggered = (bool)stream.ReceiveNext();
        }
    }
}
