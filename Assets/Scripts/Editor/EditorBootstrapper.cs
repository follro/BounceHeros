#if UNITY_EDITOR // 이 스크립트 전체는 Unity 에디터에서만 컴파일됩니다.

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class EditorBootstrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void CheckBootScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            return;
        }

        Debug.LogWarning("EDITOR PLAY: Boot 씬(0번)이 아닌 곳에서 시작했습니다. 강제로 Boot 씬을 로드합니다.");

        string scenePath = SceneManager.GetActiveScene().path;
        Bootstrapper.sceneToLoadAfterBoot = scenePath;

        SceneManager.LoadScene(0);
    }
}

#endif