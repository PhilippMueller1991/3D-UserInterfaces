using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float m_speed = 2.0f;

	void Start ()
    {
	
	}
	
	void Update ()
    {
        if (Input.GetAxis("Vertical") != 0)
            transform.position = Vector3.Lerp(transform.position, transform.position + Input.GetAxis("Vertical") * transform.forward, Time.deltaTime * m_speed);

        Debug.DrawLine(transform.position, transform.position + Input.GetAxis("Vertical") * transform.forward, Color.red);
	}

    void FixedUpdate()
    {

    }
}
