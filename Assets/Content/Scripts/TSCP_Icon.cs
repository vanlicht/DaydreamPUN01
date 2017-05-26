using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSCP_Icon : TSetCustomProperties
{
    public override void OnPhotonCustomRoomPropertiesChanged(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        //Bool: update expected value if detected the property change
        if (propertyKey_bool != null && propertiesThatChanged.ContainsKey(propertyKey_bool))
        {
            isActive = (bool)propertiesThatChanged[propertyKey_bool];
            Output_bool = (bool)propertiesThatChanged[propertyKey_bool];
            //Debug.Log("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ OnPhotonCustomRoomPropertiesChanged ... isActive: " + isActive);
            //Debug.Log("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ OnPhotonCustomRoomPropertiesChanged ... (bool)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_bool]: " + (bool)PhotonNetwork.room.CustomProperties[TCustomProperties.icon01_bool]);
        }

        if (this.gameObject.GetComponent<IconBehaviour>() != null) //check this status is important since this method is included in this script, when applied to different object this component might not be attached.
        {
            this.gameObject.GetComponent<IconBehaviour>().ShowHideIcon(isActive);
        }
    }
}
