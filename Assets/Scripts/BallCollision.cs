using UnityEngine;
using System.Collections;

public class BallCollision : MonoBehaviour {

    private Vector3 realSpeed;

    private new Rigidbody rigidbody;
    private SphereCollider sphereCollider;

    private float radius;

    private Vector3 initialChildPos;

    private RaycastHit[] rayHit;
    private int minDistObjIndex;
    private float minDist;
    private RaycastHit sHit;

    public float rawSpeed;
    public Vector3 direction;
    public int resolution;
    public LayerMask collidable;

    bool stuckToInv = false;
    bool restartVelocity = false;

    void Awake() {

        initialChildPos = new Vector3(0.0f, 3.597f, 0.5f);

        //Child initial configuration
        transform.GetChild(0).position = new Vector3(initialChildPos.x, -initialChildPos.y, -initialChildPos.z);
        transform.GetChild(0).localScale = new Vector3(0.8f, 0.8f, 0.8f);

        rigidbody = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();

        sphereCollider.center = transform.GetChild(0).position;
        sphereCollider.radius = transform.GetChild(0).localScale.x / 2.0f;

        direction = direction.normalized;

        realSpeed = rawSpeed * direction;

        radius = sphereCollider.radius;

        sHit = new RaycastHit();

        StartCoroutine(Begin());

    }

    IEnumerator Begin() {

        yield return new WaitForSeconds(1.0f);

        rigidbody.velocity = realSpeed;

    }


    void Update() {

        if (!stuckToInv) {

            Vector3 origin = transform.GetChild(0).position;
            Vector3 originAux = origin;

            Vector3 baseVect = new Vector3(0.0f, 1.0f, 0.0f);
            float cosAng = Vector3.Dot(baseVect, direction);
            float angle = Mathf.Acos(cosAng);

            rayHit = new RaycastHit[resolution + 1];

            minDistObjIndex = -1;
            minDist = Mathf.Infinity;

            for (int i = 0; i < resolution + 1; i++) {

                origin = new Vector3(originAux.x + radius * Mathf.Cos((((resolution - i) * (180 / resolution)) * Mathf.Deg2Rad) + (direction.x < 0 ? angle : -angle)),
                                     originAux.y + radius * Mathf.Sin((((resolution - i) * (180 / resolution)) * Mathf.Deg2Rad) + (direction.x < 0 ? angle : -angle)),
                                     originAux.z);

                float moveDist = Vector3.Distance(origin, origin + realSpeed * Time.fixedDeltaTime);

                //Debug.DrawLine(origin, origin + realSpeed * Time.fixedDeltaTime, Color.green, 0.0f, false);

                if (Physics.Raycast(origin, direction, out rayHit[i], moveDist, collidable))
                    if (rayHit[i].distance < minDist) {

                        minDist = rayHit[i].distance;
                        minDistObjIndex = i;

                        Physics.Raycast(rayHit[minDistObjIndex].point, -direction, out sHit, rayHit[minDistObjIndex].distance + 10, collidable);

                    }

            }

            if(restartVelocity)
                rigidbody.velocity = realSpeed;

        }

        if (stuckToInv) {

            Vector3 ballPos = parentToChildPos(transform.position) - sHit.point;
            ballPos = rayHit[minDistObjIndex].point + ballPos;

            transform.position = childToParentPos(ballPos);

            //Bouncing se na em bordar
            if (rayHit[minDistObjIndex].transform.CompareTag("Boundary")) {

                switch (rayHit[minDistObjIndex].transform.GetSiblingIndex()) {

                case 0:
                case 1:
                direction = new Vector3(-direction.x, direction.y, direction.z);
                break;

                case 2:
                case 3:
                direction = new Vector3(direction.x, -direction.y, direction.z);
                break;

                }

                realSpeed = rawSpeed * direction;
                stuckToInv = false;
                restartVelocity = true;
                minDistObjIndex = -1; //Se não stuckToInv seria reiniciado novamente para true;

            }
        }

        if (minDistObjIndex != -1) {
            stuckToInv = true;
            rigidbody.velocity = Vector3.zero;
        }

    }

    private Vector3 parentToChildPos(Vector3 parent) {
        return new Vector3(parent.x, parent.y - initialChildPos.y, parent.z - initialChildPos.z);
    }

    private Vector3 childToParentPos(Vector3 child) {
        return new Vector3(child.x, child.y + initialChildPos.y, child.z + initialChildPos.z);
    }

}