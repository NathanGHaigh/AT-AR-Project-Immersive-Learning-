using UnityEngine;
using UnityEngine.UI;

public class CrosshairControl : MonoBehaviour
{
    [SerializeField]
    private GameObject crosshair;
    [SerializeField]
    private GameObject Greeting_UI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Greeting_UI.activeSelf == true)
        {
            crosshair.GetComponent<Image>().enabled = false;
        }
        else
        {
            crosshair.GetComponent<Image>().enabled = true;
        }
    }
}
