using System.Collections.Generic;
using System.IO;
using CustomAbilityLib.API;
using CustomRoleLib.API;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Paths;
using MEC;
using PlayerRoles;
using PlayerStatsSystem;
using SecretLabNAudio.Core.Extensions;
using Scp066.Features;
using UnityEngine;

namespace Scp066.Features.Abilities;

public class PlayNoise : ServerSpecificSettingAbility<PlayNoiseInstance>
{
    public override string Name => "🎺 Noise";
    public override string Description => "Plays a symphony that kills nearby humans";
    public override string Id => "play_noise";
    protected override double Cooldown => 40;
    protected override KeyCode SuggestedKey => KeyCode.F;
}

public class PlayNoiseInstance : AbilityInstanceBase
{
    private CoroutineHandle _damageCoroutine;

    public override void Create(Player player) { }

    public override void Destroy()
    {
        if (_damageCoroutine.IsRunning)
            Timing.KillCoroutines(_damageCoroutine);
    }

    public override bool Execute(out string response)
    {
        response = null;
        var config = Scp066Plugin.Singleton.Config;

        if (config.MaxDistance <= 0 || config.Damage <= 0)
        {
            response = "Invalid distance or damage configuration.";
            return false;
        }

        if (!Scp066AudioComponent.PlayerAudioPlayers.TryGetValue(Owner, out var ap))
            return false;

        var soundFile = Path.Combine(PathManager.Configs.FullName, "Scp066", "symphony.ogg");
        ap.ClearBuffer();
        ap.EnqueueFileSafe(soundFile, 1f);

        if (_damageCoroutine.IsRunning)
            Timing.KillCoroutines(_damageCoroutine);
        _damageCoroutine = Timing.RunCoroutine(DamageLoop(config.MaxDistance, config.Damage, config.CustomDeathText));

        return true;
    }

    private IEnumerator<float> DamageLoop(float maxDistance, float damage, string damageText)
    {
        yield return Timing.WaitForSeconds(0.2f);

        var elapsed = 0f;
        while (elapsed < 25f && Owner != null)
        {
            foreach (var p in Player.ReadyList)
            {
                if (p.IsSCP || !p.IsAlive || p.Team == Team.OtherAlive) continue;
                if (Vector3.Distance(Owner.Position, p.Position) <= maxDistance)
                    p.Damage(new CustomReasonDamageHandler(damageText, damage));
            }

            yield return Timing.WaitForSeconds(1f);
            elapsed += 1f;
        }
    }
}
