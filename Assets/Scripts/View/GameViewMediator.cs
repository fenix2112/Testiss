using strange.extensions.mediation.impl;

namespace Game {
    public class GameViewMediator : Mediator
    {
        [Inject]
        public ITetrisModel Model { get; set; }

        [Inject]
        public GameView View { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            CreateGrig();
        }

        private void CreateGrig()
        {
            View.CreateGrig();
        }

    }

}
