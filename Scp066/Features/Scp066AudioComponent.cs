using System.Collections.Generic;
using CustomRoleLib.API.DefaultComponents;
using LabApi.Features.Wrappers;
using SecretLabNAudio.Core;
using UnityEngine;

namespace Scp066.Features;

public class Scp066AudioComponent : ComponentBase<Scp066RoleInstance>
{
    private const float Volume = 1f;
    private static readonly Vector3 PositionOffset = Vector3.zero;

    public static readonly Dictionary<Player, AudioPlayer> PlayerAudioPlayers = new();

    public override void OnCreatedInstance(Scp066RoleInstance instance)
    {
        var ap = AudioPlayer.Create(
            SpeakerSettings.Default with { Volume = Volume },
            PositionOffset, instance.Owner.GameObject!.transform);

        PlayerAudioPlayers[instance.Owner] = ap;
    }

    public override void OnDestroyedInstance(Scp066RoleInstance instance)
    {
        if (!PlayerAudioPlayers.TryGetValue(instance.Owner, out var ap)) return;
        ap.Destroy();
        PlayerAudioPlayers.Remove(instance.Owner);
    }
}
