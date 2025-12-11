using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float despawn_timer = 0;

    public GameObject self;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        despawn_timer = 3.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        despawn_timer -= Time.deltaTime;
        if(despawn_timer < 0)
        {
            Destroy(self);
        }
        
    }
}
