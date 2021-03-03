using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Utility;
using UnityEngine;
using System.Reflection;
using ECCLibrary;
using SMLHelper.V2.Handlers;
using UWE;
using CreaturesPlus;

namespace MoreCreatures
{
    public class GargantuanLeviathanPrefab : CreatureAsset
    {
        public GargantuanLeviathanPrefab(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {

        }

        public override bool CanBeInfected => false;

        public override WaterParkCreatureParameters WaterParkParameters => new WaterParkCreatureParameters(0.002f, 0.015f, 0.015f, 3f, false);
        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.VeryFar;

        public override EcoTargetType EcoTargetType => EcoTargetType.Leviathan;

        public override BehaviourLODLevelsStruct BehaviourLODSettings => new BehaviourLODLevelsStruct(200f, 400f, 1000f);

        public override float Mass => 100000f;

        public override void SetLiveMixinData(ref LiveMixinData data)
        {
            data.knifeable = true;
            data.maxHealth = float.MaxValue;
        }

        public override float EyeFov => -1f;

        public override TechType CreatureTraitsReference => TechType.ReaperLeviathan;

        public override bool EnableAggression => true;

        public override RoarAbilityData RoarAbilitySettings => new RoarAbilityData(true, 60f, 800f, "GargantuanRoar", "roar", true, 50f, 0f);

        public override float TurnSpeed => 0.1f;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(120f, 20f, 120f), 10f, 3f, 0.5f);

        public override SmallVehicleAggressivenessSettings AggressivenessToSmallVehicles => new SmallVehicleAggressivenessSettings(1f, 75f);

        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.95f, 30f, 18f, 22f, 20f, 100f);

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(1f, false, 25f);

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 8f, "Lifeforms/Fauna/Leviathans", new string[] { "Lifeforms", "Fauna", "Leviathans" }, null, null);

        public override BehaviourType BehaviourType => BehaviourType.Leviathan;

        public override string GetEncyTitle => "Gargantuan Leviathan";

        public override string GetEncyDesc => "Thought to be extinct, this leviathan-class specimen appears to have recently migrated to this location from another environment on this planet.\n\n1. Size:\nAt a length of nearly 900 meters, the Gargantuan Leviathan is the largest known living creature on Planet 4546B. Already seeming biologically impossible, this specimen appears to not be fully developed.\n\n2. Modular body:\nMany systems are working together to keep this creature alive. A repeating body structure implies this creature may actually be a series of organisms living in symbiosis, each capable of sustaining itself by passive means such as photosynthesis. If one segment dies, the creature still lives on. Each body segment contains many bioluminescent orbs that disorientate prey as the creature coils around its food.\n\n3. Tentacles:\nThese massive tentacles have been observed to help the creature suffocate large prey, including Reaper Leviathans, for easy consumption. They also play a role in impressing mates.\n\n4. Hunting:\nThis creature remains stealthy until it has found a target, and will go to any extent to put the target to put it into shock. It wields very powerful jaws, capable of rendering large creatures unconcious with one bite. The wide throat is capable of swallowing many large creatures whole.\n\nAssessment: Avoid at all costs.";

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            DealDamageOnImpact dealDamageOnImpact = prefab.AddComponent<DealDamageOnImpact>();
            dealDamageOnImpact.speedMinimumForDamage = 3f;
            dealDamageOnImpact.mirroredSelfDamage = false;

            CreateTrail(prefab.SearchChild("spine"), components, 4f, -1f);
            const float tentacleTrailSnapSpeed = 7f;
            const float tentacleTrailMaxSegmentOffset = 15f;
            CreateTrail(prefab.SearchChild("tentaclelowerl"), components, tentacleTrailSnapSpeed, tentacleTrailMaxSegmentOffset);
            CreateTrail(prefab.SearchChild("tentaclelowerr"), components, tentacleTrailSnapSpeed, tentacleTrailMaxSegmentOffset);
            CreateTrail(prefab.SearchChild("tentacleupperl"), components, tentacleTrailSnapSpeed, tentacleTrailMaxSegmentOffset);
            CreateTrail(prefab.SearchChild("tentacleupperr"), components, tentacleTrailSnapSpeed, tentacleTrailMaxSegmentOffset);
            CreateTrail(prefab.SearchChild("ChinTendrilL"), components, 7f, 3f, 1f);
            CreateTrail(prefab.SearchChild("ChinTendrilR"), components, 7f, 3f, 1f);

            prefab.SearchChild("Eye1").AddComponent<TrackLastTarget>().lastTarget = components.lastTarget;
            prefab.SearchChild("Eye2").AddComponent<TrackLastTarget>().lastTarget = components.lastTarget;
            prefab.SearchChild("Eye3").AddComponent<TrackLastTarget>().lastTarget = components.lastTarget;
            prefab.SearchChild("Eye4").AddComponent<TrackLastTarget>().lastTarget = components.lastTarget;
            prefab.SearchChild("Eye5").AddComponent<TrackLastTarget>().lastTarget = components.lastTarget;
            prefab.SearchChild("Eye6").AddComponent<TrackLastTarget>().lastTarget = components.lastTarget;

            GameObject mouth = prefab.SearchChild("Mouth");
            AddMeleeAttack(mouth, 1f, 150f, "BlazaBite", 5001f, false, components);

            AttackCyclops actionAtkCyclops = prefab.AddComponent<AttackCyclops>();
            actionAtkCyclops.swimVelocity = 25f;
            actionAtkCyclops.aggressiveToNoise = new CreatureTrait(0f, 0.1f);
            actionAtkCyclops.evaluatePriority = 0.9f;
            actionAtkCyclops.priorityMultiplier = ECCHelpers.Curve_Flat();


            MakeAggressiveTo(40f, 5, EcoTargetType.Shark, 0f, 2f);
            MakeAggressiveTo(125f, 5, EcoTargetType.Leviathan, 0f, 3f);
            MakeAggressiveTo(50f, 5, EcoTargetType.SubDecoy, 0f, 2f);
            MakeAggressiveTo(100f, 5, EcoTargetType.Whale, 0f, 1f);

            DiveAction actionDive = prefab.AddComponent<DiveAction>();

            prefab.AddComponent<GargantuanBehaviour>();
        }
    }
}