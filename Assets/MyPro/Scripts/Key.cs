using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    public class Key : MonoBehaviour
    {        
        [SerializeField] private int id;
        public GameObject TextTakeKey;
        public GameObject TextTookKey;
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TextTakeKey.SetActive(true);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player" && Input.GetKeyDown(KeyCode.X))
            {
                if (other.TryGetComponent(out IStorage storage))
                {
                    storage.AddKey(id);
                    TextTakeKey.SetActive(false);
                    other.GetComponent<MonoBehaviour>().StartCoroutine(ShowTextToKey());
                    Destroy(this.gameObject);
                }                
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TextTakeKey.SetActive(false);
            }
        }

        public IEnumerator ShowTextToKey()
        {
            TextTookKey.SetActive(true);
            yield return new WaitForSeconds(3);
            TextTookKey.SetActive(false);
        }
    }
}

