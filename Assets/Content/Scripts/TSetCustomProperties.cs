/*Important Note from Thomas:
 * Which value at what stage to use can be confusing, so it's important to know what's going on. Explaination below:
 * PhotonNetwork.room.SetCustomProperties(setValue, expectedValue, false)... Check And Swap for Properties (CAS)
    - When using CAS, such as methods in this script, IncrementVal() and IncrementBool(). The value here when recorded, 
    is one step behind OnPhotonCustomRoomPropertiesChanged() and Update(). So what can happen is if using the value from CAS on master client,
    when passing to other clients, other clients will be a step ahead.
    - Solution would be put the methods or use the value from OnPhotonCustomRoomPropertiesChanged.
*/
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
        textInfo.text = "Update TCustomProperties.icon01_int:   " + (int)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_int]
            + "\n new value: " + newValue 
            + "\n (expected) value: " + value
            
            + "\n Update TCustomProperties.icon01_bool: " + (bool)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_bool] 
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

    //Create HashTable and SetCustomProperty once join the room and isMasterClient
    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            return;
        }
        Hashtable setValue = new Hashtable();

        //int
        setValue.Add(TCustomProperties.icon01_int, value);

        //bool
        setValue.Add(TCustomProperties.icon01_bool, isActive);

        PhotonNetwork.room.SetCustomProperties(setValue);
        Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> On Joined Room ... value: " + (int)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_int]);
        Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> On Joined Room ... bool: " + (bool)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_bool]);
    }

    public override void OnPhotonCustomRoomPropertiesChanged(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        //Int: update expected value if detected the property change
        if (propertiesThatChanged.ContainsKey(TCustomProperties.icon01_int))
        {
            value = (int)propertiesThatChanged[TCustomProperties.icon01_int];
            Debug.Log("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ OnPhotonCustomRoomPropertiesChanged ... value: " + value);
            Debug.Log("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ OnPhotonCustomRoomPropertiesChanged ... (int)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_int]): " + (int)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_int]);
            
        }

        //Bool: update expected value if detected the property change
        if (propertiesThatChanged.ContainsKey(TCustomProperties.icon01_bool))
        {
            isActive = (bool)propertiesThatChanged[TCustomProperties.icon01_bool];
            Debug.Log("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ OnPhotonCustomRoomPropertiesChanged ... isActive: " + isActive);
            Debug.Log("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ OnPhotonCustomRoomPropertiesChanged ... (bool)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_bool]: " + (bool)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_bool]);
        }

        //Thomas Test. It works!
        photonView.RPC("RPCShowHideIcon", PhotonTargets.All);
    }

    #region Public Methods
    public void IncrementVal()
    {
        //SetValue
        newValue = value+1;
        Hashtable setValue = new Hashtable();
        Debug.Log("...................................N . newValue:" + newValue);
        setValue.Add(TCustomProperties.icon01_int, newValue);

        //Expected Value
        Hashtable expectedValue = new Hashtable();
        Debug.Log("...................................N . value:" + value);
        expectedValue.Add(TCustomProperties.icon01_int, value);


        PhotonNetwork.room.SetCustomProperties(setValue, expectedValue, false);
        Debug.Log("+++++++++++++++++++++++++++++++++++Result of IncrementalVal: " + PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_int]);

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
        expectedValue.Add(TCustomProperties.icon01_bool, isActive);


        PhotonNetwork.room.SetCustomProperties(setValue, expectedValue, false);

        Debug.Log("+++++++++++++++++++++++++++++++++++Result of IncrementalBool: " + PhotonNetwork.room.CustomProperties[customePropertyName[1]]);
    }
    #endregion
}
