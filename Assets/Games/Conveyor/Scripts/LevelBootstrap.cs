using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Conveyor
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] LevelSetup levelSetup;
        [SerializeField] ItemOnConveyor itemOnConveyorPrefab;
        [SerializeField] TMP_Text leftText, rightText;
        [SerializeField] ConveyorPath conveyorPath;

        class Model
        {
            readonly ItemsFactory itemsFactory;
            
            Model(LevelBootstrap backing)
            {
                itemsFactory = new ItemsFactory(backing.itemOnConveyorPrefab);
                SetupTexts();
                
                void SetupTexts()
                {
                    SetValue(backing.leftText, backing.levelSetup._leftItem);
                    SetValue(backing.rightText, backing.levelSetup._rightItem);
                    
                    void SetValue(TMP_Text text, ItemValue item) => text.SetText(item._value.ToString());
                }
            }
        }
    }

    class ItemsFactory
    {
        readonly IObjectPool<ItemOnConveyor> pool;

        public ItemsFactory(ItemOnConveyor prefab)
        {
            pool = new ObjectPool<ItemOnConveyor>(
                createFunc: () => Object.Instantiate(prefab),
                actionOnGet: item => item.SetActive(),
                actionOnRelease: item => item.SetInactive(),
                defaultCapacity: 2
            );
        }

        public void CreateItem()
        {
            pool.Get();
        }
    }
}
