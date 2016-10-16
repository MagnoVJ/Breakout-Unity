using UnityEngine;
using System.Collections;

public class Teste : MonoBehaviour {

    void OnTriggerEnter(Collider other) {

        Debug.Log("Colidiu: " + other.tag);


    }

}
