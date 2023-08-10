using Assets.Codebase.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Gameplay.General.CanvasVersion
{
    public class CombinationField : MonoBehaviour
    {
        [SerializeField] private CombinationId _combinationId;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Image _cross;
        [SerializeField] private SetOfDices _setOfDices;

        private bool _isFilled;

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private void HideField()
        {
            if (!_isFilled)
            {

            }
        }
    }
}