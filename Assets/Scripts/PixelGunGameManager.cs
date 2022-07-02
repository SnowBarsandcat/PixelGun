using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class PixelGunGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject playerPrefab;

    public static PixelGunGameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (playerPrefab != null)
            {
                int randomPoint = Random.Range(-20, 20);
                PhotonNetwork.Instantiate(
                    playerPrefab.name,
                    new Vector3(randomPoint, 0, randomPoint),
                    Quaternion.identity
                );
            }
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName
            + " joined to "
            + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName
            + " joined to "
            + PhotonNetwork.CurrentRoom.Name
            + " "
            + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Game Launcher Scene");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
