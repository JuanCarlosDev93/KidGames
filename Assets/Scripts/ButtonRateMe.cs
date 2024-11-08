using Google.Play.Review;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRateMe : MonoBehaviour
{
    
    // Create instance of ReviewManager
    private ReviewManager _reviewManager;

    [SerializeField] private string appUrl;
    private PlayReviewInfo _playReviewInfo;

    private void Start()
    {
        _reviewManager = new ReviewManager();
    }
    public void OnClicRateMe()
    {
        Debug.Log("Clicked review button");
        //Application.OpenURL(appUrl);
        StartCoroutine(RequestRate());
    }

    IEnumerator RequestRate()
    {
        //yield return new WaitForSeconds(1);
        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        _playReviewInfo = requestFlowOperation.GetResult();

        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        // The flow has finished. The API does not indicate whether the user
        // reviewed or not, or even whether the review dialog was shown. Thus, no
        // matter the result, we continue our app flow.
    }
    
}
