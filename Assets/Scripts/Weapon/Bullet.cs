using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private readonly float sphereLifetime = 7f;

    private PhotonView photonView;

    private void Start() {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine) {
            Invoke(nameof(DestroySphere), sphereLifetime);
        }
    }

    private void DestroySphere() {
        if (photonView.IsMine) {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (photonView.IsMine) {
            if (collision.gameObject.CompareTag("Player")) {
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
