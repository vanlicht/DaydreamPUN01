using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class PlayerNameInput : MonoBehaviour
{
    #region Priavate Variables
    static string playerNamePrefKey = "PlayerName";
    string defaultName;
    InputField inputField;
    #endregion

    #region MonoBehaviour Methods
    // Use this for initialization
    void Start ()
    {
        defaultName = " ";
        inputField = this.GetComponent<InputField>();
        if(inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                inputField.text = defaultName;
            }
        }
        PhotonNetwork.playerName = defaultName;
	}
    #endregion

    #region Public Methods
    public void SetPlayerName(string value)
    {
        PhotonNetwork.playerName = value + " ";
        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
    #endregion
}
