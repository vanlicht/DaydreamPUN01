using UnityEngine;
using System.Collections;

public class IconMovement : MonoBehaviour
{
    public GameObject startReference;
    GameObject endReference;

    public Vector3 startScale = Vector3.one;
    public Vector3 endScale = Vector3.one;

    public AnimationCurve animCurve;

    public float delayTime;
    public float AnimationTime;

    Vector3 startPosition;
    Vector3 endPosition;
    float curveTime;
    float timer = 0f;

    bool isActive;

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
        endPosition = endReference.transform.position;
        transform.position = startReference.transform.position;
        transform.localScale = Vector3.zero;
        StartCoroutine(showIconAnim());
        StopCoroutine(showIconAnim());
    }

    public void disableSelf()
    {
        if (this.gameObject.GetActive())
        {
            isActive = false;
            StartCoroutine(showIconAnim());
            StopCoroutine(showIconAnim());
        }
    }
    IEnumerator showIconAnim()
    {
        yield return new WaitForSeconds(delayTime);

        timer = 0;

        //Start position
        startPosition = startReference.transform.position;

        do
        {
            float percent = timer / AnimationTime;
            curveTime = animCurve.Evaluate(percent);
            if (isActive)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, curveTime);
                transform.localScale = Vector3.Lerp(startScale, endScale, curveTime);
            }
            else
            {
                transform.position = Vector3.Lerp(endPosition, startPosition, curveTime);
                transform.localScale = Vector3.Lerp(endScale, startScale, curveTime);
            }
            
            yield return null;
            timer += Time.deltaTime;
        } while (timer < AnimationTime);
        if(isActive)
        {
            transform.position = endPosition;
            transform.localScale = endScale;
        }
        else
        {
            transform.position = startPosition;
            transform.localScale = startScale;
            this.gameObject.SetActive(false);
        }
        
        yield break;
    }
}
