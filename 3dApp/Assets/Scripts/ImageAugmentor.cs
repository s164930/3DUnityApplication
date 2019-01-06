using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.AugmentedImage;

public class ImageAugmentor : MonoBehaviour
{

    public AugmentedImageVisualizer AugmentedImageVisualizerPrefab;
    private Dictionary<int, AugmentedImageVisualizer> m_Visualizers
               = new Dictionary<int, AugmentedImageVisualizer>();
    public GameObject FitToScanOverlay;

    private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();

    // Update is called once per frame
    public void Update()
    {
        // Exit the app when the 'back' button is pressed.
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Check that motion tracking is tracking.
        if (Session.Status != SessionStatus.Tracking)
        {
            Debug.Log("Motion tracking is not tracking");
            return;
        }

        // Get updated augmented images for this frame.
        Session.GetTrackables<AugmentedImage>(m_TempAugmentedImages, TrackableQueryFilter.Updated);

        // Create visualizers and anchors for updated augmented images that are tracking and do not previously
        // have a visualizer. Remove visualizers for stopped images.
        foreach (var image in m_TempAugmentedImages)
        {
            Debug.Log("There are images in the augmented images list");
            AugmentedImageVisualizer visualizer = null;
            m_Visualizers.TryGetValue(image.DatabaseIndex, out visualizer);
            if (image.TrackingState == TrackingState.Tracking && visualizer == null)
            {
                Debug.Log("The images tracking state is tracking");
                // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                Anchor anchor = image.CreateAnchor(image.CenterPose);
                visualizer = (AugmentedImageVisualizer)Instantiate(AugmentedImageVisualizerPrefab, anchor.transform);
                Debug.Log(anchor.transform);
                visualizer.Image = image;
                m_Visualizers.Add(image.DatabaseIndex, visualizer);
                Debug.Log("We have instantiated the prefab on the image");
            }
            else if (image.TrackingState == TrackingState.Stopped && visualizer != null)
            {
                Debug.Log("The image has stopped tracking");
                m_Visualizers.Remove(image.DatabaseIndex);
                GameObject.Destroy(visualizer.gameObject);
            }
        }

        // Show the fit-to-scan overlay if there are no images that are Tracking.
        foreach (var visualizer in m_Visualizers.Values)
        {
            if (visualizer.Image.TrackingState == TrackingState.Tracking)
            {
                Debug.Log("There is a tracking image, and the overlay is turned off");
                FitToScanOverlay.SetActive(false);
                return;
            }
        }
        Debug.Log("There is no tracking image, and the overlay is active");
        FitToScanOverlay.SetActive(true);
    }
}

