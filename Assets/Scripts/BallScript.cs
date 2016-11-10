using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

    [HideInInspector]
    public bool ballStuck = true;

    private RaycastHit hit;

    Camera viewCamera;
    GameObject ball;

    void Start() {

        viewCamera = Camera.main;

        ball = GameObject.FindGameObjectWithTag("Ball");

    }

    void Update() {

        touchInput();
        //mouseInput();

    }

    void FixedUpdate() {

        //Atualiza a posição da bola caso ela esteja stuck
        BallCollision ballColl = FindObjectOfType<BallCollision>();
        GameObject paddle = GameObject.FindGameObjectWithTag("Player");

        if (ballStuck)
            ballColl.transform.position = new Vector3(paddle.transform.position.x, ballColl.transform.position.y, ballColl.transform.position.z);

    }

    private void touchInput() {

        if (Input.touchCount > 0) {

            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began) {

                Vector3 tch = touch.position;

                tch.z = viewCamera.transform.position.y;

                Vector3 mousePos = viewCamera.ScreenToWorldPoint(tch);

                if (Physics.Raycast(mousePos, Vector3.forward, out hit)) {

                    if (hit.transform.CompareTag("Ball")) {

                        ballStuck = false;

                        ball.GetComponent<Rigidbody>().velocity = ball.GetComponent<BallCollision>().realSpeed;

                    }

                }

            }

        }

    }

    private void mouseInput() {

        if (Input.GetMouseButtonDown(0)) {

            Vector3 tch = Input.mousePosition;

            tch.z = viewCamera.transform.position.y;

            Vector3 mousePos = viewCamera.ScreenToWorldPoint(tch);

            if (Physics.Raycast(mousePos, Vector3.forward, out hit)) {

                if (hit.transform.CompareTag("Ball")) {

                    ballStuck = false;

                    ball.GetComponent<Rigidbody>().velocity = ball.GetComponent<BallCollision>().realSpeed;

                }

            }

        }

    }

}