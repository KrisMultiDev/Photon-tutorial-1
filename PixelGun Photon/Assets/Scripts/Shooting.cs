using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Shooting : MonoBehaviour
{
    [SerializeField]
    Camera FPSCamera;
    public float firerate = 0.1f;
    float fireTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fireTimer < firerate)
        {
            fireTimer += Time.deltaTime;
        }
        if(Input.GetButton("Fire1") && fireTimer > firerate)
        {
            fireTimer = 0f;
            RaycastHit hit;
            Ray ray = FPSCamera.ViewportPointToRay(new Vector3(0.5f,0.5f));
            if(Physics.Raycast(ray,out hit,1000))
            {
                Debug.Log(hit.collider.gameObject.name);

                if(hit.collider.gameObject.CompareTag("Player") && !hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    hit.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamage",RpcTarget.AllBuffered,5f);
                }
            }
        }
    }
}
