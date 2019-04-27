using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Diagnostics;

public class HelperFunctions : MonoBehaviour {
	[MethodImpl(MethodImplOptions.NoInlining)]
	public static string GetCurrentMethod() {
		StackTrace st = new StackTrace();
		StackFrame sf = st.GetFrame(1);

		return sf.GetMethod().Name;
	}

	public static bool GetEventWithChance(int percent) {
		int number = Random.Range(1, 101);
		return number <= percent;
	}

	public static void Shuffle<T>(IList<T> list) {
		System.Random rng = new System.Random();
		int n = list.Count;
		while (n > 1) {
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	public void ChangeAlpha(MaskableGraphic img, float startAlpha, float endAlpha, float time) {
		LeanTween.value(img.gameObject, img.color.a, endAlpha, time)
		  .setOnStart(() => {
			  Color color = img.color;
			  color.a = startAlpha;
			  img.color = color;
		  })
		  .setOnUpdate((float alpha) => {
			  Color color = img.color;
			  color.a = alpha;
			  img.color = color;
		  });
	}
}
