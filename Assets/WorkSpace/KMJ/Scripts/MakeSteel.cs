using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class MakeSteel : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hammer"))
        {
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }

            TriggerHapticFeedback();
        }
    }

    private void TriggerHapticFeedback()
    {
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        float amplitude = 0.5f; // 진동 강도
        float duration = 0.2f;  // 진동 지속 시간 (초)


        if (rightHand.isValid)
        {
            rightHand.SendHapticImpulse(0, amplitude, duration);
        }
        /*
        if (leftHand.isValid)
        {
            leftHand.SendHapticImpulse(0, amplitude, duration);
        }*/
    }
}
