using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProjectL
{
    [RequireComponent(typeof(AudioSource))]

    public class StepSoundController : MonoBehaviour
    {
        private AudioSource _source;
        [SerializeField] private AudioClip[] _footSteps;
        [SerializeField] private AudioClip[] _healthJump;
        

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }
       
        public void Step() //можем передать префаб или систему частиц
        {
            var stepSound = _footSteps[Random.Range(0, _footSteps.Length)]; //теперь это аудиоклип

            _source.PlayOneShot(stepSound); //для шагов или выстрелов единичных и можем вольюм скейл

            // _source.clip = stepSound;  
            // _source.Play(); //можно задержку
        }

        public void Health()
        {
            var healthJumpSound = _healthJump[Random.Range(0, _healthJump.Length)]; //теперь это аудиоклип

            _source.PlayOneShot(healthJumpSound, 0.1f); //для шагов или выстрелов единичных и можем вольюм скейл
            //_source.outputAudioMixerGroup.audioMixer

            // _source.clip = stepSound;  
            // _source.Play(); //можно задержку
        }
    }
}

