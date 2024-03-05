using UnityEngine;

public class BodyOrientation : MonoBehaviour
{
    public Transform vrHeadset; // Assign this to your VR headset's transform in the inspector

    void Update()
    {
        Vector3 headsetRotation = vrHeadset.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, headsetRotation.y, 0); // Rotate body to match headset orientation on Y-axis
    }
}
