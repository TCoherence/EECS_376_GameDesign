
using UnityEngine;

namespace Assets.Code.Menus
{
    public partial class UIManager : IManager
    {
        public static Transform Canvas { get; private set; }

        private MainMenu _main;
        private PauseMenu _pause;

        public bool InMainMenu { get { return _main != null && _main.Showing; } }

        public UIManager () {
            Canvas = GameObject.Find("Canvas").transform; // There should only ever be one canvas
        }

        public void ShowMainMenu () {
            _main = new MainMenu();
            _main.Show();
        }

        public void HideMainMenu () {
            _main.Hide();
            _main = null;
        }

        public void Pause () {
            _pause = new PauseMenu();
            _pause.Show();
        }

        public void Unpause () {
            _pause.Hide();
            _pause = null;
        }

        public void GameStart () { HideMainMenu(); }

        public void GameEnd () { ShowMainMenu(); }

        private abstract class Menu
        {
            protected GameObject Go;
            public bool Showing { get; private set; }

            /// <summary>
            /// Show this menu
            /// </summary>
            public virtual void Show () {
                Showing = true;
                Go.SetActive(true);
            }

            /// <summary>
            /// Hide this menu
            /// </summary>
            public virtual void Hide () {
                GameObject.Destroy(Go);
                Showing = false;
            }
        }
    }

}
