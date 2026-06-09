using System.Collections.Generic;
using System.IO;
using CustomAbilityLib.API;
using CustomRoleLib.API;
using DrawableLine;
using InventorySystem.Items.ThrowableProjectiles;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Paths;
using MEC;
using NorthwoodLib.Pools;
using PlayerRoles;
using PlayerStatsSystem;
using SecretLabNAudio.Core.Extensions;
using Scp066.Features;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;

namespace Scp066.Features.Abilities;

public class PlayNoise : ServerSpecificSettingAbility<PlayNoiseInstance>
{
    public override string Name => "SCP-066 🎺 Noise";
    public override string Description => "Plays a symphony that kills nearby humans";
    public override string Id => "play_noise";
    protected override double Cooldown => 40;
    protected override KeyCode SuggestedKey => KeyCode.F;
}

public class PlayNoiseInstance : AbilityInstanceBase
{
    private static readonly CachedLayerMask DamageTargets = new(
        "Hitbox"
    );

    private static readonly Collider[] SpherecastBuffer = new Collider[512];

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

        ap.ClearBuffer();
        ap.UseShortClip("symphony");

        if (_damageCoroutine.IsRunning)
            Timing.KillCoroutines(_damageCoroutine);
        _damageCoroutine = Timing.RunCoroutine(DamageLoop(config.MaxDistance, config.Damage, config.CustomDeathText));

        return true;
    }

    private IEnumerator<float> DamageLoop(float maxRadius, float damage, string damageText)
    {
        var damageHandler = new CustomReasonDamageHandler(damageText, damage);

        yield return Timing.WaitForSeconds(0.2f);

        var handledNetIds = HashSetPool<uint>.Shared.Rent();
        var elapsed = 0f;
        while (elapsed < 25f && Owner != null)
        {
            var position = Owner.Position;
            var hits = Physics.OverlapSphereNonAlloc(position, maxRadius, SpherecastBuffer, DamageTargets);
            for (var i = 0; i < hits; i++)
            {
                var collider = SpherecastBuffer[i];
                if (!collider.TryGetComponent<HitboxIdentity>(out var hitbox)) continue;
                if (handledNetIds.Contains(hitbox.NetworkId)) continue;
                if (!TryDamage(hitbox, position, damageHandler)) continue;

                handledNetIds.Add(hitbox.NetworkId);
            }
            handledNetIds.Clear();

            yield return Timing.WaitForSeconds(1f);
            elapsed += 1f;
        }
        HashSetPool<uint>.Shared.Return(handledNetIds);
    }

    private static bool TryDamage(HitboxIdentity hitbox, Vector3 damageSource, CustomReasonDamageHandler damageHandler)
    {
        if (Physics.Linecast(hitbox.CenterOfMass, damageSource, ThrownProjectile.HitBlockerMask))
            return false;

        if (!Player.TryGet(hitbox.NetworkId, out var player))
            return false;

        if (player.IsSCP || !player.IsAlive || player.Team == Team.OtherAlive)
            return true;

        hitbox.Damage(damageHandler.Damage, damageHandler, damageSource);
        return true;
    }
}
