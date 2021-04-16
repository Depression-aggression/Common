using System;

namespace FD.UI.Menu
{
    public interface IMenuController
    {
        IElementUI[] Menues { get; }

        Action OnGameStarted { get; set; }
        Action OnGameEnded { get; set; }

        Action OnExitButtonPressed { get; set; }
        Action OnExitConfirmed { get; set; }
        Action OnExitCanceled { get; set; }

        void ToggleStartGamePanel();
        void ToggleOptionsPanel();
        void ToggleExitPanel();

        void ShowStartPanel();
        void ShowOptionsPanel();
        void ShowExitPanel();
    }
}
