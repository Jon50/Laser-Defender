using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float _backgroundScrollSpeed = 0.05f;

    private Material _material;
    private Vector2 _offset;

    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<Renderer>().material;
        _offset = new Vector2(0, _backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        _material.mainTextureOffset += _offset * Time.deltaTime;
    }
}
