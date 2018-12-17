using UnityEngine;

namespace Assets.Code.Player
{

    public class Player : MonoBehaviour
    {
        private static readonly Vector2 Acceleration = new Vector2(1f, 0f);
        private static readonly Vector2 JumpVelocity = new Vector2(0f, 8f);
        private bool isPaused;
        private SimplePhysics _physics;
        private bool _jumping; // are we jumping?

        internal void Start () {
            _physics = GetComponent<SimplePhysics>();
            _physics.CollisionDown += CollisionDown;
            isPaused = false;
        }

        internal void Update () {
            CheckKeys();
            if (!_jumping && !isPaused) { _physics.AddVelocity(Acceleration * Time.deltaTime); } // accelerate when we're on the ground
        }

        private void CheckKeys () {
            if (Input.GetKeyDown(KeyCode.P)) { isPaused = !isPaused; Game.Ctx.Clock.TogglePause(); }
            if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }
        }

        private void CollisionDown (Collider2D other) {
            var platform = other.gameObject.GetComponent<Platform>();
            Debug.Log("--------------------------ENTERING player.cs");
            Debug.Log("--------------------------object Name :" + platform.name);
            if (platform == null) { return; } // shouldn't happen ;)
            if (!platform.LandedOn) {
                Debug.Log("--------------------------Ready to land");
                platform.Land();
                // score stuff
            }

            _jumping = false;
        }

        private void Jump () {
            if (!_jumping) { _physics.AddVelocity(JumpVelocity); }
            _jumping = true;
        }
    }
}