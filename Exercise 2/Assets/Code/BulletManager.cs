using System;
using System.Collections.Generic;
using Assets.Code.Structure;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Assets.Code
{
    /// <summary>
    /// Bullet manager for spawning and tracking all of the game's bullets
    /// </summary>
    public class BulletManager : ISaveLoad
    {
        private readonly Transform _holder;

        /// <summary>
        /// Bullet prefab. Use GameObject.Instantiate with this to make a new bullet.
        /// </summary>
        private readonly Object _bullet;

        public BulletManager (Transform holder) {
            _holder = holder;
            _bullet = Resources.Load("Bullet");
        }

        // TODO fill me in
        public void ForceSpawn (Vector2 pos, Quaternion rotation, Vector2 velocity, float deathtime) {

            GameObject bulletObj ;
            bulletObj = (GameObject)Object.Instantiate(_bullet, pos, rotation, _holder);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bullet.Initialize(velocity, deathtime);

            //var bulletRB = bullet.GetComponent<Rigidbody2D>();
            //bulletRB.velocity = bullet.GetComponent<Rigidbody2D>().velocity;
        }

        #region saveload

        // TODO fill me in
        public GameData OnSave () {
            BulletsData bulletsData = new BulletsData();
            Bullet[] gameObjs = Object.FindObjectsOfType<Bullet>();
            //Bullet[] bullets = (Assets.Code.Structure.Bullet[])Object.FindObjectsOfType(typeof(Bullet));
            for (var i = 0; i < gameObjs.Length; i++ ) {
                BulletData bulletData = new BulletData();
                bulletData.Pos = gameObjs[i].GetComponent<Rigidbody2D>().position;
                bulletData.Rotation = gameObjs[i].GetComponent<Rigidbody2D>().rotation;
                bulletData.Velocity = gameObjs[i].GetComponent<Rigidbody2D>().velocity;
                bulletsData.Bullets.Add(bulletData);
            }
            return bulletsData;
            //throw new NotImplementedException();
        }

        // TODO fill me in
        public void OnLoad (GameData data) {
            Bullet[] gameObjs = Object.FindObjectsOfType<Bullet>();
            foreach ( Bullet obj in gameObjs ) {
                Object.Destroy(obj.gameObject);
            }
            BulletsData bulletsData = (BulletsData)data;
            foreach (BulletData bulletData in bulletsData.Bullets) {
                Game.Bullets.ForceSpawn(bulletData.Pos, Quaternion.Euler(0, 0, bulletData.Rotation), 
                           bulletData.Velocity, Time.time + Bullet.Lifetime);
            }
            //throw new NotImplementedException();
        }

        #endregion

    }

    /// <summary>
    /// Save data for all bullets in game
    /// </summary>
    public class BulletsData : GameData
    {
        public List<BulletData> Bullets = new List<BulletData>();
    }

    /// <summary>
    /// Save data for a single bullet
    /// </summary>
    public class BulletData
    {
        public Vector2 Pos;
        public Vector2 Velocity;
        public float Rotation;
    }
}