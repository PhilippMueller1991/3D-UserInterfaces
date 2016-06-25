using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTransformScript : MonoBehaviour {

    public Quaternion currentCameraRotationQuat;

    public Vector3 forwardA;
    public Vector3 forwardB;
    public float angleA;
    public float angleB;
    public float angleDiffX;
    public float angleDiffY;
    public float angleDiffZ;
    public float positionX;
    public float positionY;
    public float positionZ;

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

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        float output = 0;
        InputForward(ref output);

        InputRight(ref output);

        InputYaw(ref output);
        
    }

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
        angleDiffX = Mathf.DeltaAngle(angleA, angleB);

        
        if(angleDiffX < 0 && angleDiffX <= thresholdForwardN)
        {

        }

        return false;
    }

    bool InputRight(ref float Output)
    {
        currentCameraRotationQuat = this.transform.rotation;

        // get a "forward vector" for each rotation
        forwardA = currentCameraRotationQuat * Vector3.forward;
        forwardB = Quaternion.identity * Vector3.forward;

        // get a numeric angle for each vector, on the X-Z plane (relative to world forward)
        angleA = Mathf.Atan2(forwardA.x, forwardA.z) * Mathf.Rad2Deg;
        angleB = Mathf.Atan2(forwardB.x, forwardB.z) * Mathf.Rad2Deg;

        // get the signed difference in these angles
        angleDiffY = Mathf.DeltaAngle(angleA, angleB);

        return false;
    }

    bool InputYaw(ref float Output)
    {
        // get a numeric angle for Z vector               
        angleA = transform.rotation.eulerAngles.z < 180 ? -transform.rotation.eulerAngles.z : Mathf.Abs(transform.rotation.eulerAngles.z - 360);
        angleDiffZ = angleA;

        return false;
    }

    bool InputPitch(ref float Output)
    {
        return false;
    }

    bool InputRoll(ref float Output)
    {
        return false;
    }

    bool InputUpward(ref float Output)
    {
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
