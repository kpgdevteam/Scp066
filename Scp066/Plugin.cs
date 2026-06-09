using System;
using System.IO;
using CustomAbilityLib.API;
using CustomRoleLib.API;
using LabApi.Features;
using LabApi.Loader;
using LabApi.Loader.Features.Paths;
using LabApi.Loader.Features.Plugins;
using Scp066.Features;
using SecretLabNAudio.Core.FileReading;
using SecretLabNAudio.FFmpeg.Extensions;

namespace Scp066;

public class Scp066Plugin : Plugin<Config>
{
    public override string Name => "Scp066";
    public override string Description =>
        "Adds SCP-066, the noise maker, as a custom role with unique abilities and features.";
    public override string Author => "RisottoMan, LabApi version: MedveMarci";
    public override Version Version => new(1, 3, 0);
    public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);

    public static Scp066Plugin Singleton { get; private set; }

    private const string RoleNamespaceKey = "scp066:scp066";

    public override void Enable()
    {
        Singleton = this;
        CustomRoleManager.RegisterAllRoles(typeof(Scp066Role).Assembly);
        CustomAbilityManager.RegisterAllAbilities(typeof(Scp066Role).Assembly);

        CustomSpawnManager.SetGroupMaxTokens(RoleNamespaceKey, 1);
        CustomSpawnManager.SetGroupTokenReset(RoleNamespaceKey, CustomSpawnManager.TokenResetType.RoundRestart);

        var cachePath = Path.Combine(this.GetConfigDirectory().FullName, Config.ShortClipsPath);
        ShortClipCache.AddAllFromDirectoryWithFFmpeg(cachePath);
    }

    public override void Disable()
    {
        CustomAbilityManager.UnregisterAllAbilities(typeof(Scp066Role).Assembly);
        CustomRoleManager.UnregisterAllRoles(typeof(Scp066Role).Assembly);
        Singleton = null;
    }
}
