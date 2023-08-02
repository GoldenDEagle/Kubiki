using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Gameplay.General.CanvasVersion
{
    public class Dice : MonoBehaviour
    {
        [SerializeField] private Toggle _diceToggle;
        [SerializeField] private Image _diceImage;
        [Tooltip("Time between animation frames in seconds.")]
        [SerializeField] private float _animationTimeStep = 0.5f;
        [Tooltip("Strictly in order.")]
        [SerializeField] private List<Sprite> _diceSprites;

        private Coroutine _spinRoutine;
        private int _numberOfImages;
        private WaitForSeconds _animationStep;

        private void Start()
        {
            _numberOfImages = _diceSprites.Count;
            _animationStep = new WaitForSeconds(_animationTimeStep);
        }

        [ContextMenu("Roll")]
        public void Roll()
        {
            if (_spinRoutine != null)
            {
                StopCoroutine(_spinRoutine);
                _spinRoutine = null;
            }

            _spinRoutine = StartCoroutine(Spinning());
        }

        private IEnumerator Spinning()
        {
            int previouIndex = 0;
            while (true)
            {
                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, _numberOfImages);
                    _diceImage.sprite = _diceSprites[randomIndex];
                }
                while (previouIndex == randomIndex);

                previouIndex = randomIndex;
                yield return _animationStep;
            }
        }
    }
}