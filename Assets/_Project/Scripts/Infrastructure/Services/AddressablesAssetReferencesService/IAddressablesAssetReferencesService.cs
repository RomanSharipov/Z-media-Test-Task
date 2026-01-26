using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services
{
    public interface IAddressablesAssetReferencesService
    {
        public IReadOnlyList<AssetReference> LevelReferences { get; }
    }
}