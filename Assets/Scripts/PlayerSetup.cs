using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private new GameObject camera;

    public void IsLocalPlayer() {
        movement.enabled = true;
        camera.SetActive(true);
    }
}
