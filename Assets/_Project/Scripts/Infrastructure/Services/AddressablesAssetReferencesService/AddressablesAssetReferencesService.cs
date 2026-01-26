using System.Collections.Generic;
using CodeBase.Configs;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services
{
    public class AddressablesAssetReferencesService : IAddressablesAssetReferencesService
    {
        private readonly AddressablesAssetReferencesData _assetReferencesData;

        public AddressablesAssetReferencesService(AddressablesAssetReferencesData assetReferencesData)
        {
            _assetReferencesData = assetReferencesData;
        }
        
        public IReadOnlyList<AssetReference> LevelReferences =>
            _assetReferencesData.LevelReferences;
    }
}   