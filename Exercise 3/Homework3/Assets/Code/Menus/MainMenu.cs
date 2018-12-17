using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Menus
{
    public partial class UIManager
    {
        private class MainMenu : Menu
        {
            private readonly Transform _parent;
            private GameObject menu;
            public MainMenu () {
                // TODO fill me in
                var menuprefab = Resources.Load("Menus/Main Menu");
                _parent = GameObject.Find("Canvas").transform;
                Go = (GameObject)GameObject.Instantiate(menuprefab, _parent);
                 
                InitializeButtons();
            }


            /// <summary>
            /// Add listeners to the MainMenu buttons
            /// </summary>
            private void InitializeButtons () {
                // TODO fill me in
                GameObject.Find("Start").GetComponent<Button>().onClick.AddListener(StartGame);
                GameObject.Find("Quit").GetComponent<Button>().onClick.AddListener(QuitGame);
            }
            public void StartGame() {
                Game.Ctx.StartGame();
                HideMenu();
            }
            public void QuitGame() {
                Game.Ctx.QuitGame();
            } 
            public void HideMenu() {
                GameObject.Destroy(Go);
            }
        }
    }
}