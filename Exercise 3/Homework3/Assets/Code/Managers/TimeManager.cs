using Assets.Code.Player;

namespace Assets.Code
{
    public class TimeManager : IManager
    {
        public bool Paused { get; private set; }

        /// <summary>
        /// Pauses the game and displays the pause menu
        /// </summary>
        public void Pause () {
            if (Paused || Game.Ctx.UI.InMainMenu) { return; } // doesn't make sense to pause in the main menu

            Paused = true;
            Game.Ctx.UI.Pause();
            SimplePhysics.Pause();
        }

        /// <summary>
        /// Unpauses the game and hides the pause menu
        /// </summary>
        public void Unpause () {
            if (!Paused) { return; }

            Paused = false;
            Game.Ctx.UI.Unpause();
            SimplePhysics.Unpause();
        }

        /// <summary>
        /// Toggle the Pause state
        /// </summary>
        public void TogglePause () {
            if (Paused) { Unpause(); } else { Pause(); }
        }

        public void GameStart () {
            Unpause();
        }

        public void GameEnd () {
            Pause();
        }
    }
}