using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.PunBehaviour
{
    public GameObject playerPrefab;
    //private GameObject currentPlayer;

    #region MonoBehaviour Callbacks
    // Use this for initialization
    void Start ()
    {
        if (PlayerManager.localPlayerInstance == null)
        {
            GameObject currentPlayer = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 1.6f, 0f), Quaternion.identity, 0);
            currentPlayer.GetComponent<PlayerController>().isControllable = true;
            Debug.Log(".............................clone created...");
        }
        //currentPlayer = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 1.6f, 0f), Quaternion.identity, 0);
        //currentPlayer.GetComponent<PlayerController>().isControllable = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LeaveRoom();
        }
    }
    #endregion

    #region Public Methods
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void LoadWorld()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }
    #endregion

    #region PUN Callbacks

    public override void OnLeftRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        if (PhotonNetwork.isMasterClient)
        {
            //load new scene
            if (PhotonNetwork.room.PlayerCount > 1)
            {
                LoadWorld();
            }
        }
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        
    }
    #endregion

    #region GUI
    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        GUILayout.Label(PhotonNetwork.room.PlayerCount.ToString());
    }
    #endregion
}
