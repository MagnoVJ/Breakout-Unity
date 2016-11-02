using UnityEngine;
using System.Collections;

public class Teste : MonoBehaviour {

    private float widthInWorld;
    private float heightInWorld;

    void Start() {

        heightInWorld = Camera.main.orthographicSize * 2;
        widthInWorld = heightInWorld * Screen.width / Screen.height;

    }

    void Update() {

        Debug.Log(widthInWorld);


    }
  
}
