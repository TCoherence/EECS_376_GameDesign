using UnityEngine;

namespace Assets.Code.Player
{
    /// <inheritdoc />
    /// <summary>
    /// Class for simulating the player's physics
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class SimplePhysics : MonoBehaviour
    {
        public static readonly Vector2 Gravity = new Vector2(0f, -0.15f);

        public static float TimeScale { get; private set; }
        public static void Pause () { TimeScale = 0f; }
        public static void Unpause () { TimeScale = 0.04f; }

        public Material DebugMaterial;

        public delegate void OnCollision (Collider2D other);
        public event OnCollision CollisionDown = other => { }; // fill it in with an empty one at first

        private Rigidbody2D _rb;
        private LayerMask _mask;
        private DebugHUD _hud;

        private Vector2 _velocity;

        internal void Start () {
            _rb = GetComponent<Rigidbody2D>();
            _mask = LayerMask.GetMask("Platforms");
            _velocity = Vector2.right;


            _hud = new DebugHUD(DebugMaterial);

            Unpause();
        }

        internal void FixedUpdate () {
            // TODO fill me in
            float deltaTime = TimeScale;

            Vector2 newPos = _rb.position + deltaTime * _velocity;

            if ( TimeScale > 1e-6  ) AddVelocity(Gravity);
            ProcessCollision();
            _rb.MovePosition(newPos);




        }


        /// <summary>
        /// Called whenever our player hits anything. Handles collisions by adjusting velocity.
        /// We're working under the assumption that everything that we hit is square.
        /// </summary>
        private void ProcessCollision () {
            float dt = TimeScale;
            float dy = dt * _velocity.y;
            float dx = dt * _velocity.x;
            
            //Debug.Log("+++++ THIS IS VEL +++++");
            //Debug.Log("dx = " + dx + "dy = " + dy);
            Vector2 sizetmp = _rb.transform.GetComponent<Collider2D>().bounds.size * 1.0f;
            Vector2 sizex = new Vector2(sizetmp.x * 1.2f, sizetmp.y * 0.6f);
            Vector2 sizey = new Vector2(sizetmp.x * 0.4f, sizetmp.y * 1.2f);
            //float sizey = _rb.GetComponent<Renderer>().bounds.size.y;
            //Vector2 size = new Vector2(sizex, sizey);
            // TODO fill me in
            //Debug.Log("ready to check collider in the future, and size is :" + size);
            //Debug.Log("rb position : " + _rb.position);
            //Debug.Log("Parent position : " + gameObject.transform.position);
            RaycastHit2D raycastHit2DDown = Physics2D.BoxCast(_rb.position, sizey, 0f, Vector2.down, -dy, _mask);
            RaycastHit2D raycastHit2DRight = Physics2D.BoxCast(_rb.position, sizex, 0f, Vector2.right, dx, _mask);

            //if ( raycastHit2DRight.collider != null && raycastHit2DDown.collider != null ) {
            //    _velocity.x = 0f;
            //    _velocity.y = -1f;
            //}
            if ( raycastHit2DDown.collider != null ) {
                Debug.Log("=============I HAVE HIT THE GROUND============");
                //Debug.Log("rb.position: " + _rb.position);
                //Game.Ctx.Clock.Pause();
                if (_velocity.y < 0) _velocity.y = -_velocity.y * 0.3f;

                CollisionDown.Invoke(raycastHit2DDown.collider);
                //Debug.Log("+++" + GameObject.Find("Platform(Clone)").gameObject.name);
                //CollisionDown.Invoke(platforms.GetChild(0).GetComponent<Collider2D>());
            }

            else if (raycastHit2DRight.collider != null && raycastHit2DDown.collider != null) {
                if (_velocity.y < 0) _velocity.y = -_velocity.y * 0.4f;

                CollisionDown.Invoke(raycastHit2DDown.collider);
            }
            else if ( raycastHit2DRight.collider != null ) {
                Debug.Log("=============I HAVE HIT THE RIGHT============");
                _velocity.x = -1f;
                _velocity.y = -2f;
            }
            else {

            }
        }

        /// <summary>
        /// Increase _velocity by some value
        /// </summary>
        /// <param name="value">The amount by which to increase the velocity</param>
        public void AddVelocity (Vector2 value) { _velocity += value; }

        internal void OnGUI () {
            var val = _velocity.normalized * 50f; // 50 pixel long vector in the direction of _velocity
            _hud.DrawArrow(val);
            _hud.DrawMagnitude(_velocity.magnitude);
        }

        /// <summary>
        /// Helper class to draw lines, etc. on the screen
        /// </summary>
        private class DebugHUD
        {
            private static readonly Vector2 HUDCorner = new Vector2(20f, 200f);
            private static readonly Vector3 ArrowStart = HUDCorner + new Vector2(50f, 50f);

            private readonly Material _mat;
            public DebugHUD (Material mat) { _mat = mat; }

            // need optmize later
            public void DrawArrow (Vector3 value) {
                // TODO fill me in
                Vector3 LineEnd = ArrowStart + value;
                Vector3 ArrowEnd = LineEnd + 0.4f * value;
                Vector3 vertical = new Vector3(-value.y, value.x, 0);
                Vector3 Tri1 = LineEnd + 0.2f * vertical;
                Vector3 Tri2 = LineEnd - 0.2f * vertical;
                //Debug.Log("ArrowStart : " + ArrowStart);
                //Debug.Log("ArrowEnd : " + ArrowEnd);

                //Debug.Log("endpoint :" + (ArrowStart + value));
                //Debug.Log("Screen width :" + Screen.width);
                //Debug.Log("Screen height :" + Screen.height);

                GL.PushMatrix();
                _mat.SetPass(0);
                GL.LoadOrtho();
                GL.LoadPixelMatrix();
                
                GL.Begin(GL.LINES);
                GL.Color(Color.red);
                GL.Vertex3(ArrowStart.x , ArrowStart.y , 0);
                GL.Vertex3(LineEnd.x , LineEnd.y , 0);
                GL.End();
                GL.Begin(GL.TRIANGLES);
                GL.Color(Color.red);
                GL.Vertex3(Tri1.x  , Tri1.y , 0);
                GL.Vertex3(Tri2.x , Tri2.y , 0);
                GL.Vertex3(ArrowEnd.x , ArrowEnd.y , 0);
                GL.End();

                GL.PopMatrix();


            }

            public void DrawMagnitude (float magnitude) {
                // TODO fill me in
                //Debug.Log("Magnitude: " + magnitude);
                float num = magnitude ;

                Vector3 vertical = new Vector3(0, -5, 0);
                Vector3 horizontal = new Vector3(5, 0, 0);
                Vector3 startQuad = new Vector3(0,0,0);
                Vector3 rightoffset = 2f * horizontal;
                //Vector3 downoffset = rightoffset + num * vertical;
                Vector3 upoffset = - num * vertical;
                Vector3 quad1 = startQuad + rightoffset;
                Vector3 quad2 = startQuad + upoffset + rightoffset;
                Vector3 quad3 = startQuad + upoffset;
                
                GL.PushMatrix();
                _mat.SetPass(0);
                GL.LoadOrtho();
                GL.LoadPixelMatrix();
                GL.Begin(GL.QUADS);
                GL.Color(Color.cyan);

                GL.Vertex(startQuad);
                GL.Vertex(quad1);
                GL.Vertex(quad2);
                GL.Vertex(quad3);


                GL.End();
                GL.PopMatrix();
            }
        }
    }
}
