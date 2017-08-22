using Eq.GoogleVR;
using Eq.Unity;
using UnityEngine;

public class InstantPreviewController : BaseAndroidMainController
{
    private TouchPadClickHelper mClickHelper;
    private GameObject mCube;

    internal override void Start()
    {
        base.Start();

        mCube = GameObject.Find("Cube");
        mClickHelper = new TouchPadClickHelper(mLogger);
    }

    internal override void Update()
    {
        base.Update();

        if (mCube != null)
        {
            if (mClickHelper.TouchStatus == TouchStatus.Up)
            {
                // この2行をコメントアウト/インしてFull VR Preview modeの確認を行ってみてください
                Vector2 touchPadPosition = mClickHelper.TouchPositionOnTouchPad;
                mCube.transform.localScale = new Vector3(touchPadPosition.x, touchPadPosition.y, 1);
            }
        }
    }
}
