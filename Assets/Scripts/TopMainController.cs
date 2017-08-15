using Eq.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TopMainController : BaseAndroidMainController {

    // Update is called once per frame
    internal override void Update()
    {
        base.Update();

        //if (Input.touchCount > 0)
        //{
        //    Touch lastTouch = Input.touches[0];
        //    if (lastTouch.phase == TouchPhase.Ended)
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(lastTouch.position);
        //        RaycastHit raycastHit = new RaycastHit();

        //        if (Physics.Raycast(ray, out raycastHit))
        //        {
        //            GameObject hitGameObject = raycastHit.collider.gameObject;
        //            if (hitGameObject != null)
        //            {
        //                TextMesh textMesh = hitGameObject.GetComponent<TextMesh>();
        //                PushNextScene(textMesh.text);
        //            }
        //        }
        //    }
        //}
    }

    public void MenuSpatialAudioClicked()
    {
        mLogger.CategoryLog(LogCategoryMethodIn);
        MenuClicked("SpatialAudio");
        mLogger.CategoryLog(LogCategoryMethodOut);
    }

    public void MenuSpatialAudioClicked(BaseEventData eventData)
    {
        mLogger.CategoryLog(LogCategoryMethodIn);
        MenuClicked("SpatialAudio");
        mLogger.CategoryLog(LogCategoryMethodOut);
    }

    public void AddSoundBox()
    {

    }

    private void MenuClicked(string menuAndNextSceneName)
    {
        mLogger.CategoryLog(LogCategoryMethodIn);
        PushNextScene(menuAndNextSceneName);
        mLogger.CategoryLog(LogCategoryMethodOut);
    }
}
