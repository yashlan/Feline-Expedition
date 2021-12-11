using System;
using System.Collections.Generic;
using UnityEngine;

namespace NoodleEater.Caravan.System
{
    public class InputLayerSystem : MonoBehaviour
    {
        private readonly Dictionary<InputLayer, Action> _inputActions = new Dictionary<InputLayer, Action>();

        private InputLayer _currentLayer = InputLayer.None;

        public Action<InputLayer> OnLayerChange;

        private void Update()
        {
            if (_inputActions.Count == 0 || _currentLayer == InputLayer.None)
            {
                return;
            }
            
            _inputActions[_currentLayer]?.Invoke();
        }

        public void BindAction(InputLayer layer, Action inputAction)
        {
            if (_inputActions.ContainsKey(layer))
            {
                Debug.LogWarning($"Input Layer {layer} already exist.");
                return;
            }
            
            _inputActions.Add(layer, inputAction);
        }

        public void EnableLayer(InputLayer layer)
        {
            _currentLayer = layer;
            OnLayerChange?.Invoke(_currentLayer);
        }
    }
}
