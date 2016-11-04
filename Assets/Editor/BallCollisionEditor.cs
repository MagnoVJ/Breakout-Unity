using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BallCollision))]
public class BallCollisionEditor : Editor {

    const float simDeltaTime = 0.01656758f;
    Vector3 direction;
    Vector3 realSpeed;
    float radius;

    void OnSceneGUI() {

        //BallCollision ballMovement = target as BallCollision;

        //direction = ballMovement.direction.normalized;
        //realSpeed = ballMovement.rawSpeed * direction;
        //radius = ballMovement.transform.GetComponent<SphereCollider>().radius;

        //Vector3 origin = ballMovement.transform.GetChild(0).position;
        //Vector3 originAux = origin;

        //Vector3 baseVect = new Vector3(0.0f, 1.0f, 0.0f);
        //float cosAng = Vector3.Dot(baseVect, direction);
        //float angle = Mathf.Acos(cosAng);

        //for (int i = 0; i < ballMovement.resolution + 1; i++) {

        //    origin = new Vector3(originAux.x + radius * Mathf.Cos((((ballMovement.resolution - i) * (180 / ballMovement.resolution)) * Mathf.Deg2Rad) + (direction.x < 0 ? angle : -angle)),
        //                         originAux.y + radius * Mathf.Sin((((ballMovement.resolution - i) * (180 / ballMovement.resolution)) * Mathf.Deg2Rad) + (direction.x < 0 ? angle : -angle)),
        //                         originAux.z);

        //    Debug.DrawLine(origin, origin + realSpeed * simDeltaTime, Color.red, 0.0f, false);

        //}

    }

}