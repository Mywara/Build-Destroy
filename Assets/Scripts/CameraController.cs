using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform targetToRotateAround;
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 50.0f;
    public float zoomSpeed = 50.0f;
    public float upAndDownSpeed = 10.0f;
    public float angleMaxVertical = 90.0f;
    public float angleminVertical = -30.0f;
    public float minDistance = 10;
    public float maxDistance = 50;
    public float startDistance = 20;
    private float rotY;
    private float rotX;
    private float moveUp;
    private float distance;

    private void Start()
    {
        rotY= 0f;
        rotX= 10f;
        distance = startDistance;
    }

    private void LateUpdate()
    {
        float horiInput = Input.GetAxis("Horizontal");
        float upInput = Input.GetAxis("Vertical");
        rotY -= Time.deltaTime * horizontalSpeed * horiInput;
        moveUp = Time.deltaTime * upAndDownSpeed * upInput;
        if (Input.GetKey(KeyCode.R))
        {
            rotX += Time.deltaTime * verticalSpeed;
        }
        if (Input.GetKey(KeyCode.F))
        {
            rotX += Time.deltaTime * verticalSpeed * -1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            distance -= Time.deltaTime * zoomSpeed;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            distance += Time.deltaTime * zoomSpeed;
        }

        //limite la distance entre la cam et la cible
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        //limite l'angle de la camera
        rotX = ClampAngle(rotX, angleminVertical, angleMaxVertical);
        Quaternion toRotation = Quaternion.Euler(rotX, rotY, 0);
        Quaternion rotation = toRotation;
    
        //figure out what your distance should be (so that it's rotating around 
        //not just rotating)
        if(targetToRotateAround != null)
        {
            //fait bouger la cible de haut en bas
            Vector3 upAndDownMovement = new Vector3(0f, moveUp, 0f);
            Vector3 upAndDownTranslation = upAndDownMovement + targetToRotateAround.position;
            if (upAndDownTranslation.y >= 0f)
            {
                targetToRotateAround.Translate(upAndDownMovement);
            }
            Vector3 negDistance = new Vector3(0, 0, -distance);
            Vector3 position = rotation * negDistance + targetToRotateAround.position;
            //la camera ne peut pas descendre en dessous de y = 5
            if (position.y < 5)
            {
                position.y = 5;
            }
            //applique la nouvelle pos / rot de la cam
            transform.rotation = rotation;
            transform.position = position;
        }
        else
        {
            Debug.Log("The camera do not have a target");
        }
    }

    //On modifie la cible de la camera
    public void SetTarget(Transform newtarget)
    {
        this.targetToRotateAround = newtarget;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
