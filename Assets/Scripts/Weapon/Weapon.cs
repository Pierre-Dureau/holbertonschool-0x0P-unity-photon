using UnityEngine;
using Photon.Pun;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject bullet;

    public float fireRate = 1f;
    public float bulletSpeed = 7f;
    private float nextFire = 0f;

    private void Update()
    {
        if (nextFire > 0f)
            nextFire -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && nextFire <= 0f) {
            nextFire = fireRate;
            Fire();
        }
    }

    private void Fire() {
        Vector3 camForward = camera.transform.forward;
        GameObject newBullet = PhotonNetwork.Instantiate(bullet.name, transform.position + camForward, Quaternion.identity);

        if (newBullet.TryGetComponent<Rigidbody>(out var bulletRigidbody))
            bulletRigidbody.velocity = camForward * bulletSpeed;

        Destroy(newBullet, 4f);
    }
}
