using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTransformScript : MonoBehaviour {

    public Vector3 averageCameraRotation;
    public Quaternion averageCameraRotationQuat;
    public Vector3 currentCameraRotation;
    public Quaternion currentCameraRotationQuat;
    //List<Vector3> oldCameraRotations;
    Vector3[] oldCameraRotations;
    //Quaternion[] oldCameraRotationsQuat;
    public int maxElementsInList;
    int currentPosition;
    bool fullArray;
    public float maxChange;
    public float maxChangeQuat;
    public string maxChangeIn;
    public string maxChangeInQuat;
    public float changeInX, changeInY, changeInZ;
    public float changeInQuatW, changeInQuatX, changeInQuatY, changeInQuatZ;
    public int amountOfQuat;

    public Vector4 cumulativeForQuat;

    public Vector3 forwardA;
    public Vector3 forwardB;
    public float angleA;
    public float angleB;
    public float angleDiffX;
    public float angleDiffY;
    public float angleDiffZ;

    public float angleForward;

    // Use this for initialization
    void Start () {
        currentCameraRotation = this.transform.rotation.eulerAngles;
        averageCameraRotation = currentCameraRotation;

        currentCameraRotationQuat = this.transform.rotation;
        averageCameraRotationQuat = currentCameraRotationQuat;

        maxElementsInList = 10;
        currentPosition = 0;
        oldCameraRotations = new Vector3[maxElementsInList];
        //oldCameraRotationsQuat = new Quaternion[maxElementsInList];
        fullArray = false;
        amountOfQuat = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (currentPosition >= maxElementsInList)
        {
            currentPosition = 0;
            fullArray = true;
        }

        currentCameraRotation = this.transform.rotation.eulerAngles;
        currentCameraRotationQuat = this.transform.rotation;

        oldCameraRotations[currentPosition] = currentCameraRotation;
        //oldCameraRotationsQuat[currentPosition] = currentCameraRotationQuat;

        currentPosition++;


        /*
        averageCameraRotation = (averageCameraRotation + currentCameraRotation) / 2;
        float[] difference = {  averageCameraRotation.x - currentCameraRotation.x,
                                averageCameraRotation.y - currentCameraRotation.y,
                                averageCameraRotation.z - currentCameraRotation.z };
        maxChange = Mathf.Max(difference);
        */
        //averageCameraRotation = new Vector3();

        // Vector3 approach with Euler axis
        if (fullArray)
        {
            for (int i = 0; i < maxElementsInList; i++)
            {
                averageCameraRotation += oldCameraRotations[i];
            }
            averageCameraRotation /= (oldCameraRotations.Length + 1);
        }

        var dif = Quaternion.Inverse(Quaternion.Euler(averageCameraRotation)) * currentCameraRotationQuat;
        /*float[] difference = {  changeInX = averageCameraRotation.x - currentCameraRotation.x,
                                changeInY = averageCameraRotation.y - currentCameraRotation.y,
                                changeInZ = averageCameraRotation.z - currentCameraRotation.z };*/
        float[] difference = {  changeInX = dif.eulerAngles.x,
                                changeInY = dif.eulerAngles.y,
                                changeInZ = dif.eulerAngles.z };
        maxChange = Mathf.Max(difference);

        if (maxChange == changeInX)
            maxChangeIn = "X";
        else if (maxChange == changeInY)
            maxChangeIn = "Y";
        else
            maxChangeIn = "Z";


        // Alternative -- testing --

        // get a "forward vector" for each rotation
        forwardA = currentCameraRotationQuat * Vector3.forward;
        forwardB = Quaternion.identity * Vector3.forward;

        // get a numeric angle for each vector, on the X-Z plane (relative to world forward)
        angleA = Mathf.Atan2(forwardA.x, forwardA.z) * Mathf.Rad2Deg;
        angleB = Mathf.Atan2(forwardB.x, forwardB.z) * Mathf.Rad2Deg;

        // get the signed difference in these angles
        angleDiffY = Mathf.DeltaAngle(angleA, angleB);


        // get a numeric angle for each vector, on the Y-Z plane (relative to world forward)
        angleA = Mathf.Atan2(forwardA.y, forwardA.z) * Mathf.Rad2Deg;
        angleB = Mathf.Atan2(forwardB.y, forwardB.z) * Mathf.Rad2Deg;

        // get the signed difference in these angles
        angleDiffX = Mathf.DeltaAngle(angleA, angleB);


        // get a numeric angle for each vector, on the X-Y plane (relative to world forward)    // <-- Rotation around Z needs to get fixed for better input values
        angleA = Mathf.Atan2(forwardA.x, forwardA.y) * Mathf.Rad2Deg;                           // <-- Rotation around Z needs to get fixed for better input values
        angleB = Mathf.Atan2(forwardB.x, forwardB.y) * Mathf.Rad2Deg;                           // <-- Rotation around Z needs to get fixed for better input values
                                                                                                // <-- Rotation around Z needs to get fixed for better input values
        // get the signed difference in these angles                                            // <-- Rotation around Z needs to get fixed for better input values
        angleDiffZ = Mathf.DeltaAngle(angleB, angleA) + 90;                                     // <-- Rotation around Z needs to get fixed for better input values




        // Quaternion approach

        averageCameraRotationQuat = CameraTransformScript.AverageQuaternion(ref cumulativeForQuat, currentCameraRotationQuat, averageCameraRotationQuat, ++amountOfQuat);

        float[] differenceQuat = {  changeInQuatW = averageCameraRotationQuat.w - currentCameraRotationQuat.w,
                                    changeInQuatX = averageCameraRotationQuat.x - currentCameraRotationQuat.x,
                                    changeInQuatY = averageCameraRotationQuat.y - currentCameraRotationQuat.y,
                                    changeInQuatZ = averageCameraRotationQuat.z - currentCameraRotationQuat.z };
        maxChangeQuat = Mathf.Max(differenceQuat);

        if (maxChangeQuat == changeInQuatW)
            maxChangeInQuat = "W";
        else if (maxChangeQuat == changeInQuatX)
            maxChangeInQuat = "X";
        else if (maxChangeQuat == changeInQuatY)
            maxChangeInQuat = "Y";
        else
            maxChangeInQuat = "Z";
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
