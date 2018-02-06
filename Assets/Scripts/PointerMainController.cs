using Eq.GoogleVR;
using Eq.Unity;
using System.Collections;
using UnityEngine;

public class PointerMainController : BaseAndroidMainController {
    public enum UpdateInterval
    {
        ASAP = 0,
        over100ms = 100,
        over500ms = 500,
        over1000ms = 1000,
        over3000ms = 3000,
        over5000ms = 5000,
    }

    private static readonly string PointerInfoFormat = "Accel: {0:0.000},{1:0.000},{2:0.000}\nAngle: {3:0.000},{4:0.000},{5:0.000}\nGyro: {6:0.000},{7:0.000},{8:0.000}";
    private static readonly UpdateInterval[] AllValues = (UpdateInterval[])System.Enum.GetValues(typeof(UpdateInterval));
    private UnityEngine.UI.Text mPointerInfoText;
    private UnityEngine.UI.Dropdown mUpdateIntervalDD;
    private PointerDeviceHelper mInputHelper;

    private float mCurrentUpdateInterval = float.MaxValue;
    private float mSleepingTime = 0;

    // Use this for initialization
    internal override void Start()
    {
        base.Start();

        mInputHelper = new PointerDeviceHelper(mLogger);

        GameObject pointerInfoGO = GameObject.Find("PointerInfoText");
        mPointerInfoText = pointerInfoGO.GetComponent<UnityEngine.UI.Text>();
        GameObject updateIntervalGO = GameObject.Find("UpdateInterval");
        mUpdateIntervalDD = updateIntervalGO.GetComponent<UnityEngine.UI.Dropdown>();
    }

    // Update is called once per frame
    internal override void Update()
    {
        base.Update();

        if(mInputHelper.AppButtonStatus == ButtonPushStatus.Up)
        {
            if (mUpdateIntervalDD.value >= (AllValues.Length - 1))
            {
                mUpdateIntervalDD.value = 0;
            }
            else
            {
                mUpdateIntervalDD.value = mUpdateIntervalDD.value + 1;
            }
        }

        int currentUpdateIntervalIndex = mUpdateIntervalDD.value;
        float currentUpdateInterval = ((float)(AllValues[currentUpdateIntervalIndex])) / 1000;

        if(mCurrentUpdateInterval != currentUpdateInterval)
        {
            mCurrentUpdateInterval = currentUpdateInterval;
        }

        mSleepingTime += Time.deltaTime;
        if(mSleepingTime > mCurrentUpdateInterval)
        {
            mSleepingTime = 0;

            Vector3 accel = GvrController.Accel;
            Vector3 angle = GvrController.Orientation.eulerAngles;
            Vector3 gyro = GvrController.Gyro;

            mPointerInfoText.text = string.Format(PointerInfoFormat, accel.x, accel.y, accel.z, angle.x, angle.y, angle.z, gyro.x, gyro.y, gyro.z);
        }
    }
}
