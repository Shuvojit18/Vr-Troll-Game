using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform ikTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;
    public void Map()
    {
        ikTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        ikTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class IKTargetFollowVRRig : MonoBehaviour
{
    [Range(0,1)]
    public float turnSmoothness = 0.1f;
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Vector3 headBodyPositionOffset;
    public float headBodyYawOffset;

    //     // New fields to control body rotation independently
    public Transform bodyTransform; // Assign a dedicated transform for the body
    public bool rotateBodyWithHead = false; // Control whether body should rotate with head

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = head.ikTarget.position + headBodyPositionOffset;
        float yaw = head.vrTarget.eulerAngles.y;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, yaw, transform.eulerAngles.z),turnSmoothness);

        head.Map();
        leftHand.Map();
        rightHand.Map();

        // Body position follows head with offset
        // if (bodyTransform != null)
        // {
        //     bodyTransform.position = head.ikTarget.position + headBodyPositionOffset;

        //     // Optionally rotate body with head
        //     if (rotateBodyWithHead)
        //     {
        //         float yaw = head.vrTarget.eulerAngles.y;
        //         bodyTransform.rotation = Quaternion.Lerp(bodyTransform.rotation, Quaternion.Euler(bodyTransform.eulerAngles.x, yaw, bodyTransform.eulerAngles.z), turnSmoothness);
        //     }
        // }
    }
}

// using UnityEngine;

// [System.Serializable]
// public class VRMap
// {
//     public Transform vrTarget;
//     public Transform ikTarget;
//     public Vector3 trackingPositionOffset;
//     public Vector3 trackingRotationOffset;

//     public void Map()
//     {
//         ikTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
//         ikTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
//     }
// }

// public class IKTargetFollowVRRig : MonoBehaviour
// {
//     [Range(0, 1)]
//     public float turnSmoothness = 0.1f;
//     public VRMap head;
//     public VRMap leftHand;
//     public VRMap rightHand;

//     public Vector3 headBodyPositionOffset;

//     // New fields to control body rotation independently
//     public Transform bodyTransform; // Assign a dedicated transform for the body
//     public bool rotateBodyWithHead = false; // Control whether body should rotate with head

//     void LateUpdate()
//     {
//         // Update head and hands positions
//         head.Map();
//         leftHand.Map();
//         rightHand.Map();

//         // Body position follows head with offset
//         if (bodyTransform != null)
//         {
//             bodyTransform.position = head.ikTarget.position + headBodyPositionOffset;

//             // Optionally rotate body with head
//             if (rotateBodyWithHead)
//             {
//                 float yaw = head.vrTarget.eulerAngles.y;
//                 bodyTransform.rotation = Quaternion.Lerp(bodyTransform.rotation, Quaternion.Euler(bodyTransform.eulerAngles.x, yaw, bodyTransform.eulerAngles.z), turnSmoothness);
//             }
//         }
//     }
// }
