using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private readonly float sphereLifetime = 3f;

    private PhotonView photonView;

    private void Awake() {
        photonView = GetComponent<PhotonView>();
    }

    private void Start() {
        Invoke(nameof(DestroySphere), sphereLifetime);
    }

    private void DestroySphere() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (photonView.IsMine) {
                if (collision.gameObject.GetComponent<Health>().health <= 33.5f) {
                    RoomManager.instance.kills++;
                    RoomManager.instance.SetHashes();
                    PhotonNetwork.LocalPlayer.AddScore(3); // Kill
                }
                else
                    PhotonNetwork.LocalPlayer.AddScore(1); // Hit
                collision.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 33.5f);

            }
        }
        
        Destroy(gameObject);
    }

}
