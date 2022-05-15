using UnityEngine;
using UnityEngine.UI;

namespace Andews 
{
    /// <summary>
    /// �����t��
    /// </summary>
    public class SystemAttack : MonoBehaviour
    {
        [HideInInspector]
        public Button btnFire;
        
        [SerializeField, Header("�l�u")]
        private GameObject goBullect;
        [SerializeField, Header("�l�u�̤j�ƶq")]
        private int bullectCountMax = 3;
        [SerializeField, Header("�l�u�ͦ���m")]
        private Transform traFire;
        [SerializeField, Header("�l�u�o�g�t��"), Range(0,3000)]
        private int speedFire = 500;

        private int bulletCountCurrent;

        private void Awake()
        {
            btnFire.onClick.AddListener(Fire);
        }


        /// <summary>
        /// �}�j
        /// </summary>
        private void Fire()
        {
            //�ͦ�(����,�y��,����)
            Instantiate(goBullect, traFire.position, Quaternion.identity);
        }
    
    }




}

