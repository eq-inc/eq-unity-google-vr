using Eq.GoogleVR;
using Eq.Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpatialAudioController : BaseAndroidMainController
{
    public GameObject mSoundBoxPrefab;
    private TouchPadClickHelper mClickHelper;
    private Dictionary<GameObject, bool> mExistSoundBoxDic = new Dictionary<GameObject, bool>();

    internal override void Start()
    {
        base.Start();

        mClickHelper = new TouchPadClickHelper(mLogger);
    }

    internal override void Update()
    {
        base.Update();
        PlaySound();
        ManageClick();
    }

    private void PlaySound()
    {
        Dictionary<GameObject, bool> tempChangedSoundBoxDic = new Dictionary<GameObject, bool>();

        foreach (KeyValuePair<GameObject, bool> keyValuePair in mExistSoundBoxDic)
        {
            if (!keyValuePair.Value)
            {
                GameObject soundBoxGO = keyValuePair.Key;
                GvrAudioSource audioSource = soundBoxGO.GetComponent<GvrAudioSource>();
                if (audioSource.isPlaying)
                {
                    tempChangedSoundBoxDic.Add(soundBoxGO, true);
                }
                else
                {
                    audioSource.clip.LoadAudioData();
                    audioSource.Play();
                    mLogger.CategoryLog(LogCategoryMethodTrace, "playing: " + audioSource.isPlaying);
                    tempChangedSoundBoxDic.Add(soundBoxGO, audioSource.isPlaying);
                }
            }
        }

        foreach (KeyValuePair<GameObject, bool> keyValuePair in tempChangedSoundBoxDic)
        {
            if (keyValuePair.Value)
            {
                mExistSoundBoxDic[keyValuePair.Key] = true;
            }
        }
    }

    private void ManageClick()
    {
        mLogger.CategoryLog(LogCategoryMethodIn, "click status = " + mClickHelper.ClickStatus);
        if (mClickHelper.ClickStatus == ClickStatus.Up)
        {
            GameObject clickedGO = mClickHelper.ClickedGameObject;

            if (clickedGO != null)
            {
                mLogger.CategoryLog(LogCategoryMethodTrace, "hit ray");
                mExistSoundBoxDic.Remove(clickedGO);
                Destroy(clickedGO);
            }
            else
            {
                mLogger.CategoryLog(LogCategoryMethodTrace, "not hit ray");
                Vector3 clickWorldPosition = mClickHelper.ClickWorldPosition;
                GameObject newInputInstance = Instantiate(mSoundBoxPrefab, clickWorldPosition, Quaternion.identity);
                newInputInstance.SetActive(true);

                mExistSoundBoxDic.Add(newInputInstance, false);
            }
        }
        mLogger.CategoryLog(LogCategoryMethodOut);
    }
}
