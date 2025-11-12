using System.Collections.Specialized;
using UnityEngine;

public class FissionReaction : MonoBehaviour
{
    [SerializeField]
    public GameObject Uranium;

    [SerializeField]
    public GameObject NeutronPrefab;

    float timer = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            Debug.Log("Fired");

            GameObject Decay = Instantiate(NeutronPrefab, Uranium.transform.position, Quaternion.identity);
            Vector3 fission = new Vector3(Random.Range(-5,5), Random.Range(-5, 5), Random.Range(-5, 5)).normalized;
            Rigidbody rb = Decay.GetComponent<Rigidbody>(); 
            rb.linearVelocity = fission;          
            timer = 5.0f;        
        }
    }
}
