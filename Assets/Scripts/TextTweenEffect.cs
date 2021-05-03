using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTweenEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(this.gameObject, transform.localScale * 0.98f, 0.5f).setLoopPingPong();
    }

}
