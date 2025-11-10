using UnityEngine;

public class SpawnAuto : MonoBehaviour
{
    public GameObject atomPrefab; // Prefab of the atom to spawn

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera cam = Camera.main;
        Vector3 spawnpos = cam.transform.position + cam.transform.forward * 2.0f; // 2 units in front of the camera
        Instantiate(atomPrefab, spawnpos, Quaternion.identity);
    }
}
