using Eq.GoogleVR;
using Eq.Unity;
using UnityEngine;

public class BackgroundCameraPreview : BaseAndroidMainController
{
    public GameObject mCameraPreviewQuad;
    private Vector3 mCenterScreenPoint;

    internal override void Start()
    {
        base.Start();

        mLogger.CategoryLog(LogCategoryMethodIn);
        SetScreenOrientation(ScreenOrientation.Landscape);
        SetScreenTimeout(BaseAndroidMainController.NeverSleep);

        mCenterScreenPoint = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        RuntimePermissionHelper helper = new RuntimePermissionHelper(mLogger);
        helper.RequestPermission(RuntimePermissionHelper.CAMERA, delegate (GvrPermissionsRequester.PermissionStatus[] permissionStatusArray)
        {
            mLogger.CategoryLog(LogCategoryMethodIn);

            bool granted = true;

            foreach (GvrPermissionsRequester.PermissionStatus permissionStatus in permissionStatusArray)
            {
                if (!permissionStatus.Granted)
                {
                    granted = permissionStatus.Granted;
                    break;
                }
            }

            if (granted)
            {
                ShowCameraPreview();
            }
            else
            {
                Back();
            }

            mLogger.CategoryLog(LogCategoryMethodOut);
        });

        mLogger.CategoryLog(LogCategoryMethodOut);
    }

    internal override void Update()
    {
        base.Update();

        //Vector3 cameraFwdWp = Camera.main.transform.forward;
        //Vector3 cameraRotation = Camera.main.transform.rotation.eulerAngles;
        ////mCameraPreviewQuad.transform.SetPositionAndRotation(cameraFwdWp, Quaternion.Euler(cameraRotation.x, cameraRotation.y, cameraRotation.z));
        //mCameraPreviewQuad.transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, cameraRotation.z);

        //mLogger.CategoryLog(LogCategoryMethodTrace, "camera position: " + Camera.main.transform.position + ", rotation: " + Camera.main.transform.rotation.eulerAngles);
        //mLogger.CategoryLog(LogCategoryMethodTrace, "camera preview position: " + mCameraPreviewQuad.transform.position + ", rotation: " + mCameraPreviewQuad.transform.rotation.eulerAngles);
    }

    private void ShowCameraPreview()
    {
        mLogger.CategoryLog(LogCategoryMethodIn);

        WebCamTexture webCamTexture = null;
        foreach (WebCamDevice device in WebCamTexture.devices)
        {
            mLogger.CategoryLog(LogCategoryMethodTrace, "camera device name = " + device.name + ", is front facing= " + device.isFrontFacing);
            if (!device.isFrontFacing)
            {
                webCamTexture = new WebCamTexture(device.name);
                break;
            }
        }

        if (webCamTexture != null)
        {
            mCameraPreviewQuad.GetComponent<Renderer>().material.mainTexture = webCamTexture;
            webCamTexture.Play();
        }
        else
        {
            mLogger.CategoryLog(LogCategoryMethodTrace, "no web camera texture");
            Back();
        }

        mLogger.CategoryLog(LogCategoryMethodOut);
    }
}
