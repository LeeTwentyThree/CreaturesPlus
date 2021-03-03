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

            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gargantuan, new Vector3(-1449, -200f, 715f), "OldGargDunes", 400f));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gargantuan, new Vector3(1459, -200f, -789f), "OldGargBehindAurora", 400f));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gargantuan, new Vector3(1130, -200f, 1760), "OldGargMountains", 400f));
            StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(gargantuan, new Vector3(-834, -200f, -1670), "OldGargGrandReef", 400f));
        }
    }
}
