using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTransformScript : MonoBehaviour {

    public Vector3 averageCameraRotation;
    public Vector3 currentCameraRotation;
    //List<Vector3> oldCameraRotations;
    Vector3[] oldCameraRotations;
    public int maxElementsInList;
    int currentPosition;
    bool fullArray;
    public float maxChange;

	// Use this for initialization
	void Start () {
        currentCameraRotation = this.transform.rotation.eulerAngles;
        averageCameraRotation = currentCameraRotation;
        maxElementsInList = 10;
        currentPosition = 0;
        oldCameraRotations = new Vector3[maxElementsInList];
        fullArray = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (currentPosition >= maxElementsInList)
        {
            currentPosition = 0;
            fullArray = true;
        }

        currentCameraRotation = this.transform.rotation.eulerAngles;

        oldCameraRotations[currentPosition] = currentCameraRotation;
        currentPosition++;


        /*
        averageCameraRotation = (averageCameraRotation + currentCameraRotation) / 2;
        float[] difference = {  averageCameraRotation.x - currentCameraRotation.x,
                                averageCameraRotation.y - currentCameraRotation.y,
                                averageCameraRotation.z - currentCameraRotation.z };
        maxChange = Mathf.Max(difference);
        */
        //averageCameraRotation = new Vector3();
        if (fullArray)
        {
            for (int i = 0; i < oldCameraRotations.Length; i++)
            {
                averageCameraRotation += oldCameraRotations[i];
            }
            averageCameraRotation /= (oldCameraRotations.Length + 1);
        }

        float[] difference = {  averageCameraRotation.x - currentCameraRotation.x,
                                averageCameraRotation.y - currentCameraRotation.y,
                                averageCameraRotation.z - currentCameraRotation.z };
        maxChange = Mathf.Max(difference);
    }
}
