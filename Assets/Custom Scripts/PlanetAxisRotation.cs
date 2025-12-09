using UnityEngine;

public class PlanetAxisRotation : MonoBehaviour
{
    [SerializeField]
    private string planetName;
    [SerializeField]
    private float rotationSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform planetTransform = this.planetName != null ? this.transform : null;
        switch (planetTransform.name)
        {
            case "Mercury(Clone)":
                rotationSpeed = 6.0f;
                planetName = "Mercury";
                break;
            case "Venus(Clone)":
                rotationSpeed = 4.0f;
                planetName = "Venus";
                break;
            case "Earth(Clone)":
                rotationSpeed = 15.0f;
                planetName = "Earth";
                break;
            case "Mars(Clone)":
                rotationSpeed = 10.0f;
                planetName = "Mars";
                break;
            case "Jupiter(Clone)":
                rotationSpeed = 25.0f;
                planetName = "Jupiter";
                break;
            case "Saturn(Clone)":
                rotationSpeed = 20.0f;
                planetName = "Saturn";
                break;
            case "Uranus(Clone)":
                rotationSpeed = 18.0f;
                planetName = "Uranus";
                break;
            case "Neptune(Clone)":
                rotationSpeed = 17.0f;
                planetName = "Neptune";
                break;
           //default:
           //    rotationSpeed = 5.0f;
           //    planetName = "Unknown Planet";
           //    break;
        }
        UnityEngine.Debug.Log($"Planet: {planetName}, Rotation Speed: {rotationSpeed}");
     
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        MainTainAxis();
    }

    void MainTainAxis()
    {
        this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, 0);
    }
}
