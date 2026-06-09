using System.IO;
using CustomAbilityLib.API;
using CustomRoleLib.API;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Paths;
using SecretLabNAudio.Core.Extensions;
using Scp066.Features;
using UnityEngine;

namespace Scp066.Features.Abilities;

public class PlayNotes : ServerSpecificSettingAbility<PlayNotesInstance>
{
    public override string Name => "SCP-066 🎶 Note";
    public override string Description => "Play back random creepy notes";
    public override string Id => "play_notes";
    protected override double Cooldown => 10;
    protected override KeyCode SuggestedKey => KeyCode.R;
}

public class PlayNotesInstance : AbilityInstanceBase
{
    public override void Create(Player player) { }
    public override void Destroy() { }

    public override bool Execute(out string response)
    {
        response = null;
        if (!Scp066AudioComponent.PlayerAudioPlayers.TryGetValue(Owner, out var ap))
            return false;

        var value = Random.Range(0, 6) + 1;
        ap.ClearBuffer();
        ap.UseShortClip($"Notes{value}");
        return true;
    }
}
