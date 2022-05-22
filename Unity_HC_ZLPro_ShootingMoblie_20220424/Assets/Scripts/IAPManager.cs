using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace Andrews
{
    public class IAPManager : MonoBehaviour
    {
        [SerializeField, Header("購買皮膚按鈕")]
        private IAPButton iapBuySkinRed;
        [SerializeField, Header("購買提示訊息")]
        private Text textIAPTip;


        /// <summary>
        /// 擁有紅色皮膚
        /// </summary>
        private bool hasSkinRed;



        private void Awake()
        {
            //紅色皮膚內購按鈕，購買成功後，添加監聽器(購買成功方法)
            iapBuySkinRed.onPurchaseComplete.AddListener(PurchaseCompleteSkinRed );
            //紅色皮膚內購按鈕，購買失敗後，添加監聽器(購買失敗方法)
            iapBuySkinRed.onPurchaseFailed.AddListener(PurchaseFailedSkinRed );
        }
        /// <summary>
        /// 購買成功
        /// </summary>
        private void PurchaseCompleteSkinRed(Product product )
        {
            textIAPTip.text ="紅色皮膚，購買成功";

            //處理購買成功後的行為
            hasSkinRed = true;

            //延遲三秒後呼叫隱藏內購提示訊息
            //延遲呼叫(方法名稱,秒數)
            Invoke("HiddenIAPTip",3);
        }


        /// <summary>
        /// 購買失敗
        /// </summary>
        private void PurchaseFailedSkinRed(Product product , PurchaseFailureReason reason )
        {
            textIAPTip.text ="紅色皮膚，購買失敗，原因: " + reason;

            //延遲三秒後呼叫隱藏內購提示訊息
            //延遲呼叫(方法名稱,秒數)
            Invoke("HiddenIAPTip", 3);
        }

        private void HiddenIAPTip()
        {
            textIAPTip.text = "";
        
        }








    }

}
