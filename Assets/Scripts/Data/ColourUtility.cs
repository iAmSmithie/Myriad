using UnityEngine;

public static class ColourUtility
{
    public static Color GetColour(PegColour colour)
    {
        switch (colour)
        {
            case PegColour.Red: return Color.red;
            case PegColour.Blue: return Color.blue;
            case PegColour.Green: return Color.green;
            case PegColour.Yellow: return Color.yellow;
            case PegColour.Purple: return Color.purple;
            case PegColour.Orange: return Color.orange;
            default: return Color.white;
        }
    }
}