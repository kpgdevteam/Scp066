namespace Scp066;

public class Config
{
    public float Damage { get; set; } = 8f;
    public float MaxDistance { get; set; } = 8f;

    public string CustomDeathText { get; set; } =
        "<color=red>The subject expired after exposure to a loud sound by SCP-066</color>";

    public string SpawnBroadcast { get; set; } =
        "<color=red>🎵 You are SCP-066 - Eric's Toy 🎵\n" +
        "Play sounds to kill humans\n" +
        "Use abilities by clicking on the buttons</color>";

    public float SpawnBroadcastDuration { get; set; } = 10f;

    public string ShortClipsPath { get; set; } = "Audio/";
}
