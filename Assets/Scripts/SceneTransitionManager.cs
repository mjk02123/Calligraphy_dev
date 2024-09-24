using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 씬 전환을 관리하는 클래스
public class SceneTransitionManager : MonoBehaviour
{
    // 페이드 아웃 효과를 관리하는 스크립트 참조
    public FadeScreen fadeScreen;
    // 싱글톤 인스턴스 참조
    public static SceneTransitionManager singleton;

    // 싱글톤 패턴을 적용하여 인스턴스를 초기화
    private void Awake()
    {
        // 다른 인스턴스가 이미 존재하면 현재 인스턴스를 파괴
        if (singleton && singleton != this)
            Destroy(singleton);

        // 현재 인스턴스를 싱글톤으로 설정
        singleton = this;
    }

    // 지정된 씬으로 전환
    public void GoToScene(int sceneIndex)
    {
        // 씬 전환 코루틴 시작
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    // 씬 전환 코루틴
    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        // 페이드 아웃 시작
        fadeScreen.FadeOut();
        // 페이드 아웃 지속 시간만큼 대기
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // 새로운 씬 로드
        SceneManager.LoadScene(sceneIndex);
    }

    // 비동기 씬 전환
    public void GoToSceneAsync(int sceneIndex)
    {
        // 비동기 씬 전환 코루틴 시작
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
    }

    // 비동기 씬 전환 코루틴
    IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        // 페이드 아웃 시작
        fadeScreen.FadeOut();
        // 씬 비동기 로드 시작
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        float timer = 0;
        // 페이드 아웃 지속 시간 동안 대기
        while (timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // 씬 활성화 허용
        operation.allowSceneActivation = true;
    }

    // 씬 1으로 이동
    public void GoToScene1()
    {
        GoToScene(1); // 씬 인덱스 1을 로드
    }
}
