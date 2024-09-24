using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �� ��ȯ�� �����ϴ� Ŭ����
public class SceneTransitionManager : MonoBehaviour
{
    // ���̵� �ƿ� ȿ���� �����ϴ� ��ũ��Ʈ ����
    public FadeScreen fadeScreen;
    // �̱��� �ν��Ͻ� ����
    public static SceneTransitionManager singleton;

    // �̱��� ������ �����Ͽ� �ν��Ͻ��� �ʱ�ȭ
    private void Awake()
    {
        // �ٸ� �ν��Ͻ��� �̹� �����ϸ� ���� �ν��Ͻ��� �ı�
        if (singleton && singleton != this)
            Destroy(singleton);

        // ���� �ν��Ͻ��� �̱������� ����
        singleton = this;
    }

    // ������ ������ ��ȯ
    public void GoToScene(int sceneIndex)
    {
        // �� ��ȯ �ڷ�ƾ ����
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    // �� ��ȯ �ڷ�ƾ
    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        // ���̵� �ƿ� ����
        fadeScreen.FadeOut();
        // ���̵� �ƿ� ���� �ð���ŭ ���
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // ���ο� �� �ε�
        SceneManager.LoadScene(sceneIndex);
    }

    // �񵿱� �� ��ȯ
    public void GoToSceneAsync(int sceneIndex)
    {
        // �񵿱� �� ��ȯ �ڷ�ƾ ����
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
    }

    // �񵿱� �� ��ȯ �ڷ�ƾ
    IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        // ���̵� �ƿ� ����
        fadeScreen.FadeOut();
        // �� �񵿱� �ε� ����
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        float timer = 0;
        // ���̵� �ƿ� ���� �ð� ���� ���
        while (timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // �� Ȱ��ȭ ���
        operation.allowSceneActivation = true;
    }

    // �� 1���� �̵�
    public void GoToScene1()
    {
        GoToScene(1); // �� �ε��� 1�� �ε�
    }
}
