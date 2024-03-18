using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private GameObject camera;
    [SerializeField] private new string name;
    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private GameObject canvas;

    public void IsLocalPlayer() {
        movement.enabled = true;
        camera.SetActive(true);
        canvas.SetActive(true);
        nameText.enabled = false;
    }

    [PunRPC]
    public void SetName(string _name) {
        name = (_name != "" ? name : "?");
        nameText.text = name;
    }

}
