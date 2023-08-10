using Assets.Codebase.UI.HUD;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Gameplay.General.CanvasVersion
{
    public class SetOfDices : MonoBehaviour
    {
        [SerializeField] private TurnPanel _turnPanel;
        [SerializeField] private List<Dice> _dices;

        private List<Dice> _rollingDices;
        private int _currentThrowNumber = 0;

        public event Action OnRollStarted;
        public event Action OnRollEnded;

        public int CurrentThrowNumber => _currentThrowNumber;

        private void Start()
        {
            _rollingDices = new List<Dice>();
        }

        public void MakeATrow()
        {
            _rollingDices.Clear();

            OnRollStarted?.Invoke();

            // roll the dices (first throw special)
            if (_currentThrowNumber == 0)
            {
                foreach (Dice d in _dices)
                {
                    _rollingDices.Add(d);
                    d.Roll();
                }
            }
            else
            {
                foreach (Dice d in _dices)
                {
                    if (d.IsSelected())
                    {
                        _rollingDices.Add(d);
                        d.Roll();
                    }
                }
            }

            StartCoroutine(StopSpinningAfterDelay());
        }

        private IEnumerator StopSpinningAfterDelay()
        {
            foreach (Dice d in _rollingDices)
            {
                yield return new WaitForSeconds(1f);
                d.StopTheRoll(_currentThrowNumber);
            }

            _currentThrowNumber++;
            _turnPanel.UpdateThrowCounter(_currentThrowNumber);

            OnRollEnded?.Invoke();
        }

        public void ResetThrowNumber()
        {
            _currentThrowNumber = 0;
            _turnPanel.UpdateThrowCounter(_currentThrowNumber);
        }
    }
}