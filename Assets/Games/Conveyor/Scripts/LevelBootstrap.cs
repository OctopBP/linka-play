using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Conveyor
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private LevelSetup levelSetup;
        [SerializeField] private ItemOnConveyor itemOnConveyorPrefab;
        [SerializeField] private TMP_Text leftText, rightText;

        private class Model
        {
            private readonly ItemsFactory _itemsFactory;

            private Model(LevelBootstrap backing)
            {
                _itemsFactory = new ItemsFactory(backing.itemOnConveyorPrefab);
                SetupTexts();

                void SetupTexts()
                {
                    SetValue(backing.leftText, backing.levelSetup.LeftItem);
                    SetValue(backing.rightText, backing.levelSetup.RightItem);

                    void SetValue(TMP_Text text, ItemValue item) => text.SetText(item.Value.ToString());
                }
            }
        }
    }

    class ItemsFactory
    {
        private readonly IObjectPool<ItemOnConveyor> _pool;

        public ItemsFactory(ItemOnConveyor prefab)
        {
            _pool = new ObjectPool<ItemOnConveyor>(
                createFunc: () => Object.Instantiate(prefab),
                actionOnGet: item => item.SetActive(),
                actionOnRelease: item => item.SetInactive(),
                defaultCapacity: 2
            );
        }

        public void CreateItem()
        {
            _pool.Get();
        }
    }
}
