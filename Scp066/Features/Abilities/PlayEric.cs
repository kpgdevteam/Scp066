using System.IO;
using LabApi.Loader.Features.Paths;
using RoleAPI.API.Abilities;
using UnityEngine;

namespace Scp066.Features.Abilities;

public class PlayEric : AbilityBase
{
    public override string Name => "\ud83c\udfb5 Eric?";
    public override string Description => "Play back random sound 'eric?'";
    public override KeyCode DefaultKey => KeyCode.Q;
    public override float Cooldown => 10f;

    protected override void OnExecute(AbilityExecutionContext context)
    {
        context.LocksDuringExecution  = true;
        var value = Random.Range(0, 3) + 1;
        context.SoundFile = Path.Combine(PathManager.Configs.FullName, "Scp066", $"Eric{value}.ogg");
    }
}