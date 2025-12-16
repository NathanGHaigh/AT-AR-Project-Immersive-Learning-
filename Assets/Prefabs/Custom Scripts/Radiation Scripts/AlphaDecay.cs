using UnityEngine;

public class AlphaDecay : MonoBehaviour
{
    [SerializeField]
    public GameObject AlphaParticlePrefab;
    [SerializeField]
    public GameObject decayVector;
    [SerializeField]
    public float decayRate = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        decayVector = this.gameObject;
        InvokeRepeating("Decay", 5f, decayRate);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Decay()
    {
        Debug.Log("Alpha Decay Fired");
        GameObject Decay = Instantiate(AlphaParticlePrefab, decayVector.transform.position, Quaternion.identity);
        Vector3 alphaDecay = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)).normalized / 2;
        Rigidbody rb = Decay.GetComponent<Rigidbody>();
        rb.linearVelocity = alphaDecay;
    }
}
