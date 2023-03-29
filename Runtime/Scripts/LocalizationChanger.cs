using UnityEngine;

namespace Qplaze.DanPie.Localization
{
    public class LocalizationChanger : MonoBehaviour
    {
        [SerializeField] private SystemLanguage _language;

        public void ChangeLanguage()
        {
            Localization.LoadLanguage(_language);
        }
    }
}