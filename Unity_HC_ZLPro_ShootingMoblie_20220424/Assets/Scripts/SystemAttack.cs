using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Andrews 
{
    /// <summary>
    /// �����t��
    /// </summary>
    public class SystemAttack : MonoBehaviourPun
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
            //�p�G�O  �������a������  �N����  �o�g
            if(photonView.IsMine)
            {
                //�o�g���s.�I��.�K�[��ť��(�}�j��k)�A���U�o�g���s����}�j��k
                btnFire.onClick.AddListener(Fire);
            }

            
        }


        /// <summary>
        /// �}�j
        /// </summary>
        private void Fire()
        {
            //�s�u.�ͦ�(����.�W��,�y��,����)
            PhotonNetwork.Instantiate(goBullect.name , traFire.position, Quaternion.identity);
        }
    
    }




}

