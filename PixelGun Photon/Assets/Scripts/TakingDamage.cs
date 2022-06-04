using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class TakingDamage : MonoBehaviourPunCallbacks
{
    public float health;
    [SerializeField]
    Image healthBar;
    [SerializeField]
    MovementController movementController;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        healthBar.fillAmount = health / 100;
    }

    // Update is called once per frame
    void Update()
    {

    }

    [PunRPC]
    void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        healthBar.fillAmount = health / 100;

        if (health <= 0f)
        {
            Die();
        }
    }
    [PunRPC]
    public void ResetHealth()
    {
        health = 100;
        healthBar.fillAmount = health / 100;
    }
    void Die()
    {
        if(photonView.IsMine)
        {
            movementController.Respawn();
            photonView.RPC("ResetHealth",RpcTarget.AllBuffered);
        }
    }
}
