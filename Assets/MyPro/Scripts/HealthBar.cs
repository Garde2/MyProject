using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyProjectL
{

    public class HealthBar : MonoBehaviour
    {
        public Image bar;
        public float _fill = 1f;  

        void Update()
        {
            //_fill -= Time.deltaTime * 1;    проверили
            bar.fillAmount = _fill;
        }
    }
}

