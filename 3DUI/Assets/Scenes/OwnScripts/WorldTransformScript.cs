﻿using UnityEngine;
using System.Collections;

public class WorldTransformScript : MonoBehaviour {

    public static WorldTransformScript m_instance = null;  // pseudo singleton
    public SpaceshipMovementScript virtualWorld;

    Quaternion previousOrientation;

    void Awake()
    {
        if (m_instance == null)
            m_instance = this;
        else if (m_instance != this)
            Destroy(this);
    }

    void Start () {
        previousOrientation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (virtualWorld.StartTracking)
        {
            Vector3 shipPos = virtualWorld.transform.position; // + virtualWorld.transform.up * 1f;
            Vector3 distanceVector = transform.position - shipPos;
            distanceVector = Quaternion.Inverse(virtualWorld.VirtualWorldRot) * distanceVector;
            transform.position = distanceVector + shipPos;

            transform.position -= Quaternion.AngleAxis(virtualWorld.InputValues.rawAngleAroundX, Vector3.right) *
                Quaternion.AngleAxis(virtualWorld.InputValues.rawAngleAroundZ, -Vector3.forward) * virtualWorld.Velocity;
            //transform.position = -virtualWorld.VirtualWorldPos;


            transform.rotation = Quaternion.Inverse(virtualWorld.VirtualWorldRot) * (transform.rotation);
            //Quaternion debugquat = Quaternion.Inverse(virtualWorld.VirtualWorldRot) * (transform.rotation);
            //transform.RotateAround(virtualWorld.transform.position, Vector3.forward, debugquat.eulerAngles.z);
            //transform.RotateAround(virtualWorld.transform.position, Vector3.right, debugquat.eulerAngles.x);
            //transform.RotateAround(virtualWorld.transform.position, Vector3.up, debugquat.eulerAngles.y);

            Vector3 debugline = transform.rotation * Vector3.forward;
            Debug.DrawLine(Vector3.zero, debugline, Color.green);
            Debug.DrawLine(virtualWorld.transform.position, distanceVector, Color.yellow);

            PlayerController.m_instance.StartTimer();
        }
    }
}
