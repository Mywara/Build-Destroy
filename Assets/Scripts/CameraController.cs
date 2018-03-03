﻿using System.Collections;
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
    public float minDistance = 5;
    public float maxDistance = 30;
    public float startDistance = 10;
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
        rotY += Time.deltaTime * horizontalSpeed * horiInput;
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

        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        rotX = ClampAngle(rotX, angleminVertical, angleMaxVertical);
        Quaternion toRotation = Quaternion.Euler(rotX, rotY, 0);
        Quaternion rotation = toRotation;
    
        //figure out what your distance should be (so that it's rotating around 
        //not just rotating)
        if(targetToRotateAround != null)
        {
            targetToRotateAround.Translate(new Vector3(0, moveUp, 0));
            Vector3 negDistance = new Vector3(0, 0, -distance);
            Vector3 position = rotation * negDistance + targetToRotateAround.position;
            if (position.y < 0)
            {
                position.y = 0;
            }

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
