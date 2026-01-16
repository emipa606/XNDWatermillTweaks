using HarmonyLib;
using RimWorld;

namespace WatermillTweaks;

[HarmonyPatch(typeof(CompPowerPlantWater), "DesiredPowerOutput", MethodType.Getter)]
public static class CompPowerPlantWater_DesiredPowerOutput
{
    public static void Postfix(CompPowerPlantWater __instance, ref float __result)
    {
        // Season
        __result *= WatermillUtility.SeasonalPowerOutputFactorFor(__instance.parent.GetMapSeason());

        // Outdoor Temperature
        __result *= WatermillUtility.GetTemperatureToPowerOutputFactorCurveFor(__instance.parent)
            .Evaluate(__instance.parent.MapHeld.mapTemperature.OutdoorTemp);

        // Turbulent Waters
        if (__instance.parent.MapHeld.GameConditionManager.ConditionIsActive(BWG_GameConditionDefOf
                .TurbulentWaters))
        {
            __result *= GameCondition_TurbulentWaters.WatermillPowerGenFactor;
        }
    }
}