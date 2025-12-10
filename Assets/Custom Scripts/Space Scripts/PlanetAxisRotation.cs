using UnityEngine;

public class PlanetAxisRotation : MonoBehaviour
{
    [SerializeField]
    private string planetName;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float axisTilt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform planetTransform = this.planetName != null ? this.transform : null;
        switch (planetTransform.name)
        {
            case "Mercury(Clone)":
                rotationSpeed = 6.0f;
                axisTilt = 0.01f; // Mecury Tilt
                planetName = "Mercury";
                break;
            case "Venus(Clone)":
                rotationSpeed = 4.0f;
                axisTilt = 177.4f; // Venus Tilt
                planetName = "Venus";
                break;
            case "Earth(Clone)":
                rotationSpeed = 15.0f;
                axisTilt = 23.5f; // Earth Tilt
                planetName = "Earth";
                break;
            case "Mars(Clone)":
                rotationSpeed = 10.0f;
                axisTilt = 25.0f; // Mars Tilt
                planetName = "Mars";
                break;
            case "Jupiter(Clone)":
                rotationSpeed = 25.0f;
                axisTilt = 3.1f; // Jupiter Tilt
                planetName = "Jupiter";
                break;
            case "Saturn(Clone)":
                rotationSpeed = 20.0f;
                axisTilt = 26.7f; // Saturn Tilt
                planetName = "Saturn";
                break;
            case "Uranus(Clone)":
                rotationSpeed = 18.0f;
                axisTilt = 97.8f; // Uranus Tilt
                planetName = "Uranus";
                break;
            case "Neptune(Clone)":
                rotationSpeed = 17.0f;
                axisTilt = 28.3f; // Neptune Tilt
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
        this.transform.rotation = Quaternion.Euler(axisTilt, this.transform.rotation.eulerAngles.y, 0);
        MainTainAxis();
    }

    void MainTainAxis()
    {
        this.transform.rotation = Quaternion.Euler(axisTilt, this.transform.rotation.eulerAngles.y, 0);
    }

}
