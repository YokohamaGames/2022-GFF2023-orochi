using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    public class ParticeleDestroyer : MonoBehaviour
    {
        public static ParticeleDestroyer Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }


        [SerializeField]
        float time;

        private void Start()
        {
            Destroy(gameObject, time);
        }

    }
}
