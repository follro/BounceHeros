using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Bootstrapper : MonoBehaviour
{
    public abstract UniTask BindingObjects();
    public abstract UniTask Initialize();
    public abstract UniTask InjectDependencies();

    protected void Start()
    {
        this.gameObject.name = $"[{this.GetType().Name}]";
    }

}

