using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class TetrisContext : SignalContext
    {
        /*
         *  Constructor
         * */
        public TetrisContext(MonoBehaviour contextView) : base(contextView)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();
            // Bind service
            injectionBinder.Bind<IGameCoreService>().To<GameCoreService>().ToSingleton();

            // Bind Model
            injectionBinder.Bind<ITetrisModel>().To<TetrisModel>().ToSingleton();
            injectionBinder.Bind<UpdateSlotsSignal>().ToSingleton();
            injectionBinder.Bind<EndOfGameSignal>().ToSingleton();

            mediationBinder.Bind<GameView>().To<GameViewMediator>();
            mediationBinder.Bind<SlotView>().To<SlotViewMediator>();
            mediationBinder.Bind<EndOfGamePanelView>().To<EndOfGamePanelMediator>();

            commandBinder.Bind<StartGameSignal>().To<LoadDataCommand>().Once();
            commandBinder.Bind<ShapeSlotIsEmptySignal<GameObject>>().To<UpdateShapeSlotsCommand>();
            commandBinder.Bind<CheckNextMoveSignal>().To<CheckNewMoveCommand>();

            commandBinder.Bind<ChackAndRemoveItemsSignal<RemoveLinesSignalDelegate>>().To<ChackAndRemoveItemsCommand>();
            commandBinder.Bind<UserSetShapeToGridSignal<List<ItemViewVO>, UserSetShapeSignalDelegate>>().To<UserSetShapeToGridCommand>();
            commandBinder.Bind<SelectRandomShapeSignal<SelectRandomShapeSignalDelegate>>().To<SelectRandomShapeCommand>();
        }
    }

}
