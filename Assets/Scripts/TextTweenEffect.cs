using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTweenEffect : MonoBehaviour
{
    public bool ignoreTimeScale;

    void Start()
    {
        LeanTween.scale(this.gameObject, transform.localScale * 0.98f, 0.5f).setLoopPingPong().setIgnoreTimeScale(ignoreTimeScale);
    }

}
