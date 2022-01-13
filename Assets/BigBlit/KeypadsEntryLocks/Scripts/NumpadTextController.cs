using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using BigBlit.ActivePack;

namespace BigBlit.Keypads
{
    public class NumpadTextController : ActiveObject
    {

        #region FIELDS AND PROPERTIES


        [SerializeField] bool _showPrompt;
        [SerializeField] float _blinkInterval;

        [SerializeField] string _promptChar;

        [SerializeField] bool _showPassword;
        [SerializeField] string _maskChar;

        [SerializeField] NumPad _numPad;
        [SerializeField] NumpadText _numPadText;


        private float _blinkAge;
        private bool _isPrompt;

        public NumPad NumPad {
            get => _numPad;
            set {
                if (_numPad == value)
                    return;
                if (_numPad != null)
                    _numPad.RemoveValueChangedListener(onNumPadValueChanged);
                _numPad = value;
                if (_numPad != null)
                    _numPad.RegisterValueChangedListener(onNumPadValueChanged);
            }
        }
        #endregion

        #region UNITY EVENTS

        protected override void OnValidate() {
            base.OnValidate();
            if (_maskChar.Length > 1)
                _maskChar = _maskChar.Substring(_maskChar.Length - 1, 1);
            if (_promptChar.Length > 1)
                _promptChar = _promptChar.Substring(_promptChar.Length - 1, 1);

        }

        protected override void Awake() {
            base.Awake();

            if (_numPad == null)
                _numPad = GetComponent<NumPad>();
            if (_numPadText == null)
                _numPadText = GetComponent<NumpadText>();

        }

        protected override void OnEnable() {
            base.OnEnable();

            if (_numPad != null)
                _numPad.RegisterValueChangedListener(onNumPadValueChanged);

            updateView();

        }

        protected override void OnDisable() {
            base.OnDisable();

            if (_numPad != null)
                _numPad.RemoveValueChangedListener(onNumPadValueChanged);
        }
        protected void Update() {
            updatePrompt();
        }
        #endregion

        #region PRIVATE METHODS


        private void onNumPadValueChanged(INumPad numPad) {
            _blinkAge = 0.0f;
            _isPrompt = true;
            updateView();
        }

        private void updateView() {
            if (_numPad == null || _numPadText == null)
                return;

            _numPadText.CellsNum = _numPad.MaxLength;

            if (_showPassword) {
                _numPadText.Text = _numPad.Value;
            }
            else {
                if (string.IsNullOrEmpty(_maskChar)) {
                    _maskChar = "*";
                }
                _numPadText.Text = new string(_maskChar[0], _numPad.Value.Length);
            }


            if (_isPrompt && _showPrompt && _numPadText.CellsNum > _numPadText.Text.Length) {
                _numPadText.Text += _promptChar;
            }
        }

        private void updatePrompt() {
            _blinkAge += Time.deltaTime;
            if (_blinkAge >= _blinkInterval) {
                _blinkAge = 0.0f;
                _isPrompt = !_isPrompt;
                updateView();
            }
        }
        #endregion


    }
}