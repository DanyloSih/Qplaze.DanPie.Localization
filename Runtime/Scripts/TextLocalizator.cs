using UnityEngine;

namespace Qplaze.DanPie.Localisation
{
    public abstract class TextLocalizator : MonoBehaviour
    {
        [SerializeField] private string _wordKey;

        protected abstract string Text { get; set; }

        protected void Awake()
        {
            Localisation.LanguageChanged += OnLanguageChanged;
            OnAwake();
        }

        protected void OnDestroy()
        {
            Localisation.LanguageChanged -= OnLanguageChanged;
            OnDestroying();
        }

        protected void OnEnable()
        {
            if (Localisation.IsLoaded)
            {
                Text = Localisation.GetString(_wordKey, gameObject);
            }
            OnEnabling();
        }

        protected virtual void OnDestroying() { }

        protected virtual void OnAwake() { }

        protected virtual void OnEnabling() { }

        private void OnLanguageChanged()
        {
            Text = Localisation.GetString(_wordKey, gameObject);
        }
    } 
}