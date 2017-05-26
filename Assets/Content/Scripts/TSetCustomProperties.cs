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
    [Tooltip("Please refer to / edit names in TCustomProperties")]
    public string propertyKey_int;

    [Tooltip("The number used for loopable count limit/ count amount, e.g. number of animations to playback before loop back to start")]
    public int loopThreshold; //effective if count is > 1

    [Tooltip("Please refer to / edit names in TCustomProperties")]
    public string propertyKey_bool;
    public Text textInfo;
    
    public static int value = 0;
    public static int newValue = 0;

    public static bool isActive;
    public static bool newIsActive;

    public static int Output_int;
    public static bool Output_bool;


	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        textInfo.text = "Update TCustomProperties.icon01_int:   " + (int)PhotonNetwork.room.CustomProperties[propertyKey_int]
            + "\n new value: " + newValue
            + "\n (expected) value: " + value

            + "\n Update TCustomProperties.icon01_bool: " + (bool)PhotonNetwork.room.CustomProperties[propertyKey_bool]
            + "\n isActive: " + isActive
            + "\n newIsActive: " + newIsActive
            ;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("..................................Space pressed");
            IncrementVal_loopBackAtOne();
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

        if(propertyKey_int != null)
        {
            //int
            setValue.Add(propertyKey_int, value);
        }
        
        if(propertyKey_bool != null)
        {
            //bool
            setValue.Add(propertyKey_bool, isActive);
        }

        PhotonNetwork.room.SetCustomProperties(setValue);
        //Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> On Joined Room ... value: " + (int)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_int]);
        //Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> On Joined Room ... bool: " + (bool)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_bool]);
    }

    //*****Thomas: The following code is set at the derived classes, eg. TSCP_Engine.cs//
    //public override void OnPhotonCustomRoomPropertiesChanged(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    //{
    //    //Int: update expected value if detected the property change
    //    if (propertyKey_int != null && propertiesThatChanged.ContainsKey(propertyKey_int))
    //    {
    //        value = (int)propertiesThatChanged[propertyKey_int];
    //        Output_int = (int)propertiesThatChanged[propertyKey_int];
    //        //Debug.Log("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ OnPhotonCustomRoomPropertiesChanged ... value: " + value);
    //        //Debug.Log("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ OnPhotonCustomRoomPropertiesChanged ... (int)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_int]): " + (int)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_int]);

    //    }

    //    //Bool: update expected value if detected the property change
    //    if (propertyKey_bool != null && propertiesThatChanged.ContainsKey(propertyKey_bool))
    //    {
    //        isActive = (bool)propertiesThatChanged[propertyKey_bool];
    //        Output_bool = (bool)propertiesThatChanged[propertyKey_bool];
    //        //Debug.Log("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ OnPhotonCustomRoomPropertiesChanged ... isActive: " + isActive);
    //        //Debug.Log("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ OnPhotonCustomRoomPropertiesChanged ... (bool)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_bool]: " + (bool)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_bool]);
    //    }

    //    if (this.gameObject.GetComponent<IconBehaviour>() != null) //check this status is important since this method is included in this script, when applied to different object this component might not be attached.
    //    {
    //        this.gameObject.GetComponent<IconBehaviour>().ShowHideIcon(isActive);
    //    }

    //    if (this.gameObject.GetComponent<TAnimationPlay>() != null) //check this status is important...
    //    {
    //        this.gameObject.GetComponent<TAnimationPlay>().OnPlayAnimation(value);
    //    }
    //}


    #region Public Methods
    virtual public void IncrementVal_loopBackAtOne()
    {
        //SetValue
        if (loopThreshold > 1 && value == loopThreshold-1)
        {
            newValue = 1; //here is for animation, assuming state 0 is only for the beginning idle mode and not need to return to; otherwise, set this newValue = 0 here.
        }
        else
        {
            newValue = value + 1;
        }
        
        Hashtable setValue = new Hashtable();
        //Debug.Log("...................................N . newValue:" + newValue);
        setValue.Add(propertyKey_int, newValue);

        //Expected Value
        Hashtable expectedValue = new Hashtable();
        //Debug.Log("...................................N . value:" + value);
        expectedValue.Add(propertyKey_int, value);


        PhotonNetwork.room.SetCustomProperties(setValue, expectedValue, false);
        //Debug.Log("+++++++++++++++++++++++++++++++++++Result of IncrementalVal: " + PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_int]);

    }
    virtual public void IncrementVal_loopBackAtZero()
    {
        //SetValue
        if (loopThreshold > 1 && value == loopThreshold - 1)
        {
            newValue = 0; //here is for animation, assuming state 0 is only for the beginning idle mode and not need to return to; otherwise, set this newValue = 0 here.
        }
        else
        {
            newValue = value + 1;
        }

        Hashtable setValue = new Hashtable();
        //Debug.Log("...................................N . newValue:" + newValue);
        setValue.Add(propertyKey_int, newValue);

        //Expected Value
        Hashtable expectedValue = new Hashtable();
        //Debug.Log("...................................N . value:" + value);
        expectedValue.Add(propertyKey_int, value);


        PhotonNetwork.room.SetCustomProperties(setValue, expectedValue, false);
        //Debug.Log("+++++++++++++++++++++++++++++++++++Result of IncrementalVal: " + PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_int]);

    }

    public void IncrementBool()
    {
        //SetValue
        newIsActive = !isActive;
        Hashtable setValue = new Hashtable();
        //Debug.Log("...................................N . newIsActive:" + newIsActive);
        setValue.Add(propertyKey_bool, newIsActive);

        //Expected Value
        Hashtable expectedValue = new Hashtable();
        //Debug.Log("...................................N . isActive:" + isActive);
        expectedValue.Add(propertyKey_bool, isActive);


        PhotonNetwork.room.SetCustomProperties(setValue, expectedValue, false);

        //Debug.Log("+++++++++++++++++++++++++++++++++++Result of IncrementalBool: " + PhotonNetwork.room.CustomProperties[customePropertyName[1]]);
    }
    #endregion
}
