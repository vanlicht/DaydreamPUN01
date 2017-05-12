using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTextColor : Photon.MonoBehaviour, IPunObservable
{

    Material mat;
    bool isSwitch;

	// Use this for initialization
	void Start ()
    {
        mat = this.GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.photonView.RPC("PunOnGlow", PhotonTargets.All);
    }
    public void TOnSwitch()
    {
        if (photonView.isMine)
        {
            isSwitch = !isSwitch;
        }
    }

    public void OnGlow()
    {

        Debug.Log(".......................TOnGlow");
        if (isSwitch)
        {
            mat.SetColor("_EmissionColor", Color.red);
        }
        else
        {
            mat.SetColor("_EmissionColor", Color.black);
        }
    }
    [PunRPC]
    public void PunOnGlow()
    {

        Debug.Log(".......................TOnGlow");
        if (isSwitch)
        {
            mat.SetColor("_EmissionColor", Color.red);
        }
        else
        {
            mat.SetColor("_EmissionColor", Color.black);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(isSwitch);
        }
        else
        {
            isSwitch = (bool)stream.ReceiveNext();
        }
    }
}
