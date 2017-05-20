using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class IconBehaviour : MonoBehaviour
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
            //ShowHideIcon();
        }
        isVisible = (bool)PhotonNetwork.room.CustomProperties["ic01_bool"];
        textLog.text = "isVisible: " + isVisible;

        targetIcon.SetActive(isVisible);
    }

    //public void ShowHideIcon()
    //{
    //    if (isVisible)
    //    {
    //        //Turn on the target icon
    //        targetIcon.SetActive(isVisible);
    //        Debug.Log("...............IconBehaviour 01");
    //    }
    //    else
    //    {
    //        //Turn off the target icon
    //        targetIcon.GetComponentInChildren<IconMovement>().disableSelf();
    //        Debug.Log("...............IconBehaviour 02");
    //    }
    //}
}
