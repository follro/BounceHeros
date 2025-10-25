using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.ComponentModel.Design;
using System.Resources;

public class Bootstrapper : MonoBehaviour
{
    public static string sceneToLoadAfterBoot = null;
    private static bool hasInitialized = false;

    [Header("로드할 기본 씬")]
    [SerializeField] private string defaultNextSceneName = "MainMenu";

    private void Awake()
    {
        if (hasInitialized)
        {
            Debug.Log("[Bootstrapper] 중복 Bootstrapper 인스턴스를 파괴합니다.");
            Destroy(gameObject);
            return; 
        }

        hasInitialized = true;
        DontDestroyOnLoad(gameObject); 

        GlobalServiceLocator.Initialize();
        ServiceSetting();
    }

    private async void Start()
    {
        Debug.Log("[Bootstrapper] 모든 전역 서비스 등록 완료. 다음 씬 로드를 시작합니다.");

        await LoadNextSceneAsync();
    }

    private void RegisterMonoBehaviourSystem<TInterface, TImplementation>()
        where TInterface : class
        where TImplementation : MonoBehaviour, TInterface 
    {
        GameObject obj = new GameObject(typeof(TImplementation).Name);
        obj.transform.parent = this.transform;
        TImplementation newSystem = obj.AddComponent<TImplementation>();
        GlobalServiceLocator.Register<TInterface>(newSystem);
    }

    private void RegisterPocoSystem<TInterface, TImplementation>()
        where TInterface : class
        where TImplementation : class, TInterface, new() 
    {
        TImplementation newSystem = new TImplementation();
        GlobalServiceLocator.Register<TInterface>(newSystem);
    }


    private void ServiceSetting()
    {
        RegisterPocoSystem<IAssetLoadManager, AssetLoadManager>();
    }

    private async UniTask LoadNextSceneAsync()
    {
        string sceneToLoad;

        if (!string.IsNullOrEmpty(Bootstrapper.sceneToLoadAfterBoot))
        {
            sceneToLoad = Bootstrapper.sceneToLoadAfterBoot;
            Bootstrapper.sceneToLoadAfterBoot = null;
        }
        else
        {
            sceneToLoad = defaultNextSceneName;
        }

        await SceneManager.LoadSceneAsync(sceneToLoad);

    }
}