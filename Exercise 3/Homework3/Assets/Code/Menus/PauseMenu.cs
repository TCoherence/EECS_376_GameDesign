using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Menus
{
    public partial class UIManager
    {
        private class PauseMenu : Menu
        {
            private readonly Transform _parent;
            private GameObject pauseMenu;
            public PauseMenu () {
                // TODO fill me in
                var pauseprefab = Resources.Load("Menus/Pause Menu");
                _parent = GameObject.Find("Canvas").transform;
                Go = (GameObject)GameObject.Instantiate(pauseprefab, _parent);
                InitializeButtons();
            }

            /// <summary>
            /// Add listeners to the Pause Menu buttons
            /// </summary>
            private void InitializeButtons () {
                // TODO fill me in
                GameObject.Find("Resume").GetComponent<Button>().onClick.AddListener(ResumeButton);
                GameObject.Find("Main Menu").GetComponent<Button>().onClick.AddListener(ReturnToMain);
            }
            private void ResumeButton() {
                Game.Ctx.Clock.Unpause();
            }
            private void ReturnToMain() {

                GameObject.Destroy(Go);
                Game.Ctx.ReturnToMenu();
            }
        }
    }

}