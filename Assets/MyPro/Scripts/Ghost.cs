using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    public class Ghost : MonoBehaviour
    {
        [SerializeField] private float _health = 6f;  //видим прочность Ghost

        public void Hurt(float damage)
        {
            print("Ouch: " + damage);

            _health -= damage; 

            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
                
    }
}

