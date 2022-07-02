using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject fpsCamera;

    [SerializeField]
    TMP_Text playerNameText;

    void Start()
    {
        if (photonView.IsMine)
        {
            GetComponent<MovementController>().enabled = true;
            fpsCamera.GetComponent<Camera>().enabled = true;
        } 
        else
        {
            GetComponent<MovementController>().enabled = false;
            fpsCamera.GetComponent<Camera>().enabled = false;
        }

        SetPlayerUI();
    }

    void SetPlayerUI()
    {
        if (playerNameText != null)
        {
            playerNameText.text = photonView.Owner.NickName;
        }
    }
}
