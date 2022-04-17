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
       
        public void Step() //����� �������� ������ ��� ������� ������
        {
            var stepSound = _footSteps[Random.Range(0, _footSteps.Length)]; //������ ��� ���������

            _source.PlayOneShot(stepSound); //��� ����� ��� ��������� ��������� � ����� ������ �����

            // _source.clip = stepSound;  
            // _source.Play(); //����� ��������
        }

        public void Health()
        {
            var healthJumpSound = _healthJump[Random.Range(0, _healthJump.Length)]; //������ ��� ���������

            _source.PlayOneShot(healthJumpSound, 0.1f); //��� ����� ��� ��������� ��������� � ����� ������ �����
            //_source.outputAudioMixerGroup.audioMixer

            // _source.clip = stepSound;  
            // _source.Play(); //����� ��������
        }
    }
}

