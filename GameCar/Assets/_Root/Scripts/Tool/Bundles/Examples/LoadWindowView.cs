using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

namespace Tool.Bundles.Examples
{
    internal class LoadWindowView : AssetBundleViewBase
    {
        [Header("Asset Bundles")]
        [SerializeField] private Button _loadAssetsButton;
        [SerializeField] private Button _changeBackgroundButton;

        [Header("Addressables")]
        [SerializeField] private AssetReference _spawningButtonPrefab;
        [SerializeField] private RectTransform _spawnedButtonsContainer;
        [SerializeField] private Button _spawnAssetButton;

        [Header("Addressables Backgrond")]
        [SerializeField] private AssetReference _background;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Button _addBackgroundButton;
        [SerializeField] private Button _removeBackgroundButton;

        private readonly List<AsyncOperationHandle<GameObject>> _addressablePrefabs =
            new List<AsyncOperationHandle<GameObject>>();

        private AsyncOperationHandle<Sprite> _loadBackground;


        private void Start()
        {
            _loadAssetsButton.onClick.AddListener(LoadAssets);
            _changeBackgroundButton.onClick.AddListener(ChangeBackground);

            _spawnAssetButton.onClick.AddListener(SpawnPrefab);
            _addBackgroundButton.onClick.AddListener(AddBackground);
            _removeBackgroundButton.onClick.AddListener(DeletBackground);
        }


        private void OnDestroy()
        {
            _loadAssetsButton.onClick.RemoveAllListeners();
            _changeBackgroundButton.onClick.RemoveAllListeners();

            _spawnAssetButton.onClick.RemoveAllListeners();
            _addBackgroundButton.onClick.RemoveAllListeners();
            _removeBackgroundButton.onClick.RemoveAllListeners();

            DespawnPrefabs();
            DeletBackground();
        }


        private void LoadAssets()
        {
            _loadAssetsButton.interactable = false;
            StartCoroutine(DownloadAndSetSpriteAssetBundles());
            StartCoroutine(DownloadAndSetAudioAssetBundles());
        }


        private void ChangeBackground()
        {
            _changeBackgroundButton.interactable = false;
            StartCoroutine(DownloadAndSetBackgroundAssetBundles());
        }


        private void SpawnPrefab()
        {
            AsyncOperationHandle<GameObject> addressablePrefab =
                Addressables.InstantiateAsync(_spawningButtonPrefab, _spawnedButtonsContainer);

            _addressablePrefabs.Add(addressablePrefab);
        }

        private void DespawnPrefabs()
        {
            foreach (AsyncOperationHandle<GameObject> addressablePrefab in _addressablePrefabs)
                Addressables.ReleaseInstance(addressablePrefab);

            _addressablePrefabs.Clear();
        }


        private void AddBackground()
        {
            if (!_loadBackground.IsValid())
            {
                _loadBackground = Addressables.LoadAssetAsync<Sprite>(_background);
                _loadBackground.Completed += OnBackgroundLoaded;
            }
        }

        private void DeletBackground()
        {
            if (_loadBackground.IsValid())
            {
                _loadBackground.Completed -= OnBackgroundLoaded;
                Addressables.Release(_loadBackground);
                SetBackground(null);
            }
        }

        private void OnBackgroundLoaded(AsyncOperationHandle<Sprite>  asyncOperationHandle)
        {
            asyncOperationHandle.Completed -= OnBackgroundLoaded;
            SetBackground(asyncOperationHandle.Result);
        }

        private void SetBackground(Sprite sprite) =>
            _backgroundImage.sprite = sprite;
    }
}
