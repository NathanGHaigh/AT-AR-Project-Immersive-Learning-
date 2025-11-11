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
        timer = 10.0f;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            Debug.Log("Fired");
            GameObject Decay = Instantiate(NeutronPrefab, Uranium.transform.position, Quaternion.identity);
            Decay.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * 5.0f;
            timer = 10.0f;
        }
    }
}
