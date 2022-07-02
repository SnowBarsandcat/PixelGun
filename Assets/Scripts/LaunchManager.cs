using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    public GameObject EnterGamePanel;
    public GameObject ConnectionStatusPanel;
    public GameObject LobbyPanel;

    #region Unity Methods
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        EnterGamePanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        //PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {
        
    }
    #endregion

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName +  " is connected to photon server");
        LobbyPanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
    }

    public override void OnConnected()
    {
        Debug.Log("Connected to Internet");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("Game Scene");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to " + 
                  PhotonNetwork.CurrentRoom.Name + " " + 
                  PhotonNetwork.CurrentRoom.PlayerCount);
    }
    #endregion

    #region Private Methods
    void CreateAndJoinRoom()
    {
        string randomRoomName = "Room " + Random.Range(0, 10000);

        RoomOptions roomOptions = new RoomOptions
        {
            IsOpen = true,
            IsVisible = true,
            MaxPlayers = 20
        };

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }
    #endregion

    #region Public Methods
    public void ConnectToPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            ConnectionStatusPanel.SetActive(true);
            EnterGamePanel.SetActive(false);
        }
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    #endregion
}
