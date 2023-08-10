using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
        private bool _isRolling = false;
        private int _currentValue;

        public event Action OnDiceRollEnded;

        public bool IsRolling => _isRolling;
        public int CurrentValue => _currentValue;

        private void Start()
        {
            _numberOfImages = _diceSprites.Count;
            _animationStep = new WaitForSeconds(_animationTimeStep);
        }

        public bool IsSelected() => _diceToggle.isOn;

        [ContextMenu("Roll")]
        public void Roll()
        {
            _isRolling = true;
            _diceToggle.isOn = false;
            _diceToggle.interactable = false;

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
            while (_isRolling)
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

        public void StopTheRoll(int rollNumber)
        {
            _isRolling = false;
            _currentValue = Random.Range(1, 7);
            _diceImage.sprite = _diceSprites[_currentValue - 1];

            if (rollNumber < 2)
            {
                _diceToggle.interactable = true;
            }
        }
    }
}