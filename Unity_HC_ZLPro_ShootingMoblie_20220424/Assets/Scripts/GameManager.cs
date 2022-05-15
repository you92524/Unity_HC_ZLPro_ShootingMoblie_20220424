using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;   //�ޥ� �t�ΤΤ@�Z (��Ƶ��c, List , ArrayList...)
using System.Linq;                  //�ޥ� �t�άd�߻y�� (��Ƶ��c�ഫ  API)

namespace Andrews
{
   /// <summary>
   /// �C���޲z��
   /// �P�_�p�G�O�s�u�i�J�����a
   /// �N�ͦ����⪫��(�Ԥh)
   /// </summary>
    public class GameManager : MonoBehaviourPun
    {
        [SerializeField, Header("���⪫��")]
        private GameObject goCharacter;
        [SerializeField, Header("���a�y�Ъ���")]
        private Transform[] traSpawnPoint;

        /// <summary>
        ///  �x�s�ͦ��y�вM��
        /// </summary>
        [SerializeField]
        private List<Transform> traSpwanPointList;



        private void Awake()
        {
            traSpwanPointList = new List<Transform>();  //�s�W �M�檫��
            traSpwanPointList = traSpawnPoint.ToList(); //�}�C�ର�M���Ƶ��c
            
            //�p�G�O�s�u�i�J�����a�A�N�b���A���ͦ����⪫��
           // if (photonView.IsMine)
           // {
                int indexRandom = Random.Range(0, traSpwanPointList.Count); //���o�H���M��(0,�M�檺����)
                Transform tra = traSpwanPointList[indexRandom];             //���o�H���y��


                //photon ���A��.�ͦ�(����,�W��,�y��,����)
                PhotonNetwork.Instantiate(goCharacter.name, tra.position, tra.rotation);

                traSpwanPointList.RemoveAt(indexRandom);                    //�R���w�g���o���ͦ��y�и��
           // }
        
        }

    }

}
