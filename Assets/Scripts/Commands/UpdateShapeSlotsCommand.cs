using strange.extensions.command.impl;
using UnityEngine;

namespace Game
{
    public class UpdateShapeSlotsCommand:Command
    {

        [Inject]
        public ITetrisModel Model { get; set; }

        [Inject]
        public UpdateSlotsSignal Signal { get; set; }

        [Inject]
        public CheckNextMoveSignal CheckNextMoveSignal { get; set; }

        [Inject]
        public GameObject Shape { get; set; }

        public override void Execute()
        {
            if (Model.CheckIsSlotsFree(Shape))
            {
                Signal.Dispatch();
            }

            CheckNextMoveSignal.Dispatch();
        }


    }
}
