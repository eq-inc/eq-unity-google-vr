using Eq.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpatialAudioController : BaseAndroidMainController
{
    public GameObject mSoundBoxPrefab;
    private BaseInput mInput;

    internal override void Start()
    {
        base.Start();
        GameObject eventSystem = GameObject.Find("GvrEventSystem");
        mInput = eventSystem.GetComponent<GvrPointerInputModule>().input;
    }

    internal override void Update()
    {
        base.Update();
        ManageTouch();
    }

    public void AddSoundBoxButtonClicked()
    {
        Vector3 positionWp = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        GameObject soundBoxGameObject = Instantiate(mSoundBoxPrefab, positionWp, Quaternion.identity);
        soundBoxGameObject.SetActive(true);

        GvrAudioSource audioSource = soundBoxGameObject.GetComponent<GvrAudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void AddSoundBox()
    {
        mLogger.CategoryLog(LogCategoryMethodIn);

        Vector3 positionWp = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        GameObject soundBoxGameObject = Instantiate(mSoundBoxPrefab, positionWp, Quaternion.identity);
        soundBoxGameObject.SetActive(true);

        GvrAudioSource audioSource = soundBoxGameObject.GetComponent<GvrAudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }

        mLogger.CategoryLog(LogCategoryMethodOut);
    }

    private void ManageTouch()
    {
        mLogger.CategoryLog(LogCategoryMethodIn, "touch count = " + mInput.touchCount + ", touching = " + GvrController.IsTouching);
        if (mInput.touchCount > 0)
        {
            Touch lastTouch = mInput.GetTouch(0);
            mLogger.CategoryLog(LogCategoryMethodTrace, "touch phase = " + lastTouch.phase);
            if (lastTouch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(lastTouch.position);
                RaycastHit raycastHit = new RaycastHit();

                if (Physics.Raycast(ray, out raycastHit))
                {
                    mLogger.CategoryLog(LogCategoryMethodTrace, "hit ray");

                    GvrAudioSource audioSource = raycastHit.collider.gameObject.GetComponent<GvrAudioSource>();
                    if (audioSource.isPlaying)
                    {
                        audioSource.Stop();
                    }
                    else
                    {
                        audioSource.Play();
                    }
                }
                else
                {
                    mLogger.CategoryLog(LogCategoryMethodTrace, "hit ray");

                    AddSoundBox();
                }
            }
        }
        else if (GvrController.IsTouching)
        {
            mLogger.CategoryLog(LogCategoryMethodTrace, "touch down = " + GvrController.TouchDown + ", up = " + GvrController.TouchUp);
            if (GvrController.TouchDown)
            {
                Vector2 touchPosition = GvrController.TouchPos;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(touchPosition.x, 0, touchPosition.y));
                RaycastHit raycastHit = new RaycastHit();

                if (Physics.Raycast(ray, out raycastHit))
                {
                    mLogger.CategoryLog(LogCategoryMethodTrace, "hit ray");

                    GvrAudioSource audioSource = raycastHit.collider.gameObject.GetComponent<GvrAudioSource>();
                    if (audioSource.isPlaying)
                    {
                        audioSource.Stop();
                    }
                    else
                    {
                        audioSource.Play();
                    }
                }
                else
                {
                    mLogger.CategoryLog(LogCategoryMethodTrace, "hit ray");

                    AddSoundBox();
                }
            }
        }
        mLogger.CategoryLog(LogCategoryMethodOut);
    }
}
