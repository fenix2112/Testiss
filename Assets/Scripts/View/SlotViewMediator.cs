using strange.extensions.mediation.impl;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public delegate void UserSetShapeSignalDelegate(bool isAdded);
    public delegate void RemoveLinesSignalDelegate(List<List<GameObject>> list);
    public delegate void SelectRandomShapeSignalDelegate(GameObject gameObject);

    public class SlotViewMediator : EventMediator
    {
        [Inject]
        public SlotView View { get; set; }

        [Inject]
        public ShapeSlotIsEmptySignal<GameObject> ShapeSlotIsEmptySignal { get; set; }

        [Inject]
        public UpdateSlotsSignal UpdateSlotsSignal { get; set; }

        [Inject]
        public ChackAndRemoveItemsSignal<RemoveLinesSignalDelegate> ChackAndRemoveItemsSignal { get; set; }

       

        [Inject]
        public UserSetShapeToGridSignal<List<ItemViewVO>, UserSetShapeSignalDelegate> UserSetShapeSignal { get; set; }

        [Inject]
        public SelectRandomShapeSignal<SelectRandomShapeSignalDelegate> SelectRandomShapeSignal { get; set; }

        UserSetShapeSignalDelegate SetShapeSignalCallback;
        RemoveLinesSignalDelegate RemoveLinesSignalCallback;
        SelectRandomShapeSignalDelegate SelectRandomShapeSignalCallback;

        public override void OnRegister()
        {
            base.OnRegister();

            SetShapeSignalCallback = UserSetShapeCallback;
            RemoveLinesSignalCallback = RemoveLinesCallback;
            SelectRandomShapeSignalCallback = SetRandomShapeCallback;

            UpdateSlotsSignal.AddListener(UpdateView);

            View.UserSetShapeToGridSignal.AddListener(OnUserSetShapeToGrid);
            View.CheckAndRemoveItemsSignal.AddListener(OnCheckAndRemoveItems);

            UpdateView();
        }

        /*
         * Payload functuin, is used in UserSetShapeToGridCommand, recieves true if the shape was successfully set 
         * in grid and false if the shape shound be set to the origin position
         */
        private void UserSetShapeCallback(bool isAdded)
        {
            if (isAdded)
            {
                View.SetShapeToPos();
                ShapeSlotIsEmptySignal.Dispatch(View.Shape);
            }
            else
            {
                View.SetShapeToOrigin();
            }
        }

        private void RemoveLinesCallback(List<List<GameObject>> list)
        {
            if (list.Count > 0)
            {
                View.RemoveLines(list);
            }
        }

        private void SetRandomShapeCallback(GameObject ShapeGameObject)
        {
            View.UpdateView(ShapeGameObject);
        }

        private void UpdateView()
        {
            SelectRandomShapeSignal.Dispatch(SelectRandomShapeSignalCallback);
        }

        private void OnUserSetShapeToGrid(List<ItemViewVO> list)
        {
            UserSetShapeSignal.Dispatch(list, SetShapeSignalCallback);
        }

        private void OnCheckAndRemoveItems()
        {
            ChackAndRemoveItemsSignal.Dispatch(RemoveLinesSignalCallback);
        }

       
    }
}

