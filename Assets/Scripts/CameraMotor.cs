using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;

    private void Start() {
        lookAt = GameObject.Find("Player").transform;
    }

    private void LateUpdate() {
        Vector3 delta = Vector3.zero;

        //check if inside bounds on the X-axis
        float deltaX = lookAt.position.x - transform.position.x; //transform.position.x is center of camera
        if (deltaX > boundX || deltaX < -boundX) {
            if (transform.position.x < lookAt.position.x) { //player on the right of the camera
                delta.x = deltaX - boundX;
            }
            else {
                delta.x = deltaX + boundX;
            }
        }

        //check if inside bounds on the Y-axis
        float deltaY = lookAt.position.y - transform.position.y; //transform.position.y is center of camera
        if (deltaY > boundY || deltaY < -boundY) {
            if (transform.position.y < lookAt.position.y) { //player on the right of the camera
                delta.y = deltaY - boundY;
            }
            else {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
