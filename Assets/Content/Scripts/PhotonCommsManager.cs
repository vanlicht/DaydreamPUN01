/*Note: 
1.) 
it seems that if there's already a player with controller in the scene when start,
then when the new player (self) is instantiated, even if you hide the original one, or even delete it, the controller still won't work.
The solution might be have to reload the scene or load new scene.

2.) It seems that if I use donotdestroy on load on the instantiated player, when switch scenes, the camera is working, but the controller is not working. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class PhotonCommsManager : Photon.PunBehaviour
{
    #region Public Variables
    public GameObject lobbyPlayer;
    public GameObject networkPlayer;
    public PhotonLogLevel logLevel = PhotonLogLevel.Informational;
    public byte maxPlayerPerRoom = 4;
    public GameObject controlPanel;
    public GameObject progressLabel;
    #endregion

    #region Private Behaviour
    private GameObject currentPlayer;
    bool isConnecting = false;
    string _gameVersion = "1.0";
    #endregion

    #region MonoBehaviours
    private void Awake()
    {
        isConnecting = false;
        PhotonNetwork.logLevel = logLevel;
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;
    }

    void Start ()
    {
        controlPanel.SetActive(true);
        progressLabel.SetActive(false);
        TConnect();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TConnect();
        }
    }
    #endregion

    #region Public Methods
    public void TConnect()
    {
        isConnecting = true;
        controlPanel.SetActive(false);
        progressLabel.SetActive(true);
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings(_gameVersion);
        }
    }

    public void LoadWorld()
    {
        PhotonNetwork.LoadLevel("space01");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    #endregion

    #region PUN Methods
    public override void OnConnectedToMaster()
    {
        if(isConnecting == true)
        {
            Debug.Log("Trying to enter random room.");
            PhotonNetwork.JoinRandomRoom();
        }
    }
    public override void OnDisconnectedFromPhoton()
    {
        controlPanel.SetActive(true);
        progressLabel.SetActive(false);
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("Can't join random room, create one!");
        //PhotonNetwork.CreateRoom(null);
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxPlayerPerRoom }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Thomas...PhotonCommManager: OnJoinedRoom");
        
        if (PhotonNetwork.isMasterClient)
        {
            LoadWorld();
        }
        //DestroyImmediate(lobbyPlayer);
        else
        {
            //currentPlayer = PhotonNetwork.Instantiate(networkPlayer.name, new Vector3(0f, 1.6f, 0f), Quaternion.identity, 0);
            //currentPlayer.GetComponent<PlayerController>().isControllable = true;
        }
        

    }

    public override void OnLeftRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    #endregion

    #region GUI
    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
    #endregion
}
