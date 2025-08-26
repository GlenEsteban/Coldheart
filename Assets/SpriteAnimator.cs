using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour {
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Material _baseMaterial;

    private void Awake () {
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private IEnumerator Start() {
        _baseMaterial = _spriteRenderer.material;

        _spriteRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        _spriteRenderer.material.color = Color.white;

        yield return new WaitForSeconds(3f);
        _spriteRenderer.material = _baseMaterial;
    }
}
