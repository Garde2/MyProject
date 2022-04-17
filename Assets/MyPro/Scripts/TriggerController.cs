using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{    
    public class TriggerController : MonoBehaviour
    {
        [SerializeField] private AudioReverbZone _component;
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _component.enabled = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _component.enabled = false;
            }
        }

        private void OnCollisionEnter(Collision collision)  //float radius
        {
            if (collision.gameObject.TryGetComponent(out ITakeDamage takeDamage))  //коллизия(класс) - точка соприк объектов физич
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    takeDamage.Hurt(20);
                    Debug.Log("Hit..PSystem!");
                    
                }
            }
            
        }
    }
}

