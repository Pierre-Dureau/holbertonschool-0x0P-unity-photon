using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [HideInInspector] public static RoomManager instance;
    public GameObject player;

    [Space] public Transform[] spawnPoints;
    [Space] public GameObject roomCam;
    [Space] public GameObject connUI;

    [SerializeField] private TextMeshProUGUI nameText;

    public new string name = "";
    [HideInInspector] public int kills = 0;
    [HideInInspector] public int deaths = 0;

    [HideInInspector] public string roomNameToJoin = "?";

    private void Awake() {
        instance = this;
    }

    public void ChangeName(string _name) {
        name = _name;
        nameText.text = name;
    }

    public void JoinRoomButtonPressed() {
        connUI.SetActive(true);

        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);
    }

    public override void OnJoinedRoom() {
        base.OnJoinedRoom();

        roomCam.SetActive(false);

        SpawnPlayer();
    }

    public void SpawnPlayer() {
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);

        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponent<Health>().isLocalPlayer = true;
        _player.GetComponent<PhotonView>().RPC("SetName", RpcTarget.AllBuffered, name);
        _player.GetComponent<PhotonView>().RPC("SetMaterial", RpcTarget.AllBuffered, _player.GetComponent<PhotonView>().Owner.ActorNumber - 1);
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
