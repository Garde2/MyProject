using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    public class Gun : WeaponFabric
    {
        private float _speedBullet;
        public Gun(GameObject spawnPrefab, Transform spawnPoint, float speedBullet) : base(spawnPrefab, spawnPoint)
        {
            _speedBullet = speedBullet;
        }
        public override GameObject Spawn()
        {            
            var bulletObj = Object.Instantiate(_spawnPrefab, _spawnPoint.position, _spawnPoint.rotation);
            var bullet = bulletObj.GetComponent<Bullet>();
            bulletObj.layer = LayerMask.NameToLayer("Enemy");
            bullet.Init(10, _speedBullet);            
            return bulletObj;
        }
    }
}

