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
            Vector3 distanceVector = transform.position - virtualWorld.transform.position;
            distanceVector = Quaternion.Inverse(virtualWorld.VirtualWorldRot) * distanceVector;
            transform.position = virtualWorld.transform.position + distanceVector;

            transform.position -= Quaternion.AngleAxis(virtualWorld.InputValues.rawAngleAroundX, Vector3.right) *
                Quaternion.AngleAxis(virtualWorld.InputValues.rawAngleAroundZ, -Vector3.forward) * virtualWorld.Velocity;
            //transform.position = -virtualWorld.VirtualWorldPos;

            transform.rotation = Quaternion.Inverse(virtualWorld.VirtualWorldRot) * transform.rotation;
        }
    }
}
