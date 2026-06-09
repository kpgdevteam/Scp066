using System.IO;
using CustomAbilityLib.API;
using CustomRoleLib.API;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Paths;
using SecretLabNAudio.Core.Extensions;
using Scp066.Features;
using UnityEngine;

namespace Scp066.Features.Abilities;

public class PlayEric : ServerSpecificSettingAbility<PlayEricInstance>
{
    public override string Name => "🎵 Eric?";
    public override string Description => "Play back random sound 'eric?'";
    public override string Id => "play_eric";
    protected override double Cooldown => 10;
    protected override KeyCode SuggestedKey => KeyCode.Q;
}

public class PlayEricInstance : AbilityInstanceBase
{
    public override void Create(Player player) { }
    public override void Destroy() { }

    public override bool Execute(out string response)
    {
        response = null;
        if (!Scp066AudioComponent.PlayerAudioPlayers.TryGetValue(Owner, out var ap))
            return false;

        var value = Random.Range(0, 3) + 1;
        var soundFile = Path.Combine(PathManager.Configs.FullName, "Scp066", $"eric{value}.ogg");
        ap.ClearBuffer();
        ap.EnqueueFileSafe(soundFile, 1f);
        return true;
    }
}
