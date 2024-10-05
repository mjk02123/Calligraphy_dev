//using UnityEngine;
//using UnityEngine.UI;

//public class MyAudioManager : MonoBehaviour
//{
//    public AudioClip[] audioClips; // 재생할 오디오 파일 배열
//    public Button nextButton; // UI 버튼
//    public GameObject portalBlueObject; // Portal Blue 오브젝트
//    private int currentClipIndex = 0; // 현재 재생 중인 오디오의 인덱스
//    private GameObject audioSourceContainer; // 오디오 소스가 포함된 컨테이너
//    private int buttonPressCount = 0; // 버튼 누름 횟수
//    public Whiteboard whiteboard; // Whiteboard 스크립트 참조

//    void Start()
//    {
//        // 오디오 소스를 담을 빈 게임 오브젝트 생성
//        audioSourceContainer = new GameObject("AudioSourceContainer");
//        audioSourceContainer.transform.SetParent(transform);

//        if (nextButton != null)
//        {
//            nextButton.onClick.AddListener(OnNextButtonClick);
//        }

//        // Portal Blue 오브젝트를 비활성화
//        if (portalBlueObject != null)
//        {
//            portalBlueObject.SetActive(false);
//        }
//    }

//    void OnNextButtonClick()
//    {
//        PlayNextAudio();
//        buttonPressCount++;

//        // 세 번째 버튼 클릭 시 Portal Blue 오브젝트 활성화
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

//        // 기존 오디오 소스가 있으면 삭제
//        foreach (Transform child in audioSourceContainer.transform)
//        {
//            Destroy(child.gameObject);
//        }

//        // 새로운 오디오 소스 생성
//        GameObject newAudioSourceObject = new GameObject("AudioSource_" + currentClipIndex);
//        newAudioSourceObject.transform.SetParent(audioSourceContainer.transform);
//        AudioSource audioSource = newAudioSourceObject.AddComponent<AudioSource>();

//        // 오디오 클립 설정 및 재생
//        audioSource.clip = audioClips[currentClipIndex];
//        audioSource.Play();

//        // 인덱스 증가 및 배열 길이 초과 시 초기화
//        currentClipIndex++;
//        if (currentClipIndex >= audioClips.Length)
//        {
//            currentClipIndex = 0;
//        }
//    }
//}
