using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PaddleMovement : MonoBehaviour {

    public LayerMask touchableObjectsMask;

    private RaycastHit2D hit;
    private Camera mainCamera;

    private bool beingTouched;

    private Vector3 fingCurrPos;
    private Vector3 fingPrevPos;

    private Vector3 paddlePosition;
    private Transform parent;

    void Start() {

        beingTouched = false;
        mainCamera = Camera.main;
        parent = gameObject.transform.parent;

    }

    void Update() {

        touchInput();
        //mouseInput();

        if (beingTouched) {

            float distX = fingCurrPos.x - fingPrevPos.x;

            paddlePosition = parent.GetComponent<Transform>().position;
            parent.GetComponent<Transform>().position = new Vector3(paddlePosition.x + distX, paddlePosition.y, paddlePosition.z);
            fingPrevPos = fingCurrPos;

        }

    }

    private void touchInput() {

        if (Input.touchCount > 0) {          

            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began) {

                Vector2 ray = new Vector2(mainCamera.GetComponent<Camera>().ScreenToWorldPoint(touch.position).x,
                                            mainCamera.GetComponent<Camera>().ScreenToWorldPoint(touch.position).y);

                hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, touchableObjectsMask);

                if (hit.transform) {

                    GameObject obHit = hit.transform.gameObject;

                    if (obHit.CompareTag("PlayerArea")) {

                        fingCurrPos = hit.point;
                        fingPrevPos = hit.point;

                        beingTouched = true;

                    }

                }

            }

            if (touch.phase == TouchPhase.Ended)
                beingTouched = false;

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
                Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(touch.position);
                fingCurrPos = ray.GetPoint(0);
            }


        }


    }

    private void mouseInput() {

        if (Input.GetMouseButtonDown(0)) {

            Vector2 ray = new Vector2(mainCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition).x,
                                      mainCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition).y);

            hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, touchableObjectsMask);

            if (hit.transform) {

                GameObject obHit = hit.transform.gameObject;

                if (obHit.CompareTag("PlayerArea")) {

                    fingCurrPos = hit.point;
                    fingPrevPos = hit.point;

                    beingTouched = true;

                }

            }

        }


        if (Input.GetMouseButtonUp(0)) {

            beingTouched = false;

        }

        if (Input.GetMouseButton(0)) {

            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            fingCurrPos = ray.GetPoint(0);

        }

    }

}

