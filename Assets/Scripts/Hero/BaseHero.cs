using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BounceHeros
{
    public class BaseHero : MonoBehaviour
    {
        public Rigidbody2D Rigid2D { get; private set; }
        private HeroStateMachine stateMachine;  
        private void Awake()
        {
            Rigid2D = GetComponent<Rigidbody2D>();
            HeroStateMachine stateMachine = new HeroStateMachine(this);
        }

        private void Update() => stateMachine.Update();
        private void FixedUpdate() => stateMachine.FixedUpdate();
        private void LateUpdate() => stateMachine.LateUpdate();
        private void OnCollisionEnter(Collision collision) => stateMachine.StateOnCollisionEnter(collision);

        public void Catched() => stateMachine.TransitionTo(HeroStateMachine.HeroState.Catched); 
        public void Rush() => stateMachine.TransitionTo(HeroStateMachine.HeroState.Rush);



    }
}