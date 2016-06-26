using UnityEngine;
using System.Collections;

public class CollectableItem : MonoBehaviour
{
    public int m_addedScore = 100;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player")
            return;

        PlayerController.m_instance.AddScore((uint)m_addedScore);
        Destroy(gameObject);
    }
}
