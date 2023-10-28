using UnityEngine.AddressableAssets;

namespace Infrastructure
{
    public class SceneLoader
    {
        public void LoadScene(AssetReference assetReference)
        {
            assetReference.LoadSceneAsync();
        }
    }
}