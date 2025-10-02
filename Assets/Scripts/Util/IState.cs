using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HakSeung.Util
{
    public interface IState
    {
        public void Enter();
        public void Update();
        public void Exit();

    }
}
