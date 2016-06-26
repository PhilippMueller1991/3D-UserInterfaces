using UnityEngine;
using System.Collections;

public class SpaceshipController : MonoBehaviour
{
    public float m_speed = 1.0f;
    public float m_maxSpeed = 200.0f; // TODO

    private Rigidbody m_rigidbody;


    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

	void Update ()
    {
        // TODO 
        // weight forward acceleration and backward acceleration different
        // use different speed for turning 
        if (Input.GetAxis("Horizontal") != 0)
            m_rigidbody.AddTorque(m_speed * Input.GetAxis("Horizontal") * -transform.forward);
        if (Input.GetAxis("Vertical") != 0)
            m_rigidbody.AddForce(m_speed * Input.GetAxis("Vertical") * -transform.up);
    }
    
    void OnCollisionEnter(Collision other)
    {
        Debug.DrawLine(gameObject.transform.position, other.transform.position, Color.red, 3.0f);
    }

    // TODO: for later use asa AR marker works
    public void Move(Vector3 dir)
    {
        m_rigidbody.AddForce(m_speed * dir);
    }

    public void Turn(Vector3 dir)
    {
        m_rigidbody.AddTorque(m_speed * dir);
    }
}
