using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Andrews 
{
    /// <summary>
    /// 攻擊系統
    /// </summary>
    public class SystemAttack : MonoBehaviourPun
    {
        [HideInInspector]
        public Button btnFire;
        
        [SerializeField, Header("子彈")]
        private GameObject goBullect;
        [SerializeField, Header("子彈最大數量")]
        private int bullectCountMax = 3;
        [SerializeField, Header("子彈生成位置")]
        private Transform traFire;
        [SerializeField, Header("子彈發射速度"), Range(0,3000)]
        private int speedFire = 500;

        private int bulletCountCurrent;

        private void Awake()
        {
            //如果是  本身玩家的物件  就執行  發射
            if(photonView.IsMine)
            {
                //發射按鈕.點擊.添加監聽器(開槍方法)，按下發射按鈕執行開槍方法
                btnFire.onClick.AddListener(Fire);
            }

            
        }


        /// <summary>
        /// 開槍
        /// </summary>
        private void Fire()
        {
            //暫存子彈 = 連線.生成(物件.名稱,座標,角度)
            GameObject tempBullet = PhotonNetwork.Instantiate(goBullect.name , traFire.position, Quaternion.identity);
            //暫存子彈.取得元件<剛體>().添加推力(角色前方*子彈速度)
            tempBullet.GetComponent<Rigidbody>().AddForce(transform.forward * speedFire);  
        }
    
    }




}

