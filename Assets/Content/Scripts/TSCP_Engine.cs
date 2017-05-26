using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSCP_Engine : TSetCustomProperties
{
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

        if (this.gameObject.GetComponent<TAnimationPlay>() != null) //check this status is important...
        {
            this.gameObject.GetComponent<TAnimationPlay>().OnPlayAnimation(value);
        }
    }
}
