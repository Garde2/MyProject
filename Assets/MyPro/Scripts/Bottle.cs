using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    public class Bottle : MonoBehaviour
    {

        public GameObject healthPrefab;

        private void OnTriggerStay(Collider other)
        {
            if(other.tag == "Player" && Input.GetKeyDown(KeyCode.Z))
            {
                Destroy(this.gameObject);
            }
        }


    }
}