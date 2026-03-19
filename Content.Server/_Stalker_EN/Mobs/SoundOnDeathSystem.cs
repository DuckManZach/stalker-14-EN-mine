using Content.Shared.Mobs;
using Content.Shared.Speech.Muting;
using Robust.Server.Audio;

namespace Content.Server._Stalker_EN.Mobs;

/// <summary>
/// This handles...
/// </summary>
public sealed class SoundOnDeathSystem : EntitySystem
{
    [Dependency] private readonly AudioSystem _audio = default!;
    /// <inheritdoc/>
    public override void Initialize()
    {
        SubscribeLocalEvent<SoundOnDeathComponent, MobStateChangedEvent>(OnMobStateChanged);
    }

    private void OnMobStateChanged(EntityUid uid, SoundOnDeathComponent component, MobStateChangedEvent args)
    {
        if (args.NewMobState != MobState.Dead)
            return;

        DeathSound(uid, component);
    }

    /// <summary>
    ///     Causes an entity to perform their deathgasp emote, if they have one.
    /// </summary>
    private bool DeathSound(EntityUid uid, SoundOnDeathComponent? component = null)
    {
        if (!Resolve(uid, ref component, false))
            return false;

        if (HasComp<MutedComponent>(uid))
            return false;

        _audio.PlayPvs(component.Sound, Transform(uid).Coordinates);

        return true;
    }
}
