using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public interface ISpawner 
    {
        void StartSpawning();
        void StopSpawning();
        void ClearAll();

    }
}