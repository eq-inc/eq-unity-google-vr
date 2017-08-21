using Eq.GoogleVR;
using Eq.Unity;
using System.Text;
using UnityEngine;

public class RuntimePermissionController : BaseAndroidMainController
{
    private TouchPadClickHelper mClickHelper;
    private UnityEngine.UI.Text mExternalStorageAccessStatusText;
    private UnityEngine.UI.Button mRequestPerissionButton;
    private RuntimePermissionHelper mPermissionHelper;

    internal override void Start()
    {
        base.Start();

        mClickHelper = new TouchPadClickHelper(mLogger);
        mPermissionHelper = new RuntimePermissionHelper(mLogger);
        mExternalStorageAccessStatusText = GameObject.Find("ExternalStorageAccessStatusText").GetComponent<UnityEngine.UI.Text>();
        mRequestPerissionButton = GameObject.Find("RequestPerissionButton").GetComponent<UnityEngine.UI.Button>();
        ShowExternalStoragePermissionStatus();
    }

    internal override void Update()
    {
        base.Update();
        
        if(mClickHelper.AppButtonStatus == ButtonPushStatus.Up)
        {
            PopCurrentScene();
        }
    }

    public void RequestPermissionClicked()
    {
        mLogger.CategoryLog(LogCategoryMethodIn);

        mPermissionHelper.RequestPermission(RuntimePermissionHelper.READ_EXTERNAL_STORAGE, delegate(GvrPermissionsRequester.PermissionStatus[] grantedArray){
            ShowExternalStoragePermissionStatus();
        });

        mLogger.CategoryLog(LogCategoryMethodOut);
    }

    private void ShowExternalStoragePermissionStatus()
    {
        mLogger.CategoryLog(LogCategoryMethodIn);
        bool granted = mPermissionHelper.CheckPermission(RuntimePermissionHelper.READ_EXTERNAL_STORAGE);

        StringBuilder builder = new StringBuilder();
        builder.Append(RuntimePermissionHelper.READ_EXTERNAL_STORAGE.Name).Append(" is ").Append(granted ? "allowed" : "denied");
        mExternalStorageAccessStatusText.text = builder.ToString();

        mRequestPerissionButton.enabled = !granted;
        mLogger.CategoryLog(LogCategoryMethodOut, RuntimePermissionHelper.READ_EXTERNAL_STORAGE + "is " + (granted ? "allowed" : "denined"));
    }
}
