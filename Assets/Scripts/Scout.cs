using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : MonoBehaviour
{
    private Animator _scoutAnimator;
    private float _deathAnimDuration = 0f;

    private void Awake() {
        initializeAnimParams();
    }

    private void Start() {
        
    }

    private void Update() {
        
    }

    private void initializeAnimParams() {
        _scoutAnimator = transform.GetComponent<Animator>();
        AnimationClip[] clips = _scoutAnimator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips) {
            if(clip.name == "Destruction") {
                _deathAnimDuration = clip.length;
            }
        }
    }
    private IEnumerator death() {
        _scoutAnimator.SetBool("isDead", true);
        foreach(Transform child in transform) {
            child.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(_deathAnimDuration);
        gameObject.SetActive(false);
    }
}
