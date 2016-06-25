﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTransformScript : MonoBehaviour {

    public GameObject[] coordinateSystemCheck;

    public Quaternion currentCameraRotationQuat;
    public Vector3 currentCameraPosition;

    Vector3 forwardA;
    Vector3 forwardB;
    float angleA;
    float angleB;

    public float angleForward;
    public float angleRight;
    public float angleYaw;

    public float positionPitch;
    public float positionRoll;
    public float positionUpward;

    public float thresholdForwardP;
    public float thresholdForwardN;
    public float thresholdRightP;
    public float thresholdRightN;
    public float thresholdYawP;
    public float thresholdYawN;
    public float thresholdPitchP;
    public float thresholdPitchN;
    public float thresholdRollP;
    public float thresholdRollN;
    public float thresholdUpwardP;
    public float thresholdUpwardN;

    public float boundaryForwardP;
    public float boundaryForwardN;
    public float boundaryRightP;
    public float boundaryRightN;
    public float boundaryYawP;
    public float boundaryYawN;
    public float boundaryPitchP;
    public float boundaryPitchN;
    public float boundaryRollP;
    public float boundaryRollN;
    public float boundaryUpwardP;
    public float boundaryUpwardN;

    public float maxX, minX;
    public float maxY, minY;
    public float maxZ, minZ;

    public int addAmount = 0;
    public Vector3 addedCameraPositions = Vector3.zero;
    public Vector3 averageCameraPosition;
    //Vector3[] multipleVectors new Vector3[totalAmount];
 


// Use this for initialization
void Start () {
        // Amount of degrees needed to register movement
        thresholdForwardP =  20f;
        thresholdForwardN = -20f;
        thresholdRightP   =  20f;
        thresholdRightN   = -20f;
        thresholdYawP     =  20f;
        thresholdYawN     = -20f;
        thresholdPitchP   =  20f;
        thresholdPitchN   = -20f;
        thresholdRollP    =  20f;
        thresholdRollN    = -20f;
        thresholdUpwardP  =  20f;
        thresholdUpwardN  = -20f;

        // Boundary Degrees capping the resulting values
        boundaryForwardP  =  45f;
        boundaryForwardN  = -45f;
        boundaryRightP    =  45f;
        boundaryRightN    = -45f;
        boundaryYawP      =  45f;
        boundaryYawN      = -45f;
        boundaryPitchP    =  45f;
        boundaryPitchN    = -45f;
        boundaryRollP     =  45f;
        boundaryRollN     = -45f;
        boundaryUpwardP   =  45f;
        boundaryUpwardN   = -45f;

        minX = minY = minZ =  100;
        maxX = maxY = maxZ = -100;
        currentCameraPosition = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        
        float output = angleForward = angleRight = angleYaw = positionPitch = positionRoll = positionUpward = 0;

        if(InputForward(ref output))
        {
            angleForward = output;
        }
        
        if(InputRight(ref output))
        {
            angleRight = output;
        }

        if(InputYaw(ref output))
        {
            angleYaw = output;
        }

        addAmount++;
        addedCameraPositions += currentCameraPosition;
        averageCameraPosition = addedCameraPositions / (float)Mathf.Abs(addAmount);

        maxX = currentCameraPosition.x > maxX ? currentCameraPosition.x : maxX;
        maxY = currentCameraPosition.y > maxY ? currentCameraPosition.y : maxY;
        maxZ = currentCameraPosition.z > maxZ ? currentCameraPosition.z : maxZ;
        minX = currentCameraPosition.x < minX ? currentCameraPosition.x : minX;
        minY = currentCameraPosition.y < minY ? currentCameraPosition.y : minY;
        minZ = currentCameraPosition.z < minZ ? currentCameraPosition.z : minZ;

        // Initializing System Values
        // Calibrate values when entered a certain Button!
        // TODO

        if (Input.GetKeyDown(KeyCode.Space))
        {
            minX = minY = minZ = 100;
            maxX = maxY = maxZ = -100;

            addAmount = 0;
            addedCameraPositions = Vector3.zero;

            foreach (GameObject coordinate in coordinateSystemCheck)
            {
                coordinate.SetActive(true);
            }
        }
        // After a delay of 3 seconds to position oneself at the center:
        // Instructions to hold Device for 3 seconds top, then bottom, then left, then right, then forward, then backwards (close to oneself)
        // Remember these min/max values and set Boundaries for Pitch, Roll and Upward.
        // Threshold being determined by a weighted percentage between boundary and center towards averageCameraPositions
        // For now, just press Backspace to copy the values after calibrating.
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            foreach (GameObject coordinate in coordinateSystemCheck)
            {
                coordinate.SetActive(false);
            }

            float boundaryFactor = 0.9f;  // How close the boundary value should be on the respective min/max values
            float thresholdFactor = 0.3f; // How close the threshold value should be on the average center position

            boundaryPitchP  = (maxZ - averageCameraPosition.z) * boundaryFactor + averageCameraPosition.z;
            boundaryPitchN  = (minZ - averageCameraPosition.z) * boundaryFactor + averageCameraPosition.z;
            boundaryRollP   = (maxX - averageCameraPosition.x) * boundaryFactor + averageCameraPosition.x;
            boundaryRollN   = (minX - averageCameraPosition.x) * boundaryFactor + averageCameraPosition.x;
            boundaryUpwardP = (maxY - averageCameraPosition.y) * boundaryFactor + averageCameraPosition.y;
            boundaryUpwardN = (minY - averageCameraPosition.y) * boundaryFactor + averageCameraPosition.y;

            thresholdPitchP  = (boundaryPitchP - averageCameraPosition.z)  *  thresholdFactor  + averageCameraPosition.z;
            thresholdPitchN  = (boundaryPitchN - averageCameraPosition.z)  *  thresholdFactor  + averageCameraPosition.z;
            thresholdRollP   = (boundaryRollP - averageCameraPosition.x)   *  thresholdFactor  + averageCameraPosition.x;
            thresholdRollN   = (boundaryRollN - averageCameraPosition.x)   *  thresholdFactor  + averageCameraPosition.x;
            thresholdUpwardP = (boundaryUpwardP - averageCameraPosition.y) *  thresholdFactor  + averageCameraPosition.y;
            thresholdUpwardN = (boundaryUpwardN - averageCameraPosition.y) *  thresholdFactor  + averageCameraPosition.y;
        }

        // End TODO

        if (InputPitch(ref output))
        {
            positionPitch = output;
        }

        if(InputRoll(ref output))
        {
            positionRoll = output;
        }

        if(InputUpward(ref output))
        {
            positionUpward = output;
        }
        
    }

    /* -------------------- // Methods for position change // -------------------- */

    bool InputForward(ref float Output)
    {
        currentCameraRotationQuat = this.transform.rotation;

        // get a "forward vector" for each rotation
        forwardA = currentCameraRotationQuat * Vector3.forward;
        forwardB = Quaternion.identity * Vector3.forward;

        // get a numeric angle for each vector, on the Y-Z plane (relative to world forward)
        angleA = Mathf.Atan2(forwardA.y, forwardA.z) * Mathf.Rad2Deg;
        angleB = Mathf.Atan2(forwardB.y, forwardB.z) * Mathf.Rad2Deg;

        // get the signed difference in these angles
        float angleDiffX = Mathf.DeltaAngle(angleA, angleB);

        angleDiffX = Mathf.Clamp(angleDiffX, boundaryForwardN, boundaryForwardP);
        
        if (angleDiffX <= thresholdForwardN)
        {
            //new_value = ( (old_value - old_min) / (old_max - old_min) ) * (new_max - new_min) + new_min
            Output = ((angleDiffX - thresholdForwardN) / (boundaryForwardN - thresholdForwardN)) * (-1 - 0) + 0;
            return true;
        }
        else if(angleDiffX >= thresholdForwardP)
        {
            Output = ((angleDiffX - thresholdForwardP) / (boundaryForwardP - thresholdForwardP)) * ( 1 - 0) + 0;
            return true;
        }

        return false;
    }

    bool InputRight(ref float Output)
    {
        // get a numeric angle for Z vector               
        angleA = transform.rotation.eulerAngles.z < 180 ? -transform.rotation.eulerAngles.z : Mathf.Abs(transform.rotation.eulerAngles.z - 360);
        float angleDiffZ = angleA;

        angleDiffZ = Mathf.Clamp(angleDiffZ, boundaryRightN, boundaryRightP);
        if (angleDiffZ <= thresholdRightN)
        {
            //new_value = ( (old_value - old_min) / (old_max - old_min) ) * (new_max - new_min) + new_min
            Output = ((angleDiffZ - thresholdRightN) / (boundaryRightN - thresholdRightN)) * (-1 - 0) + 0;
            return true;
        }
        else if (angleDiffZ >= thresholdRightP)
        {
            Output = ((angleDiffZ - thresholdRightP) / (boundaryRightP - thresholdRightP)) * ( 1 - 0) + 0;
            return true;
        }

        return false;
    }

    bool InputYaw(ref float Output)
    {
        currentCameraRotationQuat = this.transform.rotation;

        // get a "forward vector" for each rotation
        forwardA = currentCameraRotationQuat * Vector3.forward;
        forwardB = Quaternion.identity * Vector3.forward;

        // get a numeric angle for each vector, on the X-Z plane (relative to world forward)
        angleA = Mathf.Atan2(forwardA.x, forwardA.z) * Mathf.Rad2Deg;
        angleB = Mathf.Atan2(forwardB.x, forwardB.z) * Mathf.Rad2Deg;

        // get the signed difference in these angles
        float angleDiffY = Mathf.DeltaAngle(angleA, angleB);

        angleDiffY = Mathf.Clamp(angleDiffY, boundaryYawN, boundaryYawP);
        if (angleDiffY <= thresholdYawN)
        {
            //new_value = ( (old_value - old_min) / (old_max - old_min) ) * (new_max - new_min) + new_min
            Output = ((angleDiffY - thresholdYawN) / (boundaryYawN - thresholdYawN)) * (-1 - 0) + 0;
            return true;
        }
        else if (angleDiffY >= thresholdYawP)
        {
            Output = ((angleDiffY - thresholdYawP) / (boundaryYawP - thresholdYawP)) * (1 - 0) + 0;
            return true;
        }

        return false;
    }

    /* -------------------- // Methods for rotation change // -------------------- */

    bool InputPitch(ref float Output)
    {
        currentCameraPosition = this.transform.position;
        currentCameraPosition.y *= -1;
        float testingDifference = currentCameraPosition.z; // - averageCameraPosition.z;

        testingDifference = Mathf.Clamp(testingDifference, boundaryPitchN, boundaryPitchP);
        if (testingDifference <= thresholdPitchN)
        {
            //new_value = ( (old_value - old_min) / (old_max - old_min) ) * (new_max - new_min) + new_min
            Output = ((testingDifference - thresholdPitchN) / (boundaryPitchN - thresholdPitchN)) * (-1 - 0) + 0;
            return true;
        }
        else if (testingDifference >= thresholdPitchP)
        {
            Output = ((testingDifference - thresholdPitchP) / (boundaryPitchP - thresholdPitchP)) * ( 1 - 0) + 0;
            return true;
        }

        return false;
    }

    bool InputRoll(ref float Output)
    {
        currentCameraPosition = this.transform.position;
        currentCameraPosition.y *= -1;
        float testingDifference = currentCameraPosition.x; // - averageCameraPosition.x;

        testingDifference = Mathf.Clamp(testingDifference, boundaryRollN, boundaryRollP);
        if (testingDifference <= thresholdRollN)
        {
            //new_value = ( (old_value - old_min) / (old_max - old_min) ) * (new_max - new_min) + new_min
            Output = ((testingDifference - thresholdRollN) / (boundaryRollN - thresholdRollN)) * (-1 - 0) + 0;
            return true;
        }
        else if (testingDifference >= thresholdRollP)
        {
            Output = ((testingDifference - thresholdRollP) / (boundaryRollP - thresholdRollP)) * (1 - 0) + 0;
            return true;
        }

        return false;
    }

    bool InputUpward(ref float Output)
    {
        currentCameraPosition = this.transform.position;
        currentCameraPosition.y *= -1;
        float testingDifference = currentCameraPosition.y; // - averageCameraPosition.y;

        testingDifference = Mathf.Clamp(testingDifference, boundaryUpwardN, boundaryUpwardP);
        if (testingDifference <= thresholdUpwardN)
        {
            //new_value = ( (old_value - old_min) / (old_max - old_min) ) * (new_max - new_min) + new_min
            Output = ((testingDifference - thresholdUpwardN) / (boundaryUpwardN - thresholdUpwardN)) * (-1 - 0) + 0;
            return true;
        }
        else if (testingDifference >= thresholdUpwardP)
        {
            Output = ((testingDifference - thresholdUpwardP) / (boundaryUpwardP - thresholdUpwardP)) * (1 - 0) + 0;
            return true;
        }

        return false;
    }




    //Get an average (mean) from more then two quaternions (with two, slerp would be used).
    //Note: this only works if all the quaternions are relatively close together.
    //Usage: 
    //-Cumulative is an external Vector4 which holds all the added x y z and w components.
    //-newRotation is the next rotation to be added to the average pool
    //-firstRotation is the first quaternion of the array to be averaged
    //-addAmount holds the total amount of quaternions which are currently added
    //This function returns the current average quaternion
    public static Quaternion AverageQuaternion(ref Vector4 cumulative, Quaternion newRotation, Quaternion firstRotation, int addAmount)
    {

        float w = 0.0f;
        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        //Before we add the new rotation to the average (mean), we have to check whether the quaternion has to be inverted. Because
        //q and -q are the same rotation, but cannot be averaged, we have to make sure they are all the same.
        if (!CameraTransformScript.AreQuaternionsClose(newRotation, firstRotation))
        {

            newRotation = CameraTransformScript.InverseSignQuaternion(newRotation);
        }

        //Average the values
        float addDet = 1f / (float)addAmount;
        cumulative.w += newRotation.w;
        w = cumulative.w * addDet;
        cumulative.x += newRotation.x;
        x = cumulative.x * addDet;
        cumulative.y += newRotation.y;
        y = cumulative.y * addDet;
        cumulative.z += newRotation.z;
        z = cumulative.z * addDet;

        //note: if speed is an issue, you can skip the normalization step
        return NormalizeQuaternion(x, y, z, w);
    }

    public static Quaternion NormalizeQuaternion(float x, float y, float z, float w)
    {

        float lengthD = 1.0f / (w * w + x * x + y * y + z * z);
        w *= lengthD;
        x *= lengthD;
        y *= lengthD;
        z *= lengthD;

        return new Quaternion(x, y, z, w);
    }

    //Changes the sign of the quaternion components. This is not the same as the inverse.
    public static Quaternion InverseSignQuaternion(Quaternion q)
    {

        return new Quaternion(-q.x, -q.y, -q.z, -q.w);
    }

    //Returns true if the two input quaternions are close to each other. This can
    //be used to check whether or not one of two quaternions which are supposed to
    //be very similar but has its component signs reversed (q has the same rotation as
    //-q)
    public static bool AreQuaternionsClose(Quaternion q1, Quaternion q2)
    {

        float dot = Quaternion.Dot(q1, q2);

        if (dot < 0.0f)
        {

            return false;
        }

        else
        {

            return true;
        }
    }
}
