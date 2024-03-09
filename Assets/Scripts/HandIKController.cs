using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandIKController : MonoBehaviour
{
    public XRBaseController leftHandController;
    public XRBaseController rightHandController;

    public Transform leftIKTarget;
    public Transform rightIKTarget;

    public float armLengthRatio = 1.5f; // Adjust this ratio based on the arm length difference

    // Non-linear scaling factors
    public float nearDistanceThreshold = 0.5f; // Distance threshold for slower movement
    public float farDistanceMultiplier = 2.0f; // Multiplier for movement when far away

    private void Update()
    {
        if (leftHandController && leftIKTarget)
        {
            Vector3 leftHandPosition = leftHandController.transform.position;
            leftIKTarget.position = ScalePositionNonLinearly(leftHandPosition);
        }

        if (rightHandController && rightIKTarget)
        {
            Vector3 rightHandPosition = rightHandController.transform.position;
            rightIKTarget.position = ScalePositionNonLinearly(rightHandPosition);
        }
    }

    private Vector3 ScalePositionNonLinearly(Vector3 originalPosition)
    {
        Vector3 basePosition = this.transform.position; // Adjust if your base position is different
        Vector3 direction = (originalPosition - basePosition).normalized;
        float distance = Vector3.Distance(originalPosition, basePosition);

        // Apply non-linear scaling based on distance
        if (distance <= nearDistanceThreshold)
        {
            // Closer to the base, move slower
           // distance *= (1f + (distance / nearDistanceThreshold) * (farDistanceMultiplier - 1f));
        }
        else
        {
            // Farther from the base, increase the movement speed
            distance *= farDistanceMultiplier;
        }

        Vector3 scaledPosition = basePosition + direction * distance * armLengthRatio;

        return scaledPosition;
    }
}
