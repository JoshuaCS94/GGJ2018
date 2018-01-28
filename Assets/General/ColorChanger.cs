using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
	public Color color;
	public float TimeToNextColor;
	private List<SpriteRenderer> items;

	private void Awake()
	{
		items = new List<SpriteRenderer>();
		color = new Color(Random.value, Random.value, Random.value);
		var bleah = FindObjectsOfType<SpriteRenderer>();
		var floor = LayerMask.NameToLayer("Floor");
		var danger = LayerMask.NameToLayer("TouchDanger");
		var hammer = LayerMask.NameToLayer("Hammer");

		for (int i = 0; i < bleah.Length; i++)
		{
			var layer = bleah[i].gameObject.layer;
			if(layer == floor || layer == danger || layer == hammer) items.Add(bleah[i]);
		}
		items.Add(GetComponent<SpriteRenderer>());
		StartCoroutine(ChangeColorsCoroutine());
	}

	IEnumerator ChangeColorsCoroutine()
	{
		while (true)
		{
			color = new Color(Random.value, Random.value, Random.value);
			while(color.r + color.g + color.b < 1.5)
				color = new Color(Random.value, Random.value, Random.value);
			foreach (var item in items)
			{
				item.DOColor(color, TimeToNextColor/2);
			}
			yield return new WaitForSeconds(TimeToNextColor);
		}
	}

}
