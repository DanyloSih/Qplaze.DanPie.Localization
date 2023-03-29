using UnityEngine;
using TMPro;

namespace Qplaze.DanPie.Localization
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class TextMeshProUGUILocalizator : TextLocalizator
	{
		private TextMeshProUGUI _textMesh;

		protected override string Text { get => _textMesh.text; set => _textMesh.text = value; }

		protected override void OnAwake()
		{
			_textMesh = GetComponent<TextMeshProUGUI>();
		}
	} 
}
