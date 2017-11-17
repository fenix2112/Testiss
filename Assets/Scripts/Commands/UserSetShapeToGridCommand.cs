using System;
using System.Collections.Generic;
using System.Linq;
using strange.extensions.command.impl;

namespace Game
{
    public class UserSetShapeToGridCommand:Command
    {

        [Inject]
        public ITetrisModel Model { get; set; }

        [Inject]
        public UserSetShapeSignalDelegate callbackFunction { set; get; }

        [Inject]
        public List<ItemViewVO> list { set; get; }

        public override void Execute()
        {
            bool isAdded = Model.IsNewShapeCanBeAdded(list);

            callbackFunction(isAdded);
        }
       }
}
