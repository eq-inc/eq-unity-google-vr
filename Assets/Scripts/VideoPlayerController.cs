using Eq.GoogleVR;
using Eq.Unity;
using UnityEngine;

public class VideoPlayerController : BaseAndroidMainController
{
    private enum ReserveControl
    {
        None, Play, Pause,
    }

    private GvrVideoPlayerTexture mVideoTexture;
    private TouchPadClickHelper mClickHelper;
    private ReserveControl mReserveControl = ReserveControl.None;

    internal override void Start()
    {
        base.Start();

        GameObject videoPlayerGO = GameObject.Find("VideoPlayer");
        mVideoTexture = videoPlayerGO.GetComponent<GvrVideoPlayerTexture>();
        mClickHelper = new TouchPadClickHelper(mLogger);
    }

    internal override void Update()
    {
        base.Update();

        switch (mReserveControl)
        {
            case ReserveControl.Pause:
                if (mVideoTexture.Pause())
                {
                    mReserveControl = ReserveControl.None;
                }
                break;
            case ReserveControl.Play:
                if (mVideoTexture.Play())
                {
                    mReserveControl = ReserveControl.None;
                }
                break;
        }

        if (mClickHelper.ClickStatus == ClickStatus.Up)
        {
            GameObject clickedGO = mClickHelper.ClickedGameObject;
            if (clickedGO != null && clickedGO.name.CompareTo("VideoPlayer") == 0)
            {
                GvrVideoPlayerTexture videoTexture = clickedGO.GetComponent<GvrVideoPlayerTexture>();
                if (videoTexture.IsPaused)
                {
                    if (!string.IsNullOrEmpty(videoTexture.videoURL))
                    {
                        if (videoTexture.videoURL.StartsWith("http"))
                        {
                            // そのまま再生を試みる
                            if (!videoTexture.Play())
                            {
                                mReserveControl = ReserveControl.Play;
                                mLogger.CategoryLog(LogCategoryMethodTrace, "Play video is failed, Reserved");
                            }
                            else
                            {
                                mReserveControl = ReserveControl.None;
                                mLogger.CategoryLog(LogCategoryMethodTrace, "Play video");
                            }
                        }
                        else
                        {
                            // ローカルファイルのため、external storageへのアクセス権限を確認
                            RuntimePermissionHelper helper = new RuntimePermissionHelper(mLogger);
                            helper.RequestPermission(RuntimePermissionHelper.READ_EXTERNAL_STORAGE, delegate (GvrPermissionsRequester.PermissionStatus[] permissionStatusArray)
                            {
                                bool needFinish = true;

                                if(permissionStatusArray != null && permissionStatusArray.Length > 0)
                                {
                                    if(permissionStatusArray[0].Name.CompareTo(RuntimePermissionHelper.READ_EXTERNAL_STORAGE.Name) == 0 && permissionStatusArray[0].Granted)
                                    {
                                        needFinish = false;
                                        if (!videoTexture.Play())
                                        {
                                            mReserveControl = ReserveControl.Play;
                                            mLogger.CategoryLog(LogCategoryMethodTrace, "Play video is failed, Reserved");
                                        }
                                        else
                                        {
                                            mReserveControl = ReserveControl.None;
                                            mLogger.CategoryLog(LogCategoryMethodTrace, "Play video");
                                        }
                                    }
                                }

                                if (needFinish)
                                {
                                    PopCurrentScene();
                                }
                            });
                        }
                    }
                }
                else
                {
                    if (!videoTexture.Pause())
                    {
                        mReserveControl = ReserveControl.Pause;
                        mLogger.CategoryLog(LogCategoryMethodTrace, "Pause video is failed, Reserved");
                    }
                    else
                    {
                        mReserveControl = ReserveControl.None;
                        mLogger.CategoryLog(LogCategoryMethodTrace, "Pause video");
                    }
                }
            }
        }
        else if (mClickHelper.AppButtonStatus == ButtonPushStatus.Up)
        {
            PopCurrentScene();
        }
    }
}
