using Cysharp.Threading.Tasks;
using Infrastructure;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Conveyor
{
    public class BigBoxView : MonoBehaviour
    {
        [SerializeField] private Image image;

        public readonly Subject<Unit> OnSelect = new();
        
        private IAssetProvider _assetProvider;

        [Inject]
        private void Construct(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void Select() => OnSelect.OnNext(Unit.Default);
        
        public async UniTask SetValue(ItemValue itemValue)
        {
            image.sprite = await _assetProvider.Load<Sprite>(itemValue.EmojiSprite);
        }
    }
}
