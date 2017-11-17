using strange.extensions.mediation.impl;

namespace Game
{
    public class EndOfGamePanelMediator : Mediator
    {
        [Inject]
        public EndOfGameSignal EndOfGameSignal { get; set; }

        [Inject]
        public EndOfGamePanelView View { get; set; }

        public override void OnRegister()
        {
            EndOfGameSignal.AddListener(OnEndOfGame);
        }

        private void OnEndOfGame()
        {
            View.MovePanel();
        }
    }
}
