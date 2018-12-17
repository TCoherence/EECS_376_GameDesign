using UnityEngine;

namespace Assets.Code
{
    public class Platform : MonoBehaviour {
        public bool LandedOn { get; private set; }

        public void Land () {
            Debug.Log("++++++++++ Going to Land");

            if (LandedOn) { return; }
            Debug.Log("++++++++++ Finish landing");

            Game.Ctx.Score.IncreaseScore(2);
            Debug.Log("++++++++++ SHIFTING PLATFORM++++++++++");
            Game.Ctx.Platforms.ShiftPlatform();
            Debug.Log("+++++++ FININSH SHIFT++++++++++");
            LandedOn = true;
        }
    }
}
