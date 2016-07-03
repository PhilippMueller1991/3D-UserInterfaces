using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    public float m_resetDistance = 0.001f;
    public int m_damage = 10;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player")
            return;

        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        player.LoseLife((uint)m_damage);
    }
}
