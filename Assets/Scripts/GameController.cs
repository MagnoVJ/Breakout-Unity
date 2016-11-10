using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    private string debugTextOut;

    public float updateInterval = 0.5f;
    private double lastInterval;
    private int frames = 0;
    private float fps;

    private float widthInWorld;
    private float heightInWorld;

    void Awake() {

        heightInWorld = Camera.main.orthographicSize * 2;
        widthInWorld = heightInWorld * Screen.width / Screen.height;

        //Define os limite da tela para a bola e o paddle
        //BoundaryLeft
        Vector3 localScale = GameObject.Find("BoundaryLeft").transform.localScale;
        GameObject.Find("BoundaryLeft").transform.localScale = new Vector3(localScale.x, heightInWorld, localScale.z);
        float posiCent = -(widthInWorld / 2);
        Vector3 position = GameObject.Find("BoundaryLeft").transform.position;
        GameObject.Find("BoundaryLeft").transform.position = new Vector3(posiCent - localScale.x / 2, position.y, position.z);

        //BoundaryRight
        localScale = GameObject.Find("BoundaryRight").transform.localScale;
        GameObject.Find("BoundaryRight").transform.localScale = new Vector3(localScale.x, heightInWorld, localScale.z);
        posiCent = widthInWorld / 2;
        position = GameObject.Find("BoundaryRight").transform.position;
        GameObject.Find("BoundaryRight").transform.position = new Vector3(posiCent + localScale.x / 2, position.y, position.z);

        //BoundaryTop
        localScale = GameObject.Find("BoundaryTop").transform.localScale;
        GameObject.Find("BoundaryTop").transform.localScale = new Vector3(localScale.x, widthInWorld, localScale.z);
        posiCent = heightInWorld / 2;
        position = GameObject.Find("BoundaryTop").transform.position;
        GameObject.Find("BoundaryTop").transform.position = new Vector3(position.x, posiCent + localScale.x / 2, position.z);

        //BoundaryBottom
        localScale = GameObject.Find("BoundaryBottom").transform.localScale;
        GameObject.Find("BoundaryBottom").transform.localScale = new Vector3(localScale.x, widthInWorld, localScale.z);
        posiCent = -(heightInWorld / 2);
        position = GameObject.Find("BoundaryBottom").transform.position;
        GameObject.Find("BoundaryBottom").transform.position = new Vector3(position.x, posiCent - localScale.x / 2, position.z);

    }

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

        foreach (Transform t in ts)
            if (t.gameObject.name == withName)
                return t.gameObject;

        return null;

    }

    public static void ResetPosition() {

        GameObject.FindGameObjectWithTag("Player").transform.position = Vector3.zero;
        GameObject.FindGameObjectWithTag("Ball").transform.position = Vector3.zero;

        FindObjectOfType<BallScript>().ballStuck = true;

        FindObjectOfType<BallCollision>().direction = FindObjectOfType<BallCollision>().directionDefault;

        FindObjectOfType<PaddleMovement>().beingTouched = false;

    }

}