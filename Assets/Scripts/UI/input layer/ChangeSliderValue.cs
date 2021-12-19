using NoodleEater.Caravan.System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace NoodleEater.Caravan
{
    public class ChangeSliderValue : MonoBehaviour
    {
        public Text SfxText;
        public Slider sfxSlider;
        public Text bgmText;
        public Slider bgmSlider;

        private InputLayerSystem _baseInputLayerSystem;

        private InputLayer _currentLayer = InputLayer.SliderSfxUI;
        
        void Start()
        {
            _baseInputLayerSystem = GetComponent<InputLayerSystem>();
            
            _baseInputLayerSystem.BindAction(InputLayer.SliderSfxUI, SetSFXVolume);
            _baseInputLayerSystem.BindAction(InputLayer.SliderBgmUI, SetBGMVolume);
            
            _baseInputLayerSystem.OnLayerChange += (layer) =>
            {
                if (layer == InputLayer.SliderSfxUI)
                {
                    bgmText.color = Color.black;
                    SfxText.color = Color.white;
                }

                if (layer == InputLayer.SliderBgmUI)
                {
                    bgmText.color = Color.white;
                    SfxText.color = Color.black;
                }
            };
            
            _baseInputLayerSystem.EnableLayer(InputLayer.SliderSfxUI);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && 
                _currentLayer == InputLayer.SliderBgmUI)
            {
                _currentLayer = InputLayer.SliderSfxUI;
                _baseInputLayerSystem.EnableLayer(InputLayer.SliderBgmUI);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && 
                _currentLayer == InputLayer.SliderSfxUI)
            {
                _currentLayer = InputLayer.SliderBgmUI;
                _baseInputLayerSystem.EnableLayer(InputLayer.SliderSfxUI);
            }
        }

#if UNITY_EDITOR
        [MenuItem("Test/Layer")]
        private static void Test()
        {
            InputLayer layer = InputLayer.ButtonNewGame;
            Debug.Log((layer - 1));
        }
#endif

        private void SetSFXVolume()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                sfxSlider.value = Mathf.MoveTowards(sfxSlider.value, sfxSlider.maxValue, Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                sfxSlider.value = Mathf.MoveTowards(sfxSlider.value, sfxSlider.minValue, Time.deltaTime);
            }
        }

        private void SetBGMVolume()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                bgmSlider.value = Mathf.MoveTowards(bgmSlider.value, bgmSlider.maxValue, Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                bgmSlider.value = Mathf.MoveTowards(bgmSlider.value, bgmSlider.minValue, Time.deltaTime);
            }
        }
    }
}
