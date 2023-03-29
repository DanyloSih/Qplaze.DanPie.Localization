using UnityEngine;

namespace Qplaze.DanPie.Localization
{
    public abstract class TextLocalizator : MonoBehaviour
    {
        [SerializeField] private string _wordKey;

        protected abstract string Text { get; set; }

        protected void Awake()
        {
            Localization.LanguageChanged += OnLanguageChanged;
            OnAwake();
        }

        protected void OnDestroy()
        {
            Localization.LanguageChanged -= OnLanguageChanged;
            OnDestroying();
        }

        protected void OnEnable()
        {
            if (Localization.IsLoaded)
            {
                Text = Localization.GetString(_wordKey, gameObject);
            }
            OnEnabling();
        }

        protected virtual void OnDestroying() { }

        protected virtual void OnAwake() { }

        protected virtual void OnEnabling() { }

        private void OnLanguageChanged()
        {
            Text = Localization.GetString(_wordKey, gameObject);
        }
    } 
}