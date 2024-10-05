//using UnityEngine;
//using UnityEngine.UI;

//public class MyAudioManager : MonoBehaviour
//{
//    public AudioClip[] audioClips; // ����� ����� ���� �迭
//    public Button nextButton; // UI ��ư
//    public GameObject portalBlueObject; // Portal Blue ������Ʈ
//    private int currentClipIndex = 0; // ���� ��� ���� ������� �ε���
//    private GameObject audioSourceContainer; // ����� �ҽ��� ���Ե� �����̳�
//    private int buttonPressCount = 0; // ��ư ���� Ƚ��
//    public Whiteboard whiteboard; // Whiteboard ��ũ��Ʈ ����

//    void Start()
//    {
//        // ����� �ҽ��� ���� �� ���� ������Ʈ ����
//        audioSourceContainer = new GameObject("AudioSourceContainer");
//        audioSourceContainer.transform.SetParent(transform);

//        if (nextButton != null)
//        {
//            nextButton.onClick.AddListener(OnNextButtonClick);
//        }

//        // Portal Blue ������Ʈ�� ��Ȱ��ȭ
//        if (portalBlueObject != null)
//        {
//            portalBlueObject.SetActive(false);
//        }
//    }

//    void OnNextButtonClick()
//    {
//        PlayNextAudio();
//        buttonPressCount++;

//        // �� ��° ��ư Ŭ�� �� Portal Blue ������Ʈ Ȱ��ȭ
//        if (buttonPressCount == 3)
//        {
//            if (portalBlueObject != null)
//            {
//                portalBlueObject.SetActive(true);
//            }
//            if (whiteboard != null)
//            {
//                whiteboard.ClearWhiteboard();
//            }
//        }
//    }

//    void PlayNextAudio()
//    {
//        if (audioClips.Length == 0)
//        {
//            Debug.LogWarning("No audio clips assigned.");
//            return;
//        }

//        // ���� ����� �ҽ��� ������ ����
//        foreach (Transform child in audioSourceContainer.transform)
//        {
//            Destroy(child.gameObject);
//        }

//        // ���ο� ����� �ҽ� ����
//        GameObject newAudioSourceObject = new GameObject("AudioSource_" + currentClipIndex);
//        newAudioSourceObject.transform.SetParent(audioSourceContainer.transform);
//        AudioSource audioSource = newAudioSourceObject.AddComponent<AudioSource>();

//        // ����� Ŭ�� ���� �� ���
//        audioSource.clip = audioClips[currentClipIndex];
//        audioSource.Play();

//        // �ε��� ���� �� �迭 ���� �ʰ� �� �ʱ�ȭ
//        currentClipIndex++;
//        if (currentClipIndex >= audioClips.Length)
//        {
//            currentClipIndex = 0;
//        }
//    }
//}
