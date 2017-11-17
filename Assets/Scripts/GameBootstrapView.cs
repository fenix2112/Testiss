using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.impl;

namespace Game
{
    public class GameBootstrapView : ContextView
    {
        void Awake()
        {
            this.context = new TetrisContext(this);
        }
    }
}

