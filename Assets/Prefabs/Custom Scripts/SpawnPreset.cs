using UnityEngine;

public class SpawnPreset : MonoBehaviour
{
    public GameObject atomPrefab;
    [SerializeField]
    private Camera _camera;


    public void OnAwake()
    {
        Vector3 spawnpos = _camera.transform.position + _camera.transform.forward * 2.0f;
        Instantiate(atomPrefab, spawnpos, Quaternion.identity);
    }

}