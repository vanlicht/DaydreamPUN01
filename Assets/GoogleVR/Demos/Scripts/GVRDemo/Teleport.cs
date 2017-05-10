// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Collider))]
public class Teleport : Photon.MonoBehaviour, IPunObservable
{
  private Vector3 startingPosition;

  public Material inactiveMaterial;
  public Material gazedAtMaterial;

    private AudioSource audioSrc;
    public bool isAudioPlay;

  void Start() {
    startingPosition = transform.localPosition;
    SetGazedAt(false);

        audioSrc = this.GetComponent<AudioSource>();
  }
    private void Update()
    {
        PlayCubeSound(isAudioPlay);
    }
    public void SetGazedAt(bool gazedAt) {
    if (inactiveMaterial != null && gazedAtMaterial != null) {
      GetComponent<Renderer>().material = gazedAt ? gazedAtMaterial : inactiveMaterial;
      return;
    }
    GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
  }

  public void Reset() {
    transform.localPosition = startingPosition;
  }

  public void TeleportRandomly()
    {
        if (!photonView.isMine)
        {
            return;


        }
        Vector3 direction = UnityEngine.Random.onUnitSphere;
        direction.y = Mathf.Clamp(direction.y, 0.5f, 1f);
        float distance = 2 * UnityEngine.Random.value + 1.5f;
        transform.localPosition = direction * distance;
  }

    void PlayCubeSound(bool isAudioPlay)
    {
        if (isAudioPlay)
        {
            audioSrc.Play();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(isAudioPlay);
        }
        else
        {
            isAudioPlay = (bool)stream.ReceiveNext();
        }
    }
}
