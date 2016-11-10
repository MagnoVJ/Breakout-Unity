using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PaddleMovement : MonoBehaviour {

    private RaycastHit2D hit;
    private Camera mainCamera;

    public bool beingTouched;

    private Vector3 fingCurrPos;
    private Vector3 fingPrevPos;

    //private Transform parent;

    private bool insideLeftBound;
    private bool insideRightBound;

    public LayerMask touchableObjectsMask;

    void Start() {

        beingTouched = false;
        mainCamera = Camera.main;
        //parent = gameObject.transform.parent;

        insideLeftBound = false;
        insideRightBound = false;

    }

    void FixedUpdate() {

        touchInput();
        //mouseInput();

        if (beingTouched) {

            float distX = fingCurrPos.x - fingPrevPos.x;

            Vector3 paddlePosition = gameObject.GetComponent<Transform>().position;

            if (distX < 0) {
                if (!insideLeftBound)
                    gameObject.GetComponent<Transform>().position = new Vector3(paddlePosition.x + distX, paddlePosition.y, paddlePosition.z);
            }
            else if (distX > 0) {
                if(!insideRightBound)
                    gameObject.GetComponent<Transform>().position = new Vector3(paddlePosition.x + distX, paddlePosition.y, paddlePosition.z);
            }

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

            if (touch.phase == TouchPhase.Moved) {

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

    void OnTriggerEnter(Collider other) {

        Vector3 paddlePosition = gameObject.GetComponent<Transform>().position;

        if (other.name == "BoundaryLeft") {

            gameObject.transform.position = new Vector3((other.transform.position.x + other.transform.localScale.x / 2) + gameObject.transform.localScale.x * gameObject.GetComponent<CapsuleCollider>().height / 2,
                                                         gameObject.transform.position.y, 
                                                         gameObject.transform.position.z);
            insideLeftBound = true;

        }
        else if (other.name == "BoundaryRight") {

            gameObject.transform.position = new Vector3((other.transform.position.x - other.transform.localScale.x / 2) - gameObject.transform.localScale.x * gameObject.GetComponent<CapsuleCollider>().height / 2,
                                                         gameObject.transform.position.y,
                                                         gameObject.transform.position.z);

            insideRightBound = true;

        }

    }

    void OnTriggerExit(Collider other) {

        if (other.name == "BoundaryLeft")
            insideLeftBound = false;
        else if (other.name == "BoundaryRight")
            insideRightBound = false;

    }

}   