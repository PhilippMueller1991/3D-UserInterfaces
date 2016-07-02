using UnityEngine;
using System.Collections;

public class SpaceshipMovementScript : MonoBehaviour
{

    public CameraTransformScript InputValues;

    public float oldForward;
    public float oldRight;
    public float oldYaw;
    public float oldPitch;
    public float oldRoll;
    public float oldUpward;

    float alphaBlend;

    float speedPos;
    float speedRot;
    float maxVelocity;
    Vector3 velocity;

    Vector3 startLocalPosition;
    Quaternion startLocalRotation;

    Vector3 virtualWorldPos;
    Quaternion virtualWorldRot;

    public bool startTracking;

    // Use this for initialization
    void Start()
    {
        oldForward = oldRight = oldYaw = oldPitch = oldRoll = oldUpward = 0;
        startLocalPosition = transform.localPosition;
        startLocalRotation = transform.localRotation;

        alphaBlend = 0.025f;

        virtualWorldPos = Vector3.zero;
        virtualWorldRot = Quaternion.identity;

        speedPos = 100;
        speedRot = 100;

        startTracking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(startTracking)
        { 
            Vector3 addPos = new Vector3(InputValues.GetRight(), InputValues.GetUpward(), InputValues.GetForward());
            Vector3 addRot = new Vector3(InputValues.GetPitch(), InputValues.GetYaw(), InputValues.GetRoll());

            if (addPos.magnitude >= 1)
                addPos.Normalize();
            if (addRot.magnitude >= 1)
                addRot.Normalize();

            addPos *= speedPos * Time.deltaTime;
            addRot *= speedRot * Time.deltaTime;

            virtualWorldPos += addPos;
            virtualWorldRot = Quaternion.Euler(addRot) * virtualWorldRot;

            oldForward  = InputValues.GetForward()  * alphaBlend + oldForward   * (1 - alphaBlend);
            oldRight    = InputValues.GetRight()    * alphaBlend + oldRight     * (1 - alphaBlend);
            oldYaw      = InputValues.GetYaw()      * alphaBlend + oldYaw       * (1 - alphaBlend);
            oldPitch    = InputValues.GetPitch()    * alphaBlend + oldPitch     * (1 - alphaBlend);
            oldRoll     = InputValues.GetRoll()     * alphaBlend + oldRoll      * (1 - alphaBlend);
            oldUpward   = InputValues.GetUpward()   * alphaBlend + oldUpward    * (1 - alphaBlend);

            //new_value = ( (old_value  - old_min)  / (old_max - old_min) ) * (new_max - new_min)   + new_min
            float posZ  = ((oldForward  - (-1f))    /       (1f - (-1f)))   *   (0.49f - 0.39f)     + 0.39f;    // PosZ
            float posY  = ((oldUpward   - (-1f))    /       (1f - (-1f)))   *   (0f  - (-0.16f))    + (-0.16f); // PosY
            float posX  = ((oldRight    - (-1f))    /       (1f - (-1f)))   *   (0.1f - (-0.1f))    + (-0.1f);  // PosX

            transform.localPosition = new Vector3(posX, posY, posZ);

            float rotX  = ((oldPitch    - (-1f))    /       (1f - (-1f)))   *   (10f - (-10f))      + (-10f);   // RotX
            float rotZ  = ((oldRoll     - (-1f))    /       (1f - (-1f)))   *   (20f - (-20f))      + (-20f);   // RotZ
            float rotY  = ((oldYaw      - (-1f))    /       (1f - (-1f)))   *   (20f - (-20f))      + (-20f);   // RotY

            Quaternion rotation = Quaternion.AngleAxis(rotX, Vector3.right) * Quaternion.AngleAxis(-rotZ, Vector3.forward) * Quaternion.AngleAxis(rotY, Vector3.up);
            transform.localRotation = rotation * startLocalRotation;
            //transform.localEulerAngles = new Vector3(oldPitch, oldRoll, oldYaw);
            //transform.localEulerAngles = new Vector3(0, 0, 1f*Time.deltaTime);
        }
    }

    public bool StartTracking
    {
        get { return startTracking; }
        set { startTracking = value; }
    }
}