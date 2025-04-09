namespace ContinentalWorld;

public class ContinentalConfig
{
    public float ExtraLandcoverScale = 2.0f;
    public float CellularJitter = 0.43701595f;

    public int ContinentWarpOctaves = 5;
    public float ContinentWarpScale = 4.0f;
    public float ContinentWarpPersistence = 0.75f;
    public float ContinentWarpPower = 2.0f;

    public float IslandThreshold = 1.0f;
    public int IslandNoiseOctaves = 5;
    public float IslandNoiseScale = 2.0f;
    public float IslandNoisePersistence = 0.9f;
    public float IslandScale = 2.0f;

    public ContinentalConfig Clone()
    {
        return (ContinentalConfig)this.MemberwiseClone();
    }
}
