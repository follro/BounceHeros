using System;
using System.Collections;
using System.Collections.Generic;

namespace HakSeung.Util
{

    public class StateEnumArray<TState, TEnum>
        where TState : IState
        where TEnum : struct, Enum 
    {
        private readonly TState[] states;

        public TState this[TEnum key]
        {
            get => states[Convert.ToInt32(key)];
            set => states[Convert.ToInt32(key)] = value;
        }

        public TState this[int key]
        {
            get => states[key];
            set => states[key] = value;
        }

        public StateEnumArray()
        {
            int size = Enum.GetValues(typeof(TEnum)).Length;
            states = new TState[size];
        }

        public StateEnumArray(int size)
        {
            states = new TState[size];
        }
    }
}