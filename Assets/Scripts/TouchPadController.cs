using Eq.GoogleVR;
using Eq.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPadController : BaseAndroidMainController
{
    public GameObject mTouchPointerPrefab;
    private TouchPadClickHelper mInput;
    private UnityEngine.UI.Text mTouchedPositionText;

    internal override void Start()
    {
        base.Start();
        mInput = new TouchPadClickHelper(mLogger);
        mTouchedPositionText = GameObject.Find("TouchedPositionText").GetComponent<UnityEngine.UI.Text>();
    }

    internal override void Update()
    {
        base.Update();
        ManageClick();
        ManageTouch();
    }

    private void ManageClick()
    {
        mLogger.CategoryLog(LogCategoryMethodIn, "click status = " + mInput.ClickStatus);
        if (mInput.ClickStatus == ClickStatus.Up)
        {
            GameObject clickedGO = mInput.ClickedGameObject;

            if (clickedGO != null)
            {
                mLogger.CategoryLog(LogCategoryMethodTrace, "hit ray");
                Destroy(clickedGO);
            }
            else
            {
                mLogger.CategoryLog(LogCategoryMethodTrace, "not hit ray");
                Vector3 clickWorldPosition = mInput.ClickWorldPosition;
                GameObject newInputInstance = Instantiate(mTouchPointerPrefab, clickWorldPosition, Quaternion.identity);
                newInputInstance.SetActive(true);
            }
        }
        mLogger.CategoryLog(LogCategoryMethodOut);
    }

    private void ManageTouch()
    {
        mLogger.CategoryLog(LogCategoryMethodIn, "touch status = " + mInput.TouchStatus);
        if (mInput.TouchStatus == TouchStatus.Down)
        {
            mLogger.CategoryLog(LogCategoryMethodTrace, "touched position down(" + mInput.TouchPositionOnTouchPad.x + ", " + mInput.TouchPositionOnTouchPad.y + ")");
            mTouchedPositionText.text = mInput.TouchPositionOnTouchPad.ToString();
        }
        else if (mInput.TouchStatus == TouchStatus.Up)
        {
            mLogger.CategoryLog(LogCategoryMethodTrace, "touched position up(" + mInput.TouchPositionOnTouchPad.x + ", " + mInput.TouchPositionOnTouchPad.y + ")");
            mTouchedPositionText.text = mInput.TouchPositionOnTouchPad.ToString();
        }
        mLogger.CategoryLog(LogCategoryMethodOut);
    }
}
