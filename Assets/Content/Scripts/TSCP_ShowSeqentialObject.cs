using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class TSCP_ShowSeqentialObject : TSetCustomProperties
{
    public GameObject[] targetObjects;

    void Awake()
    {
        foreach (GameObject targetObj in targetObjects)
        {
            targetObj.SetActive(false);
        }
        loopThreshold = targetObjects.Length;
    }

    public override void IncrementVal_loopBackAtZero()
    {
        //SetValue
        if (loopThreshold > 1 && value == loopThreshold) // change this so the value can reach up to the total object count, using the total number as timing to hide all objects, so number 0 can be used to show the [0] object.
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


    public override void OnPhotonCustomRoomPropertiesChanged(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        //Int: update expected value if detected the property change
        if (propertyKey_int != null && propertiesThatChanged.ContainsKey(propertyKey_int))
        {
            value = (int)propertiesThatChanged[propertyKey_int];
            Output_int = (int)propertiesThatChanged[propertyKey_int];
            //Debug.Log("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ OnPhotonCustomRoomPropertiesChanged ... value: " + value);
            //Debug.Log("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ OnPhotonCustomRoomPropertiesChanged ... (int)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_int]): " + (int)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_int]);

        }

        this.OnClick(value);
    }

    public override void TTextInformation()
    {
        textInfo.text = "Update TCustomProperties.icon01_int: " + (int)PhotonNetwork.room.CustomProperties[propertyKey_int]
            + "\n new value: " + newValue
            + "\n (expected) value: " + value
            ;
    }

    public void OnClick(int value)
    {
        if (value < targetObjects.Length)
        {
            if(value < 0)
            {
                foreach (GameObject targetObj in targetObjects)
                {
                    targetObj.SetActive(false);
                }
            }
            if (value > 0)
            {
                targetObjects[value].SetActive(true);
                targetObjects[value - 1].SetActive(false);
            }
            if (value == 0)
            {
                targetObjects[value].SetActive(true);
            }
        }

        if (value == targetObjects.Length)
        {
            foreach (GameObject targetObj in targetObjects)
            {
                targetObj.SetActive(false);
            }
        }
    }
}