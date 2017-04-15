using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class IconToggle : MonoBehaviour {

	public Sprite iconTrue;
	public Sprite iconFalse;

	public bool defaultIconState = true;

	Image image;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		image.sprite = defaultIconState ? iconTrue : iconFalse;
	}
	
	// Update is called once per frame
	public void ToggleIcon (bool state) {
		if (!image || !iconTrue || !iconFalse) {
			return;
		}
		image.sprite = state ? iconTrue : iconFalse;
	}
}
