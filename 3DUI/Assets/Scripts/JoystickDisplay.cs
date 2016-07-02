using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JoystickDisplay : MonoBehaviour {

    public CameraTransformScript m_ar;

    public Image m_rotateX;
    public Image m_rotateY;
    public Image m_rotateZ;
    
    public Image m_movementUp;
    public Image m_movementDown;
    public Image m_movementLeft;
    public Image m_movementRight;
    public Image m_movementForward;
    public Image m_movementBackward;


    void Start()
    {
        Color c;
        c = m_rotateX.color;
        c.a = 0.2f;
        m_rotateX.color = c;
        m_rotateY.color = c;
        m_rotateZ.color = c;

        c = m_movementUp.color;
        c.a = 0.2f;
        m_movementUp.color      = c;
        m_movementDown.color    = c;
        m_movementLeft.color    = c;
        m_movementRight.color   = c;
        m_movementForward.color = c;
        m_movementBackward.color= c;
    }

    public void DisplayRotation(Vector3 rot)
    {
        UpdateRotationImage(m_rotateX, rot.x);
        UpdateRotationImage(m_rotateY, rot.y);
        UpdateRotationImage(m_rotateZ, rot.z);
    }

    public void DisplayMovement(Vector3 movement)
    {
        UpdateMovementImages(m_movementRight, m_movementLeft, movement.x);
        UpdateMovementImages(m_movementUp, m_movementDown, movement.y);
        UpdateMovementImages(m_movementForward, m_movementBackward, movement.z);
    }

    void Update()
    {
        //DisplayRotation(new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Vertical"), Input.GetAxis("Vertical")));
        //DisplayMovement(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetKey(KeyCode.Q) ? -1 : Input.GetKey(KeyCode.E) ? 1 : 0));
        
        Vector3 rot = new Vector3(m_ar.GetYaw(), m_ar.GetPitch(), m_ar.GetRoll());
        Vector3 movement = new Vector3(m_ar.GetRight(), m_ar.GetUpward(), m_ar.GetForward());
        DisplayRotation(rot);
        DisplayMovement(movement);
    }

    // aux methods
    private void UpdateRotationImage(Image img, float axisAngle)
    {
        Color c;
        if (axisAngle > 0.1f)
        {
            img.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            c = img.color;
            c.a = 1.0f;
            img.color = c;
        }
        else if (axisAngle < -0.1f)
        {
            img.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            c = img.color;
            c.a = 1.0f;
            img.color = c;
        }
        else
        {
            c = img.color;
            c.a = 0.2f;
            img.color = c;
        }
    }

    private void UpdateMovementImages(Image img1, Image img2, float axis)
    {
        Color c;
        if (axis > 0.1f)
        {
            c = img1.color;
            c.a = 1.0f;
            img1.color = c;

            c = img2.color;
            c.a = 0.2f;
            img2.color = c;
        }
        else if (axis < -0.1f)
        {
            c = img1.color;
            c.a = 0.2f;
            img1.color = c;

            c = img2.color;
            c.a = 1.0f;
            img2.color = c;
        }
        else
        {
            c = img1.color;
            c.a = 0.2f;
            img1.color = c;

            c = img2.color;
            c.a = 0.2f;
            img2.color = c;
        }
    }
}
