using UnityEngine;
using UnityEngine.Advertisements;

namespace B_Scripts.Ads
{
    public class Ads_Rewarded : MonoBehaviour, IUnityAdsListener { 

        public string gameId = "1234567";
        public string myPlacementId = "rewardedVideo";
        public bool testMode = true;

        // Initialize the Ads listener and service:
        void Start () {
            Advertisement.AddListener (this);
            Advertisement.Initialize (gameId, testMode);
        }

        public void ShowRewardedVideo() {
            // Check if UnityAds ready before calling Show method:
            if (Advertisement.IsReady(myPlacementId)) {
                Advertisement.Show(myPlacementId);
            } 
            else {
                Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
            }
        }

        // Implement IUnityAdsListener interface methods:
        public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
            // Define conditional logic for each ad completion status:
            if (showResult == ShowResult.Finished) {
                // Reward the user for watching the ad to completion.
            } else if (showResult == ShowResult.Skipped) {
                // Do not reward the user for skipping the ad.
            } else if (showResult == ShowResult.Failed) {
                Debug.LogWarning ("The ad did not finish due to an error.");
            }
        }

        public void OnUnityAdsReady (string placementId) {
            // If the ready Placement is rewarded, show the ad:
            if (placementId == myPlacementId) {
                // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
            }
        }

        public void OnUnityAdsDidError (string message) {
            // Log the error.
        }

        public void OnUnityAdsDidStart (string placementId) {
            // Optional actions to take when the end-users triggers an ad.
        } 

        // When the object that subscribes to ad events is destroyed, remove the listener:
        public void OnDestroy() {
            Advertisement.RemoveListener(this);
        }
    }
}
