using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class LounchManager : MonoBehaviourPunCallbacks
{
    public GameObject EnterGamePnel;
    public GameObject ConnectionStatusPannel;
    public GameObject LobbyPanel;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        //PhotonNetwork.ConnectUsingSettings()
        EnterGamePnel.SetActive(true);
        ConnectionStatusPannel.SetActive(false);
        LobbyPanel.SetActive(false);
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public void ConnectedToPhotonServer()
    {
       if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            ConnectionStatusPannel.SetActive(true);
            EnterGamePnel.SetActive(false);
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + "Connected");
        LobbyPanel.SetActive(true);
        ConnectionStatusPannel.SetActive(false);
    }

    public override void OnConnected()
    {
        
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoinRoom();
        
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log(PhotonNetwork.NickName + " joined room " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("GameScene");
    }

    void CreateAndJoinRoom()
    {
        string randomName = "Room" + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = (byte)4;
        Debug.Log("Creating Room");
        PhotonNetwork.CreateRoom(randomName,roomOptions);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer.NickName + "joined room " + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
}
