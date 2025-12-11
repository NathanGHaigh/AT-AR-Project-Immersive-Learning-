using UnityEngine;

public class SpawnPreset : MonoBehaviour
{
    public GameObject atomPrefab; // Prefab of the atom to spawn

    [SerializeField]
    private Camera _camera;


    public void OnAwake()
    {
        Vector3 spawnpos = _camera.transform.position + _camera.transform.forward * 2.0f; // 2 units in front of the camera
        Instantiate(atomPrefab, spawnpos, Quaternion.identity);
    }

}