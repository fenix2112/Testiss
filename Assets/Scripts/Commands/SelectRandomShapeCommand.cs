using UnityEngine;
using strange.extensions.command.impl;

namespace Game
{
    class SelectRandomShapeCommand:Command
    {
        [Inject]
        public ITetrisModel Model { get; set; }

        [Inject]
        public SelectRandomShapeSignalDelegate CallbackFunction { set; get; }

        public override void Execute()
        {
            GameObject gameObject =  Model.SelectRandomShape();
            CallbackFunction(gameObject);
        }
    }
}
