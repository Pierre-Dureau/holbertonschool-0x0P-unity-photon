using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [HideInInspector] public static RoomManager instance;
    public GameObject player;

    [Space] public Transform[] spawnPoints;
    [Space] public GameObject roomCam;
    [Space] public GameObject nameUI;
    [Space] public GameObject connUI;

    private new string name = "";
    [HideInInspector] public int kills = 0;
    [HideInInspector] public int deaths = 0;

    public string roomNameToJoin = "test";

    private void Awake() {
        instance = this;
    }

    public void ChangeName(string _name) {
        name = _name;
    }

    public void JoinRoomButtonPressed() {
        Debug.Log("Connecting...");

        nameUI.SetActive(false);
        connUI.SetActive(true);

        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);
    }

    public override void OnJoinedRoom() {
        base.OnJoinedRoom();

        Debug.Log("Connected to a room");

        roomCam.SetActive(false);

        SpawnPlayer();
    }

    public void SpawnPlayer() {
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);

        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponent<Health>().isLocalPlayer = true;
        _player.GetComponent<PhotonView>().RPC("SetName", RpcTarget.AllBuffered, name);
        PhotonNetwork.LocalPlayer.NickName = name;
    }

    public void SetHashes() {
        try {
            Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;

            hash["kills"] = kills;
            hash["deaths"] = deaths;

            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        } catch { }
    }
}
