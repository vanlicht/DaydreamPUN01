using UnityEngine;
using System.Collections;

public class IconMovement : MonoBehaviour
{
    #region public variables
    public GameObject startReference;
    public Vector3 startScale = Vector3.one;
    public Vector3 endScale = Vector3.one;
    public AnimationCurve animCurve;
    public float delayTime;
    public float AnimationTime;
    #endregion

    #region private vairables
    private GameObject endReference;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float curveTime;
    private float timer = 0f;

    private bool isActive;
    #endregion


    private void Awake()
    {
        //Create end point reference object
        endReference = new GameObject(this.name + "_end");
        endReference.transform.position = this.gameObject.transform.position;
        endReference.transform.rotation = this.gameObject.transform.rotation;
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        isActive = true;

        //Start position
        startPosition = startReference.transform.position;

        //set the target to start position
        transform.position = startPosition;

        //end position
        endPosition = endReference.transform.position;

        transform.localScale = Vector3.zero;
        StartCoroutine(showIconAnim());
        StopCoroutine(showIconAnim());
    }

    public void disableSelf()
    {
        if (this.gameObject.GetActive())  //this if is to avoid coroutine error due to the game object is not active
        {
            isActive = false;
            transform.position = endReference.transform.position;
            StartCoroutine(showIconAnim());
            StopCoroutine(showIconAnim());
            //Debug.Log("...............IconMovement 01");
        }
        
    }
    IEnumerator showIconAnim()
    {
        yield return new WaitForSeconds(delayTime);

        timer = 0;
        //Debug.Log("...............IconMovement 02");


        do
        {
            float percent = timer / AnimationTime;
            curveTime = animCurve.Evaluate(percent);
            if (isActive)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, curveTime);
                transform.localScale = Vector3.Lerp(startScale, endScale, curveTime);
                //Debug.Log("...............IconMovement 03");
            }
            else
            {
                transform.position = Vector3.Lerp(endPosition, startPosition, curveTime);
                transform.localScale = Vector3.Lerp(endScale, startScale, curveTime);
                //Debug.Log("...............IconMovement 04");
            }

            yield return null;
            timer += Time.deltaTime;
        } while (timer < AnimationTime);
        if (isActive)
        {
            transform.position = endPosition;
            transform.localScale = endScale;
            //Debug.Log("...............IconMovement 05");
        }
        else
        {
            transform.position = startPosition;
            transform.localScale = startScale;

            this.gameObject.SetActive(false);
            //Debug.Log("...............IconMovement 06");
            
        }
        yield break;
    }
}
