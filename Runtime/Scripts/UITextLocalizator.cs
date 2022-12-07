using UnityEngine;
using UnityEngine.UI;

namespace Qplaze.DanPie.Localisation
{
    [RequireComponent(typeof(Text))]
    public class UITextLocalizator : TextLocalizator
    {
        private Text _UIText;

        protected override string Text { get => _UIText.text; set => _UIText.text = value; }

        protected override void OnAwake()
        {
            _UIText = GetComponent<Text>();
        }
    }
}
