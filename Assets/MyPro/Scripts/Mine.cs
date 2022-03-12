using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    public class Mine : MonoBehaviour
    {
        [SerializeField] private float _damage = 3f;
        [SerializeField] private float _durability = 3f;
               
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<Ghost>();
                enemy.Hurt(_damage);
                Destroy(gameObject);
            }
            
        }
       

    }
}
