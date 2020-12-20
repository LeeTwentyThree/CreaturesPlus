using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ECCLibrary;
using MoreCreatures;
using QModManager.API;
using QModManager.API.ModLoading;
using UnityEngine;

namespace CreaturesPlus
{
    [QModCore]
    public static class QPatch
    {
        public static AssetBundle assetBundle;
        public static GargantuanLeviathanPrefab gargantuan;

        [QModPatch]
        public static void Patch()
        {
            assetBundle = ECCHelpers.LoadAssetBundleFromAssetsFolder(Assembly.GetExecutingAssembly(), "creaturesplusassets");
            ECCAudio.RegisterClips(assetBundle);

            gargantuan = new GargantuanLeviathanPrefab("GargantuanLeviathan", "Gargantuan Leviathan", "An ancient predator thought to be extinct", assetBundle.LoadAsset<GameObject>("GargantuanPrefab"), null);
            gargantuan.Patch();
        }
    }
}
