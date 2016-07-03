using UnityEngine;
using System.Collections;

public class WorldTransformScript : MonoBehaviour {

    public SpaceshipMovementScript virtualWorld;

    Quaternion previousOrientation;

	// Use this for initialization
	void Start () {
        previousOrientation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (virtualWorld.StartTracking)
        {
            transform.rotation = Quaternion.Inverse(virtualWorld.VirtualWorldRot) * transform.rotation;

            Vector3 distanceVector = transform.position - virtualWorld.transform.position;
            distanceVector = Quaternion.Inverse(virtualWorld.VirtualWorldRot) * distanceVector;
            transform.position = virtualWorld.transform.position + distanceVector;

            //Quaternion adjustment = Quaternion.Inverse(Quaternion.Inverse(previousOrientation) * virtualWorld.VirtualWorldRot);

            transform.position -= virtualWorld.Velocity;
        }
    }
}
