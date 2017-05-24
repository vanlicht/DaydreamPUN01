using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

public class TAnimationPlay : Photon.PunBehaviour
{
    Animator animator;
    public string[] animationStateNames;
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    OnPlayAnimation(i);
        //}
    }
    //public override void OnPhotonCustomRoomPropertiesChanged(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    //{
    //    base.OnPhotonCustomRoomPropertiesChanged(propertiesThatChanged);
    //}
    public void OnPlayAnimation(int i)
    {
        animator = this.gameObject.GetComponent<Animator>();
        animator.Play(animationStateNames[i], 0);
    }
}
