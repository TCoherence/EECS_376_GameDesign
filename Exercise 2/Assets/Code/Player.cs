using System;
using Assets.Code.Structure;
using UnityEngine;

namespace Assets.Code
{
    /// <summary>
    /// Player controller class
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Player : MonoBehaviour, ISaveLoad
    {
        private static string _fireaxis;
        private Rigidbody2D _rb;
        private Gun _gun;

        // ReSharper disable once UnusedMember.Global
        internal void Start () {
            _rb = GetComponent<Rigidbody2D>();
            _gun = GetComponent<Gun>();

            _fireaxis = Platform.GetFireAxis();
        }

        // ReSharper disable once UnusedMember.Global
        internal void Update () {
            HandleInput();
        }

        /// <summary>
        /// Check the controller for player inputs and respond accordingly.
        /// </summary>
        private void HandleInput () {
            // TODO fill me in
            float xAxis = Input.GetAxis("Horizontal");
            float yAxis = Input.GetAxis("Vertical");
            float fire = Input.GetAxis(_fireaxis);
            if ( fire > 0 ) {
                Fire();
            };
            Turn(xAxis);
            Thrust(yAxis);

            //if(Input.GetKey(KeyCode.UpArrow))
            //    _rb.AddRelativeForce(new Vector2(xAxis, 0));
            //if (Input.GetKey(KeyCode.DownArrow))
            //    _rb.AddRelativeForce(new Vector2(-xAxis, 0));
            //if (Input.GetKey(KeyCode.LeftArrow))
            //    _rb.AddTorque(yAxis);
            //else if (Input.GetKey(KeyCode.RightArrow))
                //_rb.AddTorque(-yAxis);
        }

        private void Turn (float direction) {
            if (Mathf.Abs(direction) < 0.02f) { return; }
            _rb.AddTorque(direction * -0.05f);
        }

        private void Thrust (float intensity) {
            if (Mathf.Abs(intensity) < 0.02f) { return; }
            _rb.AddRelativeForce(Vector2.up * intensity);
        }

        private void Fire () {
            _gun.Fire();
        }

        #region saveload

        // TODO fill me in
        public GameData OnSave () {
            PlayerGameData playerdata = new PlayerGameData();
            playerdata.Pos = _rb.position;
            playerdata.Velocity = _rb.velocity;
            playerdata.Rotation = _rb.rotation;
            playerdata.AngularVelocity = _rb.angularVelocity;
            return playerdata;
            //throw new NotImplementedException();
        }

        // TODO fill me in
        public void OnLoad (GameData data) {
            PlayerGameData playerdata = (PlayerGameData)data;
            _rb.position = playerdata.Pos;
            _rb.velocity = playerdata.Velocity;
            _rb.rotation = playerdata.Rotation;
            _rb.angularVelocity = playerdata.AngularVelocity;
            return;
            //throw new NotImplementedException();
        }
        
        #endregion
    }

    public class PlayerGameData : GameData
    {
        public Vector2 Pos;
        public Vector2 Velocity;
        public float Rotation;
        public float AngularVelocity; // read/store as DEGREES
    }
}
