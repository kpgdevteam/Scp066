using System;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using Scp066.Features;
using UncomplicatedCustomRoles.API.Features;

namespace Scp066;

public class Scp066 : Plugin<Config>
{
    private readonly EventHandler _eventHandler = new();
    public string githubRepo = "MedveMarci/Scp066";
    public override string Name => "Scp066";

    public override string Description =>
        "Adds SCP-066, the noise maker, as a custom role with unique abilities and features.";

    public override string Author => "RisottoMan, LabApi version: MedveMarci";
    public override Version Version => new(1, 2, 0);
    public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);
    public static Scp066 Singleton { get; private set; }
    private Scp066Role Role { get; set; }

    public override void Enable()
    {
        Singleton = this;
        RoleAPI.RoleAPI.RegisterRole(Role);
        CustomHandlersManager.RegisterEventsHandler(_eventHandler);
        AudioSetup.EnsureAudioFiles();
    }

    public override void LoadConfigs()
    {
        base.LoadConfigs();
        Role = Config.Scp066Role;
    }

    public override void Disable()
    {
        Singleton = null;
        CustomHandlersManager.UnregisterEventsHandler(_eventHandler);
        RoleAPI.RoleAPI.UnregisterRole(Role);
    }
}