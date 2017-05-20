using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class TSetCustomProperties : Photon.PunBehaviour
{
    public string[] customePropertyName;
    public Text textInfo;
    int value = 0;
    int newValue = 0;

    bool isActive;
    bool newIsActive;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        textInfo.text = "ic01_int value:" + (int)PhotonNetwork.room.CustomProperties[customePropertyName[0]] 
            + "\n new value: " + newValue 
            + "\n (expected) value: " + value
            
            + "\n ic01_bool value:" + (bool)PhotonNetwork.room.CustomProperties[customePropertyName[1]] 
            + "\n isActive: " + isActive
            + "\n newIsActive: " + newIsActive
            ;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("..................................Space pressed");
            IncrementVal();
            IncrementBool();
        }
	}

    #region Public Methods
    public void IncrementVal()
    {
        //SetValue
        newValue = value + 1;
        Hashtable setValue = new Hashtable();
        Debug.Log("...................................N . newValue:" + newValue);
        setValue.Add(customePropertyName[0], newValue);

        //Expected Value
        Hashtable expectedValue = new Hashtable();
        Debug.Log("...................................N . value:" + value);
        expectedValue.Add(customePropertyName[0], value);


        PhotonNetwork.room.SetCustomProperties(setValue, expectedValue, false);
    }

    public void IncrementBool()
    {
        //SetValue
        newIsActive = !isActive;
        Hashtable setValue = new Hashtable();
        Debug.Log("...................................N . newIsActive:" + newIsActive);
        setValue.Add(customePropertyName[1], newIsActive);

        //Expected Value
        Hashtable expectedValue = new Hashtable();
        Debug.Log("...................................N . isActive:" + isActive);
        expectedValue.Add(customePropertyName[1], isActive);


        PhotonNetwork.room.SetCustomProperties(setValue, expectedValue, false);
    }
    #endregion


    //Create HashTable and SetCustomProperty once join the room and isMasterClient
    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            return;
        }
        Hashtable setValue = new Hashtable();

        //int
        setValue.Add(customePropertyName[0], value);

        //bool
        setValue.Add(customePropertyName[1], isActive);

        PhotonNetwork.room.SetCustomProperties(setValue);
    }

    public override void OnPhotonCustomRoomPropertiesChanged(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        //Int: update expected value if detected the property change
        if (propertiesThatChanged.ContainsKey(customePropertyName[0]))
        {
            value = (int)propertiesThatChanged[customePropertyName[0]];
            Debug.Log("...................OnPhotonCustomRoomPropertiesChanged ... value: " + value);
        }

        //Bool: update expected value if detected the property change
        if (propertiesThatChanged.ContainsKey(customePropertyName[1]))
        {
            isActive = (bool)propertiesThatChanged[customePropertyName[1]];
            Debug.Log("...................OnPhotonCustomRoomPropertiesChanged ... isActive: " + isActive);
        }
    }
}
