using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private float _durability = 10f;  //����� ��������� � �����

        public void Init(float durability)
        {
            _durability = durability;
            Destroy(gameObject, 3f); //3 sec
        }

    }

}
