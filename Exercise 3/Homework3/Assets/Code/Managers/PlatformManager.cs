
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Managers
{
    public class PlatformManager : IManager
    {
        private const float MinLength = 10f;
        private const float MaxLength = 20f;
        private const float MinDeltaX = 10f;
        private const float MaxDeltaX = 15f;
        private const float MaxDeltaY = 3f;

        private const string HolderName = "Platforms";
        private readonly Object _platform;
        private readonly Transform _holder;

        private Queue<GameObject> _platforms;
        private GameObject _oldPlatform;

        private Vector3 _lastPos;
        private Vector3 _lastScale;

        public PlatformManager () {
            // GameObject.Find is okay to call if you're going to call it _once_

            _holder = GameObject.Find(HolderName).transform;
            _platform = Resources.Load("Platform");

            _platforms = new Queue<GameObject>();
            _lastPos = new Vector3(-10f, -8f, 0f);
            _lastScale = Vector3.zero;
        }

        public void GameStart () {
            for (int i = 0; i < 10; i++) {
                _platforms.Enqueue(MakePlatform());
            }
        }

        public void GameEnd () {
            for (int i = 0; i < _holder.childCount; i++) {
                Object.Destroy(_holder.GetChild(i).gameObject);
            }
            _lastPos = new Vector3(-10f, -8f, 0f);
            _lastScale = Vector3.zero;
            _oldPlatform = null;
            _platforms = new Queue<GameObject>();   // change it to be private and not readonly to fix the bug, 
                                                    // this bug occurs when we return to menu and start the game 
                                                    // again, the platform DON'T SHIFT because _platforms haven't
                                                    // been modified in GameEnd() so it stores previous GameObjects
                                                    // which has been distoryed. So shiftplatform failed.
        }

        public void ShiftPlatform () {
            if (_oldPlatform != null) {
                SetPlatform(_oldPlatform);
                _platforms.Enqueue(_oldPlatform);
            }
            _oldPlatform = _platforms.Dequeue();
        }

        private GameObject MakePlatform () {
            var platform = (GameObject)Object.Instantiate(_platform, _holder);
            SetPlatform(platform);
            return platform;
        }

        private void SetPlatform (GameObject platform) {
            var oldpos = _lastPos;
            var newpos = oldpos + new Vector3(
                             Random.Range(MinDeltaX, MaxDeltaX),
                             Random.Range(-MaxDeltaY, MaxDeltaY));
            newpos.x += _lastScale.x / 2;
            platform.transform.position = newpos;

            float newlength = Random.Range(MinLength, MaxLength);
            var scale = platform.transform.localScale;
            scale.x = newlength;
            platform.transform.localScale = scale;

            _lastPos = platform.transform.position;
            _lastScale = platform.transform.localScale;
        }
    }
}
