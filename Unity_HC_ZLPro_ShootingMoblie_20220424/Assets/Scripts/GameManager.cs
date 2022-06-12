using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;   //�ޥ� �t�ΤΤ@�Z (��Ƶ��c, List , ArrayList...)
using System.Linq;                  //�ޥ� �t�άd�߻y�� (��Ƶ��c�ഫ  API)
using Photon.Realtime;

namespace Andrews
{
   /// <summary>
   /// �C���޲z��
   /// �P�_�p�G�O�s�u�i�J�����a
   /// �N�ͦ����⪫��(�Ԥh)
   /// </summary>
    public class GameManager : MonoBehaviourPunCallbacks
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
            //���a�w�g�[�J�ж�����...
            
            //Photon �s�u.��e�ж�.�i���� = �_ (��L�ж��ݤ��즹�ж�.����[�J)
            PhotonNetwork.CurrentRoom.IsVisible = false;
            
            
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

        //�����a���}�ж��|����@��
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            // �p�G ��e�ж����a�H��  �ѤU  �@�H  �N�Y��
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1) Win();
        }

        private void Win()
        {
            print("�ӧQ");
        }

    }

}
