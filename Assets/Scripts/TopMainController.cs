using Eq.Unity;

public class TopMainController : BaseAndroidMainController {

    public void MenuBackgroundCameraPreviewClicked()
    {
        mLogger.CategoryLog(LogCategoryMethodIn);
        MenuClicked("BackgroundCameraPreview");
        mLogger.CategoryLog(LogCategoryMethodOut);
    }

    public void MenuInstantPreviewClicked()
    {
        mLogger.CategoryLog(LogCategoryMethodIn);
        MenuClicked("InstantPreview");
        mLogger.CategoryLog(LogCategoryMethodOut);
    }

    public void MenuRuntimePermissionClicked()
    {
        mLogger.CategoryLog(LogCategoryMethodIn);
        MenuClicked("RuntimePermission");
        mLogger.CategoryLog(LogCategoryMethodOut);
    }

    public void MenuVideoPlayerClicked()
    {
        mLogger.CategoryLog(LogCategoryMethodIn);
        MenuClicked("VideoPlayer");
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

    public void MenuPointerTestClicked()
    {
        mLogger.CategoryLog(LogCategoryMethodIn);
        MenuClicked("Pointer");
        mLogger.CategoryLog(LogCategoryMethodOut);
    }

    public void MenuResonanceAudioClicked()
    {
        mLogger.CategoryLog(LogCategoryMethodIn);
        MenuClicked("ResonanceAudio");
        mLogger.CategoryLog(LogCategoryMethodOut);
    }

    private void MenuClicked(string menuAndNextSceneName)
    {
        mLogger.CategoryLog(LogCategoryMethodIn);
        PushNextScene(menuAndNextSceneName);
        mLogger.CategoryLog(LogCategoryMethodOut);
    }
}
