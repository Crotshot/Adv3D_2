using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;//added

public class PlanetGenerator : MonoBehaviour {
    [SerializeField] public float baseOrbitalSpeed = .20f, baseOrbitalRotationalSpeed = 20f, baseDistanceToSun = 150, baseDiameter = 10f;
    [SerializeField] GameObject planetPrefab;

    void Start()
    {
        PlanetsFromXML();
    }

    void PlanetsFromXML() {
        TextAsset textAsset = (TextAsset)Resources.Load("planets");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);

        foreach (XmlNode planet in doc.SelectNodes("Data/planets/planet")) {
            string planetName = planet.Attributes.GetNamedItem("name").Value;
            float diameter = StringToFloat(planet.Attributes.GetNamedItem("diameter").Value),
            distancetoSun = StringToFloat(planet.Attributes.GetNamedItem("distancetoSun").Value),
            rotationPeriod = StringToFloat(planet.Attributes.GetNamedItem("rotationPeriod").Value),
            orbitalVelocity = StringToFloat(planet.Attributes.GetNamedItem("orbitalVelocity").Value);
            //print("Planet Name:" + planetName + ", Normalised Distance: " + distancetoSun + ", Normalised Rotational Period: " + rotationPeriod + ", Normalised Orbital Velocity" + orbitalVelocity);

            GameObject obj = Instantiate(planetPrefab, transform);
            obj.name = planetName;
            obj.GetComponent<Planet>().SetupPlanet(diameter, distancetoSun, rotationPeriod, orbitalVelocity, this);
        }
    }

    float StringToFloat(string s) {
        return float.Parse(s);
    }

    Vector3 StringToVector3(string s) {
        string[] newString = s.Split(new char[] { ',' });
        return (new Vector3(float.Parse(newString[0]), float.Parse(newString[1]), float.Parse(newString[2])));
    }
}


//TextAsset textAsset = (TextAsset)Resources.Load("scene");
//XmlDocument doc = new XmlDocument();
//doc.LoadXml(textAsset.text);
//foreach (XmlNode level in doc.SelectNodes("game/level")) {
//    if (level.Attributes.GetNamedItem("number").Value == "1") {
//        foreach (XmlNode gameObject in level.SelectNodes(".//object")) {
//            string name, location;
//            name = gameObject.Attributes.GetNamedItem("name").Value;
//            location = gameObject.Attributes.GetNamedItem("location").Value;
//            Vector3 v = ConvertStringToVector(location);
//            GameObject g = (GameObject)Instantiate(wall, v, Quaternion.identity);
//            g.name = name;
//        }
//    }
//}
