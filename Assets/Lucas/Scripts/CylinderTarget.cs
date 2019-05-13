using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;
using Vuforia;

public class CylinderTarget :DefaultTrackableEventHandler {
    private VideoPlayer videoplayer;
    private RawImage rawImage;
    private Tween tween;
    // Start is called before the first frame update
     void Awake()
    {
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(()=> {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        });

        videoplayer = GetComponentInChildren<VideoPlayer>();
        rawImage = GetComponentInChildren<RawImage>();

        videoplayer.started += a => {
            tween=rawImage.DOFade(1,2f);
        };

        videoplayer.loopPointReached += a => {
            rawImage.DOFade(0,2f);
        };
    }

    protected override void OnTrackingFound() {
        base.OnTrackingFound();
        videoplayer.enabled = true;
    }

    protected override void OnTrackingLost() {
        base.OnTrackingLost();
        videoplayer.enabled = false;
        if(tween!=null&& tween.IsPlaying()) {
            tween.Kill();
        }
        rawImage.DOFade(0,0f);
    }
}
