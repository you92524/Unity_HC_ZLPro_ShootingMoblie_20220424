using UnityEngine;

namespace Andrews
{
    /// <summary>
    /// ���a��T�����l��
    /// �����l�ܪ��a����y��
    /// </summary>
    public class PlayerUIFollow : MonoBehaviour
    {
        [SerializeField, Header("�첾")]
        private Vector3 v3Offset;
        private string namePlayer = "�Ԥh";

        public Transform traPlayer;

        private void Awake()
        {
            //���a�ܧΤ��� = �C������.�M��(����W��).�ܧΤ���
            //traPlayer = GameObject.Find(namePlayer).transform;
        }

        private void Update()
        {
           Follow();
        }

        private void Follow()
        {

            transform.position = traPlayer.position + v3Offset;
        }


    }








}


