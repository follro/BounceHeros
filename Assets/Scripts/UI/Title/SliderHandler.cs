using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BounceHeros
{
    public class SliderHandler
    {
        private VisualElement slider;
        private VisualElement dragger;
        private VisualElement bar;

        public SliderHandler(VisualElement slider, VisualElement dragger)
        {
            this.slider = slider;
            this.dragger = dragger;
            AddElements();
        }

        private void AddElements()
        {
            bar = new VisualElement();
            dragger.Add(bar);
        }


    }
}