using UnityEngine;

public class FloatOrbitSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject SolarSystem;
    [SerializeField]
    private GameObject AsteroidBeltPrefab;
    [SerializeField]
    private Transform centerPoint;
    [SerializeField]
    private float mercuryOrbitSpeed = 47.87f;
    [SerializeField]
    private float venusOrbitSpeed = 35.02f;
    [SerializeField]
    private float earthOrbitSpeed = 29.78f;
    [SerializeField]
    private float marsOrbitSpeed = 24.07f;
    [SerializeField]
    private float jupiterOrbitSpeed = 13.07f;
    [SerializeField]
    private float saturnOrbitSpeed = 9.69f;
    [SerializeField]
    private float uranusOrbitSpeed = 6.81f;
    [SerializeField]
    private float neptuneOrbitSpeed = 5.43f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        transform.position = new(0,1,0);
        InstaniateAsteroidBelt();
        InstaniateKupierBelt();

    }

    // Update is called once per frame
    void Update()
    {
        Transform mercury = SolarSystem.transform.Find("Mercury");
        Transform venus = SolarSystem.transform.Find("Venus");
        Transform earth = SolarSystem.transform.Find("Earth");
        Transform mars = SolarSystem.transform.Find("Mars");
        Transform jupiter = SolarSystem.transform.Find("Jupiter");
        Transform saturn = SolarSystem.transform.Find("Saturn");
        Transform uranus = SolarSystem.transform.Find("Uranus");
        Transform neptune = SolarSystem.transform.Find("Neptune");
        Transform AsteroidBelt = SolarSystem.transform.Find("Belt");
        Transform KupierBelt = SolarSystem.transform.Find("Belt2");

        for (int i = 0; i < SolarSystem.transform.childCount; i++)
        {
            Transform child = SolarSystem.transform.GetChild(i);
            if (child.name.StartsWith("Asteroid"))
            {
                float rotationSpeed = Random.Range(10f, 50f);
                child.RotateAround(centerPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }

        for(int i = 0; i < SolarSystem.transform.childCount; i++)
        {
            Transform child = SolarSystem.transform.GetChild(i);
            if (child.name.StartsWith("AsteroidKupier"))
            {
                float rotationSpeed = Random.Range(5f, 20f);
                child.RotateAround(centerPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }

        float time = Time.time;
        if (mercury != null)
            mercury.RotateAround(centerPoint.position, Vector3.up, mercuryOrbitSpeed * Time.deltaTime);
        if (venus != null)
            venus.RotateAround(centerPoint.position, Vector3.up, venusOrbitSpeed * Time.deltaTime);
        if (earth != null)
            earth.RotateAround(centerPoint.position, Vector3.up, earthOrbitSpeed * Time.deltaTime);
        if (mars != null)
            mars.RotateAround(centerPoint.position, Vector3.up, marsOrbitSpeed * Time.deltaTime);
        if(AsteroidBelt != null)
            AsteroidBelt.RotateAround(centerPoint.position, Vector3.up, 15f * Time.deltaTime);
        if(KupierBelt != null)
            KupierBelt.RotateAround(centerPoint.position, Vector3.up, 7f * Time.deltaTime);
        if (jupiter != null)
            jupiter.RotateAround(centerPoint.position, Vector3.up, jupiterOrbitSpeed * Time.deltaTime);
        if (saturn != null)
            saturn.RotateAround(centerPoint.position, Vector3.up, saturnOrbitSpeed * Time.deltaTime);
        if (uranus != null)
            uranus.RotateAround(centerPoint.position, Vector3.up, uranusOrbitSpeed * Time.deltaTime);
        if (neptune != null)
            neptune.RotateAround(centerPoint.position, Vector3.up, neptuneOrbitSpeed * Time.deltaTime);
        ToScale();
        retainAllignment();


    }

    private void ToScale()
    {
        Transform mercury = SolarSystem.transform.Find("Mercury");
        Transform venus = SolarSystem.transform.Find("Venus");
        Transform earth = SolarSystem.transform.Find("Earth");
        Transform mars = SolarSystem.transform.Find("Mars");
        Transform jupiter = SolarSystem.transform.Find("Jupiter");
        Transform saturn = SolarSystem.transform.Find("Saturn");
        Transform uranus = SolarSystem.transform.Find("Uranus");
        Transform neptune = SolarSystem.transform.Find("Neptune");
        Transform Sun = SolarSystem.transform;
        if (Sun != null)
            Sun.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        if (mercury != null)
            mercury.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        if (venus != null)
            venus.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        if (earth != null)
            earth.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        if (mars != null)
            mars.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        if (jupiter != null)
            jupiter.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        if (saturn != null)
            saturn.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        if (uranus != null)
            uranus.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        if (neptune != null)
            neptune.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    private void retainAllignment()
    {
        SolarSystem.transform.rotation = Quaternion.identity;   
        for (int i = 0; i < SolarSystem.transform.childCount; i++)
        {
            Transform child = SolarSystem.transform.GetChild(i);
            child.rotation = Quaternion.identity;
        }
    }

    private void InstaniateAsteroidBelt()
    {
        if (AsteroidBeltPrefab != null)
        {
            for (int i = 0; i < 50; i++)
            {
                GameObject asteroidBelt = Instantiate(AsteroidBeltPrefab, SolarSystem.transform);
                asteroidBelt.name = "Asteroid";
                asteroidBelt.transform.localPosition = new Vector3(Random.Range(3.5f, 4.5f), 0, Random.Range(3.5f, 4.5f));
                asteroidBelt.transform.RotateAround(centerPoint.position, Vector3.up, Random.Range(0f, 360f));
                asteroidBelt.transform.localScale = Vector3.one * 0.05f;               
            }
        }
    }

    private void InstaniateKupierBelt()
    {
        if (AsteroidBeltPrefab != null)
        {
            for (int i = 0; i < 50; i++)
            {
                GameObject asteroidBelt = Instantiate(AsteroidBeltPrefab, SolarSystem.transform);
                asteroidBelt.name = "AsteroidKupier";
                asteroidBelt.transform.localPosition = new Vector3(Random.Range(7.5f, 9f), 0, Random.Range(7.5f, 9f));
                asteroidBelt.transform.RotateAround(centerPoint.position, Vector3.up, Random.Range(0f, 360f));
                asteroidBelt.transform.localScale = Vector3.one * 0.05f;
            }
        }
    }
}
