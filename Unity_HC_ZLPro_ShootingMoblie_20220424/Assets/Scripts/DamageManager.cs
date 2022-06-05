using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Andews;

namespace Andrews
{


    /// <summary>
    /// ���˺޲z
    /// </summary>
    public class DamageManager : MonoBehaviourPun
    {
        [SerializeField, Header("��q"), Range(1, 1000)]
        private float hp = 200;
        [SerializeField, Header("�����S��")]
        private GameObject goVFXHit;
        [SerializeField, Header("���ѵۦ⾹")]
        private Shader shaderDissolve ;

        private Material materialDissolve;
        private SystemControl systemControl;
        private SystemAttack systemAttack;

        private float hpMax;

        private string nameBullet = "�l�u����";
        //�ҫ��Ҧ������V����A�̭��]�t����y
        private SkinnedMeshRenderer[] smr;

        [HideInInspector ]
        public Image imgHp;
        [HideInInspector]
        public TextMeshProUGUI textHp;


        private void Awake()
        {
            systemControl = GetComponent<SystemControl>();
            systemAttack = GetComponent<SystemAttack>();

            hpMax = hp;
            smr = GetComponentsInChildren<SkinnedMeshRenderer>();                //���o�l����̪�����
            materialDissolve = new Material(shaderDissolve);                    //�s�W ���ѵۦ⾹ ����y
            for (int i = 0; i < smr.Length; i++)                                //�Q�ΰj��ᤩ�Ҧ��l����A���ѧ���y
            {
                smr[i].material = materialDissolve;            
            }

            if (photonView.IsMine) textHp.text = hp.ToString();
        }

        //�i�J
        private void OnCollisionEnter(Collision collision)
        {
            
            if (!photonView.IsMine) return;


            //�p�G �I������W�� �]�t �l�u���� �N�B�z ����
            if (collision.gameObject.name.Contains(nameBullet ))
            {
                //collision.contacts[0] �I�쪺�Ĥ@�Ӫ���
                //point �I�쪫�󪺮y��
                Damage(collision.contacts[0].point); 
            }

        }

        //����
        private void OnCollisionStay(Collision collision)
        {
            
        }

        //���}
        private void OnCollisionExit(Collision collision)
        {
            
        }
         
        
        private void Damage(Vector3 posHit)
        {
            hp -= 20;
            imgHp.fillAmount = hp / hpMax;

            //��q = �ƾ�.����(��q,�̤p��,�̤j��)
            hp = Mathf.Clamp(hp, 0, hpMax);
            textHp.text = hp.ToString();

            //�s�u.�ͦ�(�S��,�����y��,����)
            PhotonNetwork.Instantiate(goVFXHit.name, posHit, Quaternion.identity);
            //�p�G ��q <=0 �N�z�L RPC �P�B�Ҧ��H������i�榺�`�覡
            if (hp <= 0) photonView.RPC("Dead", RpcTarget.All);
                
        }

        //�ݭn�P�B����k�����K�[ PunRPC �ݩ� Remote Procedure Call ���ݵ{���I�s
        [PunRPC]
        private void Dead()
        {
            StartCoroutine(Dissolve());
        }    

        private IEnumerator Dissolve()
        {
            systemControl.enabled = false;
            systemAttack.enabled = false;
            systemControl.traDirectionIcon.gameObject.SetActive(false);

            float valueDissolve = 5;
            
            for (int i = 0; i < 25; i++)
            {
                valueDissolve -= 0.3f;
                materialDissolve.SetFloat("dissolve", valueDissolve);
                yield return new WaitForSeconds(0.08f);


            }


        }

    }


}

