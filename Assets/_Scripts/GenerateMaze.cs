using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class GenerateMaze : MonoBehaviour {
    Color[,] colorOfPixel;
    public GameObject wall;
    int[,] worldMap;
    public Texture2D outlineImage;

    void Start() {
        //CreateFromArray();
        //CreateFromFile();
        //CreateFromImage();
        CreateFromXML();
    }

    void CreateFromXML() {
        TextAsset textAsset = (TextAsset)Resources.Load("scene");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);

        foreach (XmlNode level in doc.SelectNodes("game/level")) {
            if (level.Attributes.GetNamedItem("number").Value == "1") {
                foreach (XmlNode gameObject in level.SelectNodes(".//object")) {
                    string name, location;
                    name = gameObject.Attributes.GetNamedItem("name").Value;
                    location = gameObject.Attributes.GetNamedItem("location").Value;
                    Vector3 v = ConvertStringToVector(location);
                    GameObject g = Instantiate(wall, v, Quaternion.identity);
                    g.name = name;
                }
            }
        }
    }


    Vector3 ConvertStringToVector(string s) {
        string[] newString;
        newString = s.Split(new char[] { ',' });
        float x = float.Parse(newString[0]),
            y = float.Parse(newString[1]),
            z = float.Parse(newString[2]);
        return (new Vector3(x, y, z));
    }

    void CreateFromImage() {
        colorOfPixel = new Color[outlineImage.width, outlineImage.height];
        for (int x = 0; x < outlineImage.width; x++) {
            for (int y = 0; y < outlineImage.height; y++) {
                colorOfPixel[x, y] = outlineImage.GetPixel(x, y);
                if (colorOfPixel[x, y] != Color.white) {
                    GameObject t = Instantiate(wall,new Vector3((outlineImage.width / 2) * 10 - x * 10, 1.5f, (outlineImage.height / 2) * 10 - y * 10), Quaternion.identity);
                    t.transform.parent = transform;
                }
            }
        }
    }

    void CreateFromFile() {
        TextAsset t1 = (TextAsset)Resources.Load("maze", typeof(TextAsset));
        string s = t1.text;
        int i;
        s = s.Replace("\n", "");
        for (i = 0; i < s.Length; i++) {
            if (s[i] == '1') {
                int column, row;
                column = i % 10;
                row = i / 10;
                GameObject t = Instantiate(wall, new Vector3(-50 + column * 10, 1.5f, 50 - row * 10), Quaternion.identity);
                t.transform.parent = transform;
            }
        }
    }

    void CreateFromArray() {
        worldMap = new int[,]
        {
            {1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0},
            {1,0,0,0,0,0,0,0,0,0},
            {1,0,0,0,0,0,0,0,0,0},
            {1,1,1,1,0,0,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0},
            {1,0,0,0,0,0,0,0,0,0},
            {1,0,0,0,0,0,0,0,0,0},
            {1,0,0,0,0,0,0,0,0,0},
            {1,1,1,1,1,1,1,1,1,1},
        };

        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 10; j++) {
                GameObject t;
                if (worldMap[i, j] == 1) {
                    t = Instantiate(wall, new Vector3(50 - i * 10, 1.5f, 50 - j * 10), Quaternion.identity);
                    t.transform.parent = transform;
                }
            }
        }
    }
}

