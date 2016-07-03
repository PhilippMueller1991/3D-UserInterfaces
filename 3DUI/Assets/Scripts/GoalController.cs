using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class GoalController : MonoBehaviour {

	void Start ()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        Collider coll = GetComponent<Collider>();
        coll.isTrigger = true;
	}

    void OnTriggerEnter(Collision other)
    {
        if (other.gameObject.tag != "Player")
            return;

        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        player.StopTimer();
    }
}
