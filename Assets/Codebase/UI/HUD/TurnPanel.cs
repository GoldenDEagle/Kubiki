using TMPro;
using UnityEngine;

namespace Assets.Codebase.UI.HUD
{
    public class TurnPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _activePlayerName;
        [SerializeField] private TMP_Text _throwCounter;

        public void UpdateThrowCounter(int throwNumber)
        {
            _throwCounter.text = throwNumber.ToString() + " / 3";
        }
    }
}