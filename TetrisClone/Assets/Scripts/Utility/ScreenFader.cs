using UnityEngine.UI;
using UnityEngine;

using System.Collections;

//MaskableGraphic 可以是 Image 或 Text
[RequireComponent(typeof(MaskableGraphic))]
public class ScreenFader : MonoBehaviour {

	public float startAlpha = 1f;
	public float targetAlpha = 0f;
	public float delay = 0f;
	public float timeToFade = 1f;

	//每一帧的改变量
	float increase;

	float currentAlpha;

	MaskableGraphic graphic;
	Color originalColor;

	// Use this for initialization
	void Start () {
		//获取当前对象
		graphic = GetComponent<MaskableGraphic> ();
		originalColor = graphic.color;

		currentAlpha = startAlpha;
		//在原有颜色的基础上应用当前的透明度
		Color tempColor = new Color (originalColor.r, originalColor.g, originalColor.b, currentAlpha);
		graphic.color = tempColor;

		//计算每一帧需要改变的量
		//Time.deltaTime 使得 second 单位转换为 frame 单位 
		//units/second * Time.deltaTime = units / frame
		increase = ((targetAlpha - startAlpha) / timeToFade) * Time.deltaTime;

		StartCoroutine (FadeRoutine ());
	}
	
	// Update is called once per frame
	IEnumerator FadeRoutine () {
		yield return new WaitForSeconds (delay);

		while (Mathf.Abs (targetAlpha - currentAlpha) > 0.01f) {
			yield return new WaitForEndOfFrame ();

			currentAlpha = currentAlpha + increase;

			Color tempColor = new Color (originalColor.r, originalColor.g, originalColor.b, currentAlpha);
			graphic.color = tempColor;
		}


	}
}
