using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateTitle : MonoBehaviour
{

    [SerializeField] public Texture2D[] frames;
    private RawImage image;
    [SerializeField] private float frameDelay = .04f;
    private int index;

    private void Awake()
    {
        image = GetComponent<RawImage>();
        index = 0;
    }

    private void Start()
    {
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        index = ++index % frames.Length;
        yield return new WaitForSeconds(frameDelay);
        image.texture = frames[index];
        StartCoroutine(Animate());
    }

}
