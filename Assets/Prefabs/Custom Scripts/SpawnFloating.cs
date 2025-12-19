using UnityEngine;

public class SpawnFloating : MonoBehaviour
{
    [SerializeField]
    public GameObject object_ref;
    [SerializeField]
    public float float_height = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        object_ref.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + float_height, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
