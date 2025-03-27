This is a tiny mod that will inject a custom ocean map generator into the game.

The new generator uses Worley noise (also known as voronoi noise) to give landmasses very nice and coherent shapes. It does so by using the voroni "seed points" directly and assigning them to be either landmass or ocean, at a threshold dependent on your landcover setting. The resulting landmasses are then heavily distorted with FBM noise make them less boring :)

This technique excels at cleanly seperating continents from each other, and also works very well for story locations, as for each location it will mark the nearest voronoi seed point as land, making sure an entire well-sized continent is generated at each location. This includes the spawn point - no more spawning on a tiny island!

Landforms are not affected at all, so you can use this with any world generation mods you like. "Rivers" also works very well as a companion mod.

**Recommended world generation settings**

**Landcover**: 30% is good for an earth-like land-to-ocean ratio. Go a bit higher if you want more land and less ocean. At above 50% landcover, you will have more land than ocean, and at that point is it really an ocean or just giant lakes?

**Landcover scale**: 100% is tuned to give reasonably sized oceans so you likely won't have to sail for days to reach new continents. At this setting each landmass/ocean "tile" is about 1000-1500 square blocks in size. If you prefer more a realistic scale, 200% to 400% is ideal.

If you're using the rivers mod, the default settings work fine, but you may want to adjust landScaleMultiplier depending on your landcover scale - 1.0 for 100%, 1.5 for 200%, 2.0 for 400%.

You can also use my customized rivers config, it tunes the rivers to be a bit more wobbly and removes boulders and river tunnels. of course this is only personal preference.

```
{
"disableFlow": false,
"minForkAngle": 15,
"forkVariation": 35,
"normalAngle": 20,
"valleyStrengthMax": 1.2,
"valleyStrengthMin": 0.8,
"noiseExpansion": 1.25,
"riverPaddingBlocks": 256,
"landScaleMultiplier": 1.0, // adjust this to match your landcover scale - 1.0 for 100%, 1.5 for 200%, 2.0 for 400%.
"minSize": 14.0,
"maxSize": 50.0,
"minNodes": 8,
"maxNodes": 20,
"riverGrowth": 3.0,
"downhillError": 1,
"minLength": 150,
"lengthVariation": 200,
"zoneSize": 256,
"zonesInRegion": 128,
"riverSpawnChance": 0.2,
"riverSplitChance": 0.35,
"lakeChance": 0.15,
"segmentsInRiver": 3,
"segmentOffset": 40.0,
"baseDepth": 0.1,
"riverDepth": 0.022,
"heightBoost": 8,
"topFactor": 1.0,
"riverOctaves": 2,
"riverFrequency": 0.005,
"riverLacunarity": 3.0,
"riverGain": 0.3,
"riverDistortionStrength": 20,
"riverSpeed": 0.5,
"maxValleyWidth": 75.0,
"oceanThreshold": 30.0,
"fixGravityBlocks": true,
"boulders": false,
"gravelBeaches": true,
"RegionSize": 32768,
"ChunksInRegion": 1024,
"ChunksInZone": 8
}
```
