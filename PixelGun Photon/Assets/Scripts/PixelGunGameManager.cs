using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PixelGunGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsConnected)
        {
            if(playerPrefab != null)
            { 
                int randomZ = Random.Range(-20,-28);
                int randomX = Random.Range(0, 10);


                PhotonNetwork.Instantiate(playerPrefab .name, new Vector3(randomX, 22, randomZ),Quaternion.identity);
            
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log(PhotonNetwork.NickName + " joined " + PhotonNetwork.CurrentRoom.Name);

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer.NickName + "joined " + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);

    }
}
