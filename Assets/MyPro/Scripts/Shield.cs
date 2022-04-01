using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    public class Shield : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private float _durability = 10f;  //видим прочность в юнити

        public void Init(float durability)
        {
            _durability = durability;
            Destroy(gameObject, 10f); 
        }       

        public void Hurt(float _damage)
        {
            print("OuchShieldHurt: " + _durability);

            _durability -= _damage; ;

            if (_durability <= 0)
            {
                Destroy(gameObject);  //или сделать метод Die
            }
        }
    }

}
