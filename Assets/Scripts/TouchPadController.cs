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
    private InputHelper mInput;

    internal override void Start()
    {
        base.Start();
        mInput = new InputHelper(mLogger);
    }

    internal override void Update()
    {
        base.Update();
        ManageTouch();
    }

    private void ManageTouch()
    {
        mLogger.CategoryLog(LogCategoryMethodIn, "click status = " + mInput.ClickStatus);
        if (mInput.ClickStatus == ClickStatus.Up)
        {
            GameObject clickedGO = mInput.ClickedGameObject;

            if(clickedGO != null)
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
}
