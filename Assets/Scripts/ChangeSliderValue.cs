using System;
using System.Collections;
using System.Collections.Generic;
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

        private InputLayer _currentLayer = InputLayer.BgmUI;
        
        // Start is called before the first frame update
        void Start()
        {
            _baseInputLayerSystem = GetComponent<InputLayerSystem>();
            
            _baseInputLayerSystem.BindAction(InputLayer.SfxUI, SetSFXVolume);
            _baseInputLayerSystem.BindAction(InputLayer.BgmUI, SetBGMVolume);
            
            _baseInputLayerSystem.OnLayerChange += (layer) =>
            {
                if (layer == InputLayer.SfxUI)
                {
                    bgmText.color = Color.black;
                    SfxText.color = Color.red;
                }

                if (layer == InputLayer.BgmUI)
                {
                    bgmText.color = Color.red;
                    SfxText.color = Color.black;
                }
            };
            
            _baseInputLayerSystem.EnableLayer(InputLayer.SfxUI);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && _currentLayer == InputLayer.BgmUI)
            {
                _currentLayer = InputLayer.SfxUI;
                _baseInputLayerSystem.EnableLayer(InputLayer.BgmUI);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && _currentLayer == InputLayer.SfxUI)
            {
                _currentLayer = InputLayer.BgmUI;
                _baseInputLayerSystem.EnableLayer(InputLayer.SfxUI);
            }
        }

/*        [MenuItem("Test/Layer")]
        private static void Test()
        {
            InputLayer layer = InputLayer.SfxUI;
            Debug.Log((layer - 1));
        }*/

        public void SetSFXVolume()
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

        public void SetBGMVolume()
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
