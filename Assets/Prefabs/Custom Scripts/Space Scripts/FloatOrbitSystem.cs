using System;
using System.Collections;
using System.Collections.Specialized;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloatOrbitSystem : MonoBehaviour
{
    [Header("Solar System Orbit Parameters")]
//References to Self and Orbit Center Point
[SerializeField]
    private GameObject SolarSystem;
    [SerializeField]
    private Transform centerPoint;

    [Header("Planet and Belt Orbital Parameters")]
    //Rotation Speeds for the Planets and Belts
    [SerializeField]
    private float mercuryOrbitSpeed = 47.87f;
    [SerializeField]
    private float venusOrbitSpeed = 35.02f;
    [SerializeField]
    private float earthOrbitSpeed = 29.78f;
    [SerializeField]
    private float marsOrbitSpeed = 24.07f;
    [SerializeField]
    private float ceresOrbitSpeed = 17.88f;
    [SerializeField]
    private float jupiterOrbitSpeed = 13.07f;
    [SerializeField]
    private float saturnOrbitSpeed = 9.69f;
    [SerializeField]
    private float uranusOrbitSpeed = 6.81f;
    [SerializeField]
    private float neptuneOrbitSpeed = 5.43f;

    [Header("Dwarf Planet Orbit Parameters")]
    //Dwarf Planet Rotation Speeds and Orbital Parameters
    [SerializeField]
    float orbitScale = 0.01f;
    [Header("Pluto Orbital Parameters")]
    //Pluto
    [SerializeField]
    private float plutoOrbitSpeed = 4.74f;
    [SerializeField]
    private float plutosemiMajorAxis = 39.48f;
    [SerializeField]
    private float plutosemiMinorAxis = 38.86f;
    [SerializeField]
    private float plutoTilt = 17f;
    [SerializeField]
    Vector3 PlutoOrbitOffset = new Vector3(0, 0, 0);

    [Header("MakeMake Orbital Parameters")]
    //Makemake
    [SerializeField]
    private float MakemakeOrbitSpeed = 4.0f;
    [SerializeField]
    private float MakemakesemiMajorAxis = 45.8f;
    [SerializeField]
    private float MakemakesemiMinorAxis = 43f;
    [SerializeField]
    private float MakemakeTilt = 29f;
    [SerializeField]
    Vector3 MakemakeOrbitOffset = new Vector3(0, 0, 0);

    [Header("Haumea Orbital Parameters")]
    [SerializeField]
    private float HaumeaOrbitSpeed = 3.5f;
    [SerializeField]
    private float HaumeamiMajorAxis = 43.1f;
    [SerializeField]
    private float HaumeamiMinorAxis = 41f;
    [SerializeField]
    private float HaumeaTilt = 28f;
    [SerializeField]
    Vector3 HaumeaOrbitOffset = new Vector3(0, 0, 0);

    [Header("Eris Orbital Parameters")]
    [SerializeField]
    private float ErisOrbitSpeed = 3.0f;
    [SerializeField]
    private float ErissemiMajorAxis = 67.7f;
    [SerializeField]
    private float ErissemiMinorAxis = 50f;
    [SerializeField]
    private float ErisTilt = 44f;
    [SerializeField]
    Vector3 ErisOrbitOffset = new Vector3(0, 0, 0);

    [Header("Orcus Orbital Parameters")]
    [SerializeField]
    private float OrcusOrbitSpeed = 2.5f;
    [SerializeField]
    private float OrcussemiMajorAxis = 39.4f;
    [SerializeField]
    private float OrcussemiMinorAxis = 37f;
    [SerializeField]
    private float OrcusTilt = 20f;
    [SerializeField]
    Vector3 OrcusOrbitOffset = new Vector3(0, 0, 0);


    //Material and Prefab References
    [SerializeField]
    private Material asteroidMaterial;
    [SerializeField]
    private GameObject AsteroidBeltPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        Transform Ceres = SolarSystem.transform.Find("Ceres");
        Transform Pluto = SolarSystem.transform.Find("Pluto");
        Transform Eris = SolarSystem.transform.Find("Eris");
        Transform Haumea = SolarSystem.transform.Find("Haumea");
        Transform Makemake = SolarSystem.transform.Find("Makemake");
        Transform Orcus = SolarSystem.transform.Find("Orcus");

        for (int i = 0; i < SolarSystem.transform.childCount; i++)
        {
            Transform child = SolarSystem.transform.GetChild(i);
            if (child.name.StartsWith("Asteroid"))
            {
                float rotationSpeed = Random.Range(3f, 6f);
                child.RotateAround(centerPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }

        for(int i = 0; i < SolarSystem.transform.childCount; i++)
        {
            Transform child = SolarSystem.transform.GetChild(i);
            if (child.name.StartsWith("AsteroidKupier"))
            {
                float rotationSpeed = Random.Range(2f, 4f);
                child.RotateAround(centerPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }

        float time = Time.time;
        //Planet Orbits
        if (mercury != null)
            mercury.RotateAround(centerPoint.position, Vector3.up, mercuryOrbitSpeed * Time.deltaTime);
        if (venus != null)
            venus.RotateAround(centerPoint.position, Vector3.up, venusOrbitSpeed * Time.deltaTime);
        if (earth != null)
            earth.RotateAround(centerPoint.position, Vector3.up, earthOrbitSpeed * Time.deltaTime);
        if (mars != null)
            mars.RotateAround(centerPoint.position, Vector3.up, marsOrbitSpeed * Time.deltaTime);
        if(AsteroidBelt != null)
            AsteroidBelt.RotateAround(centerPoint.position, Vector3.up, 3f * Time.deltaTime);
        if (Ceres != null)
            Ceres.RotateAround(centerPoint.position, Vector3.up, ceresOrbitSpeed * Time.deltaTime);
        if (KupierBelt != null)
            KupierBelt.RotateAround(centerPoint.position, Vector3.up, 2f * Time.deltaTime);
        if (jupiter != null)
            jupiter.RotateAround(centerPoint.position, Vector3.up, jupiterOrbitSpeed * Time.deltaTime);
        if (saturn != null)
            saturn.RotateAround(centerPoint.position, Vector3.up, saturnOrbitSpeed * Time.deltaTime);
        if (uranus != null)
            uranus.RotateAround(centerPoint.position, Vector3.up, uranusOrbitSpeed * Time.deltaTime);
        if (neptune != null)
            neptune.RotateAround(centerPoint.position, Vector3.up, neptuneOrbitSpeed * Time.deltaTime);


        //Dwarf Planet Orbits(With Oliptical Approximation)
        if (Pluto != null)
            CalculateandApplyEllipticalOrbit(Pluto, plutosemiMajorAxis, plutosemiMinorAxis, plutoTilt, plutoOrbitSpeed, PlutoOrbitOffset, time);

            //Pluto.RotateAround(centerPoint.position, Vector3.up, plutoOrbitSpeed * Time.deltaTime);
        if (Eris != null)
            CalculateandApplyEllipticalOrbit(Eris, ErissemiMajorAxis, ErissemiMinorAxis, ErisTilt, ErisOrbitSpeed, ErisOrbitOffset, time);

        //Eris.RotateAround(centerPoint.position, Vector3.up, ErisOrbitSpeed * Time.deltaTime);
        if (Haumea != null)
            CalculateandApplyEllipticalOrbit(Haumea, HaumeamiMajorAxis, HaumeamiMinorAxis, HaumeaTilt, HaumeaOrbitSpeed, HaumeaOrbitOffset, time);

        //Haumea.RotateAround(centerPoint.position, Vector3.up, HaumeaOrbitSpeed * Time.deltaTime);
        if (Makemake != null)
            CalculateandApplyEllipticalOrbit(Makemake, MakemakesemiMajorAxis, MakemakesemiMinorAxis, MakemakeTilt, MakemakeOrbitSpeed, MakemakeOrbitOffset, time);

        //Makemake.RotateAround(centerPoint.position, Vector3.up, MakemakeOrbitSpeed * Time.deltaTime);
        if (Orcus != null)
            CalculateandApplyEllipticalOrbit(Orcus, OrcussemiMajorAxis, OrcussemiMinorAxis, OrcusTilt, OrcusOrbitSpeed, OrcusOrbitOffset, time);

        //Orcus.RotateAround(centerPoint.position, Vector3.up, OrcusOrbitSpeed * Time.deltaTime);
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
        Transform Ceres = SolarSystem.transform.Find("Ceres");
        Transform Pluto = SolarSystem.transform.Find("Pluto");
        Transform Eris = SolarSystem.transform.Find("Eris");
        Transform Haumea = SolarSystem.transform.Find("Haumea");
        Transform Makemake = SolarSystem.transform.Find("Makemake");
        Transform Orcus = SolarSystem.transform.Find("Orcus");

        Transform Sun = SolarSystem.transform;
        if (Sun != null)
            Sun.localScale = new Vector3(0.5f, 0.5f, 0.5f) * 2;
        if (mercury != null)
            mercury.localScale = new Vector3(0.05f, 0.05f, 0.05f) * 2;
        if (venus != null)
            venus.localScale = new Vector3(0.1f, 0.1f, 0.1f) * 2;
        if (earth != null)
            earth.localScale = new Vector3(0.1f, 0.1f, 0.1f) * 2;
        if (mars != null)
            mars.localScale = new Vector3(0.08f, 0.08f, 0.08f) * 2;
        if (jupiter != null)
            jupiter.localScale = new Vector3(0.3f, 0.3f, 0.3f) * 2;
        if (saturn != null)
            saturn.localScale = new Vector3(0.25f, 0.25f, 0.25f) * 2;
        if (uranus != null)
            uranus.localScale = new Vector3(0.2f, 0.2f, 0.2f) * 2;
        if (neptune != null)
            neptune.localScale = new Vector3(0.2f, 0.2f, 0.2f) * 2;
        if (Ceres != null)
            Ceres.localScale = new Vector3(0.03f, 0.03f, 0.03f) * 2;
        if (Pluto != null)
            Pluto.localScale = new Vector3(0.04f, 0.04f, 0.04f) * 2;
        if (Eris != null)
            Eris.localScale = new Vector3(0.035f, 0.035f, 0.035f) * 2;
        if (Haumea != null)
            Haumea.localScale = new Vector3(0.03f, 0.03f, 0.03f) * 2;
        if (Makemake != null)
            Makemake.localScale = new Vector3(0.03f, 0.03f, 0.03f) * 2;
        if (Orcus != null)
            Orcus.localScale = new Vector3(0.03f, 0.03f, 0.03f) * 2;

    }

    private void retainAllignment()
    {
        SolarSystem.transform.rotation = Quaternion.identity;   
        //for (int i = 0; i < SolarSystem.transform.childCount; i++)
        //{
        //    Transform child = SolarSystem.transform.GetChild(i);
        //    child.rotation = Quaternion.identity;
        //}
    }

    private void InstaniateAsteroidBelt()
    {
        if (AsteroidBeltPrefab != null)
        {
            for (int i = 0; i < 40; i++)
            {
                GameObject asteroidBelt = Instantiate(AsteroidBeltPrefab, SolarSystem.transform);
                asteroidBelt.name = "Asteroid";
                asteroidBelt.transform.localPosition = new Vector3(Random.Range(3.0f, 3.5f), 0, Random.Range(3.0f, 3.5f));
                asteroidBelt.transform.RotateAround(centerPoint.position, Vector3.up, Random.Range(0f, 360f));
                asteroidBelt.transform.localScale = new Vector3(Random.Range(0.02f, 0.05f), Random.Range(0.02f, 0.05f), Random.Range(0.02f, 0.05f));
                asteroidBelt.transform.rotation = new Quaternion(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
                asteroidBelt.GetComponent<Renderer>().material = asteroidMaterial;

            }
        }
    }

    private void InstaniateKupierBelt()
    {
        if (AsteroidBeltPrefab != null)
        {
            for (int i = 0; i < 80; i++)
            {
                GameObject asteroidBelt = Instantiate(AsteroidBeltPrefab, SolarSystem.transform);
                asteroidBelt.name = "AsteroidKupier";
                asteroidBelt.transform.localPosition = new Vector3(Random.Range(7.5f, 8.0f), 0, Random.Range(7.5f, 8.0f));
                asteroidBelt.transform.RotateAround(centerPoint.position, Vector3.up, Random.Range(0f, 360f));
                asteroidBelt.transform.localScale = new Vector3(Random.Range(0.02f, 0.05f), Random.Range(0.02f, 0.05f), Random.Range(0.02f, 0.05f));
                asteroidBelt.transform.rotation = new Quaternion(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
                asteroidBelt.GetComponent<Renderer>().material = asteroidMaterial;

            }
        }
    }

    private void CalculateandApplyEllipticalOrbit(Transform planet, float semiMajorAxis, float semiMinorAxis, float tiltAngle, float orbitSpeed, Vector3 OrbitOffset, float time)
    {
        float orbitAngle = 0f;
        float orbitspeedScaled = orbitSpeed * orbitScale;
        float tilt = tiltAngle;

        orbitAngle -= orbitSpeed * time;

        Vector3 orbitOffset = OrbitOffset;

        float rad = orbitAngle * Mathf.Deg2Rad;

        float x = semiMajorAxis * Mathf.Cos(rad);
        float z = semiMinorAxis * Mathf.Sin(rad);

        Quaternion rot = Quaternion.Euler(tilt, 0, 0);

        Vector3 pos = rot * new Vector3(x, 0, z) * orbitScale;

        pos = Quaternion.Euler(0, tilt, 0) * pos;

        planet.transform.position = centerPoint.position + pos + orbitOffset;





        int segments = 200;
        float anglestep = 360f / segments;

        Vector3 previousPoint = Vector3.zero;
        bool hasPrev = false;
        for (int i = 0; i <= segments; i++)
        {
            float angle = i * anglestep * Mathf.Deg2Rad;

            float px = semiMajorAxis * Mathf.Cos(angle);
            float pz = semiMinorAxis * Mathf.Sin(angle);

            Vector3 point = new Vector3(px, 0, pz) * orbitScale;
            if (hasPrev)
            {
                Debug.DrawLine(centerPoint.position + Quaternion.Euler(0, tilt, 0) * (rot * previousPoint) + orbitOffset,
                               centerPoint.position + Quaternion.Euler(0, tilt, 0) * (rot * point) + orbitOffset,
                               Color.white);
            }
            previousPoint = point;
            hasPrev = true;
        }

    }
}
