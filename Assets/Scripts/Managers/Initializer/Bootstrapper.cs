using UnityEngine;

public abstract class Bootstrapper : MonoBehaviour
{
    public abstract void Initialize();
    public virtual void DependencyInjection() { }

    protected virtual void Awake()
    {
        this.gameObject.name = $"[{this.GetType().Name}]";
        Initialize();
    }

    protected virtual void Start()
    {
        DependencyInjection();
    }

}

