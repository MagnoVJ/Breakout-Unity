using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {

    private Vector3 realSpeed;

    private new Rigidbody rigidbody;
    private SphereCollider sphereCollider;

    private float radius;

    private Vector3 initialChildPos;

    public float rawSpeed;
    public Vector3 direction;
    public int resolution;
    public LayerMask collidable;

    bool cont = false;

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

        StartCoroutine(Begin());

    }

    IEnumerator Begin() {

        yield return new WaitForSeconds(0.1f);

        rigidbody.velocity = realSpeed;

    }


    void Update() {

        if (cont)
            Debug.Log("STOP");

        Vector3 perpAxis = Vector3.Cross(direction, Vector3.forward).normalized;

        Vector3 originNext = transform.GetChild(0).position + realSpeed * Time.deltaTime;

        RaycastHit[] rayHit = new RaycastHit[resolution + 1];

        float moveDist = Vector3.Distance(originNext, originNext + direction * radius);

        int minDistObjIndex = -1;
        float minDist = Mathf.Infinity;

        Vector3 dirForRay = perpAxis;

        for (int i = 0; i < rayHit.Length; i++) {
            if (Physics.Raycast(originNext, dirForRay, out rayHit[i], moveDist, collidable))
                if (rayHit[i].distance < minDist) {

                    minDist = rayHit[i].distance;
                    minDistObjIndex = i;

                }

            dirForRay = Quaternion.Euler(new Vector3(0.0f, 0.0f, 180 / resolution)) * dirForRay;

        }

        if (minDistObjIndex != -1) {

            float radiusDist = Vector3.Distance(transform.GetChild(0).position, transform.GetChild(0).position + direction * radius);

            Vector3 childPos = transform.GetChild(0).position + direction * (minDist - radiusDist);
            transform.position = childPos + initialChildPos;
            transform.GetChild(0).position = childPos;

            rigidbody.velocity = Vector3.zero;

        }

    }

    

}