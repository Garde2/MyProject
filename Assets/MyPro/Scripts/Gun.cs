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
            bullet.Init(10, _speedBullet);
            //event?.Invoke();  //проверка на нуль (?)
            //Invoke(nameof(Reloading), _cooldown);   invoke не можем тк не монобих
            return bulletObj;
        }
    }
}

