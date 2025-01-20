using UnityEngine;
using UnityEngine.Advertisements;

public class ControllerAds : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{

    public static ControllerAds instance;
    public string iOsGameId;
    public string androidGameId;

    public string idAdsIos;
    public string idAdsAndroid;

    private string idSelect;
    private string idAdSelect;

    public bool moodEvidence = true;

    public void OnInitializationComplete()
    {
        Debug.Log("Completado");
        throw new System.NotImplementedException();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Fallo");
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAdds();
        } else
        {
            Destroy(gameObject);
        }
    }

    private void LoadAdds()
    {
#if UNITY_ANDROID
        idSelect = androidGameId;
        idAdSelect = idAdsAndroid;

#elif UNITY_IOS

        idSelect = iOsGameId;
        idAdSelect = idAdsIos;
#elif UNITY_EDITOR

        idSelect = androidGameId;
        idAdSelect = idAdsAndroid;
#endif
        if (!Advertisement.isInitialized)
        {
            Advertisement.Initialize(idSelect, moodEvidence, this);
        }

    }

    public void ShowAd()
    {
        Advertisement.Load(idAdSelect, this);
    }

  
}
