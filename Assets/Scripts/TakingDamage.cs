using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Image healthBar;

    private float health;
    public float startHealth = 100;
 
    void Start()
    {
        health = startHealth;
        healthBar.fillAmount = health / startHealth;
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);

        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Die function");
        if (photonView.IsMine)
        {
            PixelGunGameManager.instance.LeaveRoom();
        }
    }
}
