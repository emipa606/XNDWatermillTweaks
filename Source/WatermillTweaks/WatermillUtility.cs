using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace WatermillTweaks;

public static class WatermillUtility
{
    private const float PowerProductionFactorSpring = 1.2f;
    private const float PowerProductionFactorSummer = 1f;
    private const float PowerProductionFactorFall = 1.1f;
    private const float PowerProductionFactorWinter = 0.7f;
    private const float FullPowerProductionLowTemp = 0f;
    private const float FullPowerProductionHighTemp = 50f;
    private const float HalfPowerProductionHighTemp = 60f;
    private const float ZeroPowerProductionHighTemp = 90f;

    private static readonly Dictionary<RiverDef, float> lowPowerProductionDict = new()
    {
        { RiverDefOf.Creek, -5f },
        { RiverDefOf.River, -10f },
        { RiverDefOf.LargeRiver, -15f },
        { RiverDefOf.HugeRiver, -20f }
    };

    public static readonly List<string> WaterMillDefNames =
    [
        "WatermillGenerator",
        "VFE_AdvancedWatermillGenerator"
    ];

    public static Season GetMapSeason(this Thing thing)
    {
        return GenDate.Season(Find.TickManager.TicksAbs, Find.WorldGrid.LongLatOf(thing.MapHeld.Tile));
    }

    public static RiverDef GetRiver(this Map map)
    {
        return map.TileInfo is not SurfaceTile surfaceTile ? null : surfaceTile.Rivers?.First().river;
    }

    public static float SeasonalPowerOutputFactorFor(Season season)
    {
        switch (season)
        {
            case Season.PermanentWinter:
            case Season.Winter:
                return PowerProductionFactorWinter;
            case Season.Fall:
                return PowerProductionFactorFall;
            case Season.Spring:
                return PowerProductionFactorSpring;
            default:
                return PowerProductionFactorSummer;
        }
    }

    public static SimpleCurve GetTemperatureToPowerOutputFactorCurveFor(Thing thing)
    {
        var river = thing.Map.GetRiver();
        var halfPowerProductionLowTemp = 0f;
        if (river != null)
        {
            halfPowerProductionLowTemp = lowPowerProductionDict[river];
        }

        return
        [
            new CurvePoint(Mathf.RoundToInt(halfPowerProductionLowTemp * 1.5f), 0f),
            new CurvePoint(halfPowerProductionLowTemp, 0.5f),
            new CurvePoint(FullPowerProductionLowTemp, 1f),
            new CurvePoint(FullPowerProductionHighTemp, 1f),
            new CurvePoint(HalfPowerProductionHighTemp, 0.5f),
            new CurvePoint(ZeroPowerProductionHighTemp, 0f)
        ];
    }
}