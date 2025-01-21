using UnityEngine;
using UnityEngine.Advertisements;

public class ControllerAds : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static ControllerAds instance;  //  instance Game Controller (or Manager for others)
    public string iOsGameId;  // iOS Game ID
    public string androidGameId;  // Android Game ID
    public string idAdsIos;  // iOS ad ID
    public string idAdsAndroid;  // Android ad ID
    private string idSelect;  // Selected platform ID
    private string idAdSelect;  // Selected ad ID
    public bool moodEvidence = true;  // Test  flag

    // Called when Unity Ads initialization is complete
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization completed.");
    }

    // Called when Unity Ads initialization fails
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Initialization failed.");
        throw new System.NotImplementedException();
    }

    // Called when an ad is successfully loaded
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Advertisement.Show(placementId, this);  // Show the loaded ad
    }

    // Called when an ad fails to load
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
    }

    // Called when an ad is clicked
    public void OnUnityAdsShowClick(string placementId)
    {
    }

    // Called when an ad has finished showing
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
    }

    // Called when an ad fails to show
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    // Called when an ad starts showing
    public void OnUnityAdsShowStart(string placementId)
    {
    }

    // Called when the object is initialized
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep the object across scenes
            LoadAdds();  // Load ads based on the platform
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicae instance
        }
    }

    // Load ads 
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
        idAdSelect = idAdsAndroid;  // Default to Android in the editor
#endif
        if (!Advertisement.isInitialized)
        {
            Advertisement.Initialize(idSelect, moodEvidence, this);  // Initialize Unity Ads
        }
    }

    // Show an ad
    public void ShowAd()
    {
        Advertisement.Load(idAdSelect, this);  // Load the ad
    }
}

