using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    private string debugTextOut;

    public float updateInterval = 0.5f;
    private double lastInterval;
    private int frames = 0;
    private float fps;

    void Start() {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }

    void Update() {

        ++frames;

        float timeNow = Time.realtimeSinceStartup;

        if (timeNow > lastInterval + updateInterval) {
            fps = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;
        }

        debugTextOut  = "Debug\n";
        debugTextOut += "Time: " + timeNow + "\n";
        debugTextOut += "Delta Time: " + Time.deltaTime + "\n";
        debugTextOut += "FPS: " + fps;

        GameObject.Find("Debug").GetComponent<Text>().text = debugTextOut;

    }

    public static GameObject GetChildGameObject(GameObject fromGameObject, string withName) {
        Transform[] ts = fromGameObject.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

}
