using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Advertisements; //�ޥμs�iAPI


namespace Andrews 
{
    /// <summary>
    /// ���U�s�i���s���[�ݼs�i
    /// �ݧ��s�i�K�[�����^�X
    /// </summary>
    public class AdsManager : MonoBehaviour , IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField, Header("�ݧ��s�i������"), Range(0, 1000)]
        private int assCoinValue = 100;

        private int coinPlayer;

        /// <summary>
        /// �s�i���s�K�[����
        /// </summary>
        private Button btnAdsAddCoin;

        private string gameIdAndroid = "4754882";    //��x Android ID
        private string gameIdIos = "4754883";       // ��x Ios ID
        private string gameId;

        private string adsIdAndroid = "AddCoin";
        private string adsIdIos = "AddCoin";
        private string adsId ;

        /// <summary>
        /// ���a�����ƶq
        /// </summary>
        private Text textCoin;

        //�s�i��l�Ʀ��\�|���檺��k
        public void OnInitializationComplete()
        {
            print("<color=green>1.�s�i��l�Ʀ��\</color>");
        }

        //�s�i��l�ƥ��ѷ|���檺��k
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            print("<color=red>1.�s�i��l�ƥ���,��]:" + message + "</color>");
        }
        //�s�i���J���\�|���檺��k
        public void OnUnityAdsAdLoaded(string placementId)
        {
            print("<color=green>2.�s�i���J���\</color>");
        }
        //�s�i���J���ѷ|���檺��k
        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            print("<color=red>2.�s�i���J����,��]:" + message + "</color>");
        }

        //�s�i��ܥ��ѷ|���檺��k
        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            print("<color=red>3.�s�i��ܥ���</color>");

        }
        //�s�i��ܶ}�l�|���檺��k
        public void OnUnityAdsShowStart(string placementId)
        {
            print("<color=green>3.�s�i��ܶ}�l</color>");
        }
        //�s�i����I���|���檺��k
        public void OnUnityAdsShowClick(string placementId)
        {
            print("<color=green>3.�s�i����I��</color>");
        }
        //�s�i��ܧ����|���檺��k
        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            print("<color=green>3.�s�i��ܧ���</color>");
            coinPlayer = addCoinValue;
            textCoin.text = coinPlayer.ToString();

        }

        /// <summary>
        /// ���J�s�i
        /// </summary>
        private void LoadAds()
        {
            print("���J�s�i�AID:" + adsId);
            Advertisement.Load(adsId, this);
            ShowAds();
        }

        /// <summary>
        /// ��ܼs�i
        /// </summary>
        private void ShowAds()
        {
            Advertisement.Show(adsId, this);

        
        }


        private void Awake()
        {
            textCoin = GameObject.Find("���a�����ƶq").GetComponent<Text>();
            btnAdsAddCoin = GameObject.Find("�s�i���s�K�[����").GetComponent<Button>();
            btnAdsAddCoin.onClick.AddListener(LoadAds);

            InitializeAds();

            //#if �{���϶��P�_���A����F���~�|����Ӱ϶�
            //�p�G ���a �@�~�t�� �O IOS �N���w�� IOS �s�i
            //�_�h�p�G ���a �@�~�t�� �O Android �N���w�� Android �s�i
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
            // PC ����
            adsId = adsIdAndroid;
        }

        /// <summary>
        /// ��l�Ƽs�i�t��
        /// </summary>
        private void InitializeAds()
        {
            gameId = gameIdAndroid;
            Advertisement.Initialize(gameId, true , this);
        }

        
    }

}
