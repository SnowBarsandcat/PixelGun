using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shoting : MonoBehaviour
{
    [SerializeField]
    Camera fpsCamera;

    public float fireRate = 0.1f;
    float fireTimer;

    void Start()
    {
        
    }

    void Update()
    {
        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && fireTimer > fireRate)
        {
            fireTimer = 0;
            RaycastHit hit;
            Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.name);
                if( 
                    hit.collider.CompareTag("Player") 
                    && 
                    !hit.collider.GetComponent<PhotonView>().IsMine
                )
                {
                    hit
                        .collider
                        .GetComponent<PhotonView>()
                        .RPC("TakeDamage", RpcTarget.AllBuffered, 10f);
                }
            }

        }
    }
}
