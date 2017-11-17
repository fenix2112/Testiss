using strange.extensions.command.impl;
using UnityEngine;
namespace Game
{
    public class CheckNewMoveCommand: Command
    {
        [Inject]
        public ITetrisModel Model { get; set; }

        [Inject]
        public EndOfGameSignal EndOfGameSignal { get; set; }

        public override void Execute()
        {
            bool isMovePossible = Model.IsNextMovePossible();
          

            if (!isMovePossible)
            {
                EndOfGameSignal.Dispatch();
                Debug.Log("EndOfGameSignal");
            }
        }
    }
}
