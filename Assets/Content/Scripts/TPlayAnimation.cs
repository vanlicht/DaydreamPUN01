using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TPlayAnimation : Photon.MonoBehaviour, IPunObservable
{
    public anim[] animations;
    //public GameObject[] showObjects;
    //public GameObject[] hideObjects;
    int animLength;
    //int showLength;
    //int hideLength;
    int animCount;
    //int showCount;
    //int hideCount;

    public Text engineText;

    void Start()
    {
        animLength = animations.Length;
        //showLength = showObjects.Length;
        //hideLength = hideObjects.Length;
        animCount = -1;
        //showCount = 0;
        //hideCount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            T_OnClick();
        }
        engineText.text = "animCount: " + animCount;
    }

    public void T_OnClick()
    {
        
        if (photonView.isMine)
        {
            
            if (animCount < animLength)
            {
                animCount += 1;
                if (animCount == animLength)
                {
                    animCount = 0;
                }
            }
        }
    }

    [PunRPC]
    public void RPC_OnClick()
    {
        Debug.Log("animations.Length: " + animCount);

        if (animations[animCount].animator != null && animations[animCount].animStateName != null)
        {
            if (animCount >= 0)
            {
                animations[animCount].animator.Play(animations[animCount].animStateName);
            }
            
        }


        //if(showObjects[showCount] != null)
        //{
        //    showObjects[showCount].SetActive(true);
        //}
        //if(hideObjects[hideCount] != null)
        //{
        //    hideObjects[hideCount].SetActive(false);
        //}


        //showCount += 1;
        //hideCount += 1;
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (photonView.isMine)
        {
            
            stream.SendNext(animCount);
            this.photonView.RPC("RPC_OnClick", PhotonTargets.AllBufferedViaServer);

        }
        else
        {
            animCount = (int)stream.ReceiveNext();  
        }
    }

    [System.Serializable]
    public struct anim
    {
        public Animator animator;
        public string animStateName;
    }
}
