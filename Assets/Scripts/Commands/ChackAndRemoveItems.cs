using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ChackAndRemoveItemsCommand: Command
    {
        [Inject]
        public ITetrisModel Model { get; set; }

        [Inject]
        public RemoveLinesSignalDelegate CallbackFunction { set; get; }

        public override void Execute()
        {
            List<List<GameObject>> ListToRemove =  Model.GetItemsToRemove();

            CallbackFunction(ListToRemove);


        }
    }
}
