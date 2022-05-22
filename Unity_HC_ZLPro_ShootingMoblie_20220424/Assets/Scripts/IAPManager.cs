using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace Andrews
{
    public class IAPManager : MonoBehaviour
    {
        [SerializeField, Header("�ʶR�ֽ����s")]
        private IAPButton iapBuySkinRed;
        [SerializeField, Header("�ʶR���ܰT��")]
        private Text textIAPTip;


        /// <summary>
        /// �֦�����ֽ�
        /// </summary>
        private bool hasSkinRed;



        private void Awake()
        {
            //����ֽ����ʫ��s�A�ʶR���\��A�K�[��ť��(�ʶR���\��k)
            iapBuySkinRed.onPurchaseComplete.AddListener(PurchaseCompleteSkinRed );
            //����ֽ����ʫ��s�A�ʶR���ѫ�A�K�[��ť��(�ʶR���Ѥ�k)
            iapBuySkinRed.onPurchaseFailed.AddListener(PurchaseFailedSkinRed );
        }
        /// <summary>
        /// �ʶR���\
        /// </summary>
        private void PurchaseCompleteSkinRed(Product product )
        {
            textIAPTip.text ="����ֽ��A�ʶR���\";

            //�B�z�ʶR���\�᪺�欰
            hasSkinRed = true;

            //����T���I�s���ä��ʴ��ܰT��
            //����I�s(��k�W��,���)
            Invoke("HiddenIAPTip",3);
        }


        /// <summary>
        /// �ʶR����
        /// </summary>
        private void PurchaseFailedSkinRed(Product product , PurchaseFailureReason reason )
        {
            textIAPTip.text ="����ֽ��A�ʶR���ѡA��]: " + reason;

            //����T���I�s���ä��ʴ��ܰT��
            //����I�s(��k�W��,���)
            Invoke("HiddenIAPTip", 3);
        }

        private void HiddenIAPTip()
        {
            textIAPTip.text = "";
        
        }








    }

}
