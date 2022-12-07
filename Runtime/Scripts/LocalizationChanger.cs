using UnityEngine;

namespace Qplaze.DanPie.Localisation
{
    public class LocalizationChanger : MonoBehaviour
    {
        [SerializeField] private SystemLanguage _language;

        public void ChangeLanguage()
        {
            Localisation.LoadLanguage(_language);
        }
    }
}