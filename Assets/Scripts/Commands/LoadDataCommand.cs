using strange.extensions.command.impl;

namespace Game
{
    class LoadDataCommand:Command
    {
        [Inject]
        public ITetrisModel model { get; set; }

       

        public override void Execute()
        {
            model.CreateGameShapes();
            model.CreateGrid();
        }
    }
}
