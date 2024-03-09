using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class CustomGrabInteractable : XRGrabInteractable
{
    protected XRBaseInteractor grabbingInteractor;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        grabbingInteractor = args.interactorObject as XRBaseInteractor;;
        StartHapticFeedback(grabbingInteractor);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        
        // Calculate throw velocity
        var throwVelocity = args.interactorObject.transform.position - transform.position;
        
        // Apply throw force
        GetComponent<Rigidbody>().AddForce(throwVelocity * throwStrength, ForceMode.Impulse);
        
        StopHapticFeedback(args.interactorObject as XRBaseInteractor);
        grabbingInteractor = null;
        base.OnSelectExited(args);
    }    

    void StartHapticFeedback(XRBaseInteractor interactor)
    {
        if (interactor is XRDirectInteractor)
        {
            var device = interactor.GetComponent<XRController>().inputDevice;
            HapticFeedback(device, 0.5f, 0.5f);
        }
    }

    void StopHapticFeedback(XRBaseInteractor interactor)
    {

        if (interactor is XRDirectInteractor)
        {
            var device = interactor.GetComponent<XRController>().inputDevice;
            // Send a low-intensity, short-duration haptic impulse to signify release
            HapticFeedback(device, 0.1f, 0.1f);        }
    }

    void HapticFeedback(InputDevice device, float amplitude, float duration)
    {
        if (device.isValid)
        {
            HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities) && capabilities.supportsImpulse)
            {
                uint channel = 0;
                device.SendHapticImpulse(channel, amplitude, duration);
            }
        }
    }

    public float throwStrength = 5.0f; // Adjust 
}
