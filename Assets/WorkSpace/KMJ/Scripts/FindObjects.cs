using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(AudioSource))]
public class FindObjects : MonoBehaviour
{
    private AudioSource audioSource;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // 디버깅: AudioSource가 Play On Awake인지 확인
        Debug.Log($"AudioSource Play On Awake: {audioSource.playOnAwake}");

        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // 디버깅: OnGrab 함수가 호출될 때 로그 출력
        Debug.Log("OnGrab triggered!");

        if (audioSource != null && audioSource.clip != null)
        {
            Debug.Log("AudioSource is playing!");
            audioSource.Play();
        }
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
    }
}
