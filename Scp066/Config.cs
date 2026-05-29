using Scp066.Features;

namespace Scp066;

public class Config
{
    public bool Debug { get; set; } = false;
    public float Damage { get; set; } = 8f;
    public float MaxDistance { get; set; } = 8f;

    public string CustomDeathText { get; set; } =
        "<color=red>The subject expired after exposure to a loud sound by SCP-066</color>";

    public Scp066Role Scp066Role { get; set; } = new();
}