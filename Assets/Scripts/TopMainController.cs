using Eq.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TopMainController : BaseAndroidMainController {

    public void MenuRuntimePermissionClicked()
    {
        mLogger.CategoryLog(LogCategoryMethodIn);
        MenuClicked("RuntimePermission");
        mLogger.CategoryLog(LogCategoryMethodOut);
    }

    public void MenuSpatialAudioClicked()
    {
        mLogger.CategoryLog(LogCategoryMethodIn);
        MenuClicked("SpatialAudio");
        mLogger.CategoryLog(LogCategoryMethodOut);
    }

    public void MenuTouchPadTestClicked()
    {
        mLogger.CategoryLog(LogCategoryMethodIn);
        MenuClicked("TouchPad");
        mLogger.CategoryLog(LogCategoryMethodOut);
    }

    private void MenuClicked(string menuAndNextSceneName)
    {
        mLogger.CategoryLog(LogCategoryMethodIn);
        PushNextScene(menuAndNextSceneName);
        mLogger.CategoryLog(LogCategoryMethodOut);
    }
}
