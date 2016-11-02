using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BallMovement))]
public class BallScriptEditor : Editor {

    const float simDeltaTime = 0.01656758f;
    Vector3 direction;
    Vector3 realSpeed;
    float radius;

    Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles) {

        Vector3 dir = point - pivot;
        dir = Quaternion.Euler(angles) * dir;
        point = dir + pivot;

        return point;

    }


    void OnSceneGUI() {

        BallMovement ballMovement = target as BallMovement;

        direction = ballMovement.direction.normalized;
        realSpeed = ballMovement.rawSpeed * direction;
        radius = ballMovement.transform.GetComponent<SphereCollider>().radius;

        Vector3 perpAxis = Vector3.Cross(direction, Vector3.forward).normalized;

        Vector3 originNext =  ballMovement.transform.GetChild(0).position + realSpeed * simDeltaTime;

        Vector3 dirForRay = perpAxis;

        for (int i = 0; i < ballMovement.resolution + 1; i++) {

            Debug.DrawLine(originNext, originNext + dirForRay * radius, Color.red, 0.0f, false);

            dirForRay = Quaternion.Euler(new Vector3(0.0f, 0.0f, 180 / ballMovement.resolution)) * dirForRay;


        }


    }

	
}
