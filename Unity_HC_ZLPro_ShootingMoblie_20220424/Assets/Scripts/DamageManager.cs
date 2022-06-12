using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using System.Collections;


namespace Andrews
{


    /// <summary>
    /// 受傷管理
    /// </summary>
    public class DamageManager : MonoBehaviourPun
    {
        [SerializeField, Header("血量"), Range(1, 1000)]
        private float hp = 200;
        [SerializeField, Header("擊中特效")]
        private GameObject goVFXHit;
        [SerializeField, Header("溶解著色器")]
        private Shader shaderDissolve ;

        private Material materialDissolve;
        private SystemControl systemControl;
        private SystemAttack systemAttack;

        private float hpMax;

        private string nameBullet = "子彈物件";
        //模型所有網格渲染元件，裡面包含材質球
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
            smr = GetComponentsInChildren<SkinnedMeshRenderer>();                //取得子物件們的元件
            materialDissolve = new Material(shaderDissolve);                    //新增 溶解著色器 材質球
            for (int i = 0; i < smr.Length; i++)                                //利用迴圈賦予所有子物件，溶解材質球
            {
                smr[i].material = materialDissolve;            
            }

            if (photonView.IsMine) textHp.text = hp.ToString();
        }

        //進入
        private void OnCollisionEnter(Collision collision)
        {
            
            if (!photonView.IsMine) return;


            //如果 碰撞物件名稱 包含 子彈物件 就處理 受傷
            if (collision.gameObject.name.Contains(nameBullet ))
            {
                //collision.contacts[0] 碰到的第一個物件
                //point 碰到物件的座標
                Damage(collision.contacts[0].point); 
            }

        }

        //持續
        private void OnCollisionStay(Collision collision)
        {
            
        }

        //離開
        private void OnCollisionExit(Collision collision)
        {
            
        }
         
        
        private void Damage(Vector3 posHit)
        {
            hp -= 20;
            imgHp.fillAmount = hp / hpMax;

            //血量 = 數學.夾住(血量,最小值,最大值)
            hp = Mathf.Clamp(hp, 0, hpMax);
            textHp.text = hp.ToString();

            //連線.生成(特效,擊中座標,角度)
            PhotonNetwork.Instantiate(goVFXHit.name, posHit, Quaternion.identity);
            //如果 血量 <=0 就透過 RPC 同步所有人的物件進行死亡方式
            if (hp <= 0) photonView.RPC("Dead", RpcTarget.All);
                
        }

        //需要同步的方法必須添加 PunRPC 屬性 Remote Procedure Call 遠端程式呼叫
        [PunRPC]
        private void Dead()
        {
            StartCoroutine(Dissolve());
        }    

        private IEnumerator Dissolve()
        {
            systemControl.enabled = false;
            systemAttack.enabled = false;
            //如果 控制系統 的 方向圖示 存在 再處理隱藏
            if (systemControl.traDirectionIcon) systemControl.traDirectionIcon.gameObject.SetActive(false);

            float valueDissolve = 5;                                    //溶解數值起始值
            
            for (int i = 0; i < 30; i++)                                //迴圈執行遞減
            {
                valueDissolve -= 0.3f;
                materialDissolve.SetFloat("dissolve", valueDissolve);   //更新著色器屬性，注意要控制Reference
                yield return new WaitForSeconds(0.08f);                 //等待


            }

            ReturnToLobby();
        }
        /// <summary>
        /// 回到大廳
        /// </summary>
        private void ReturnToLobby()
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.LeaveRoom();              //離開房間
                PhotonNetwork.LoadLevel("遊戲大廳");    //回到大廳場景
            }
        }

    }


}

