using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public interface IInitializable
    {
        bool IsInitialized { get; }
        void Initialize();
    }
}