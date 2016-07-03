using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{
    public float m_resetDistance = 0.001f;
    public int m_damage = 10;
    public bool m_isKinematic = true;

    private Rigidbody m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.isKinematic = m_isKinematic;
        m_rb.useGravity = false;
        m_rb.mass = 1.0f * gameObject.transform.lossyScale.magnitude;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player")
            return;

        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        player.LoseLife((uint)m_damage);
    }
}
