using Robust.Shared.Audio;

namespace Content.Server._Stalker_EN.Mobs;

/// <summary>
/// This is used for playing a sound upon a mob's death, without having to do stupid emote jank
/// </summary>
[RegisterComponent]
public sealed partial class SoundOnDeathComponent : Component
{
    /// <summary>
    ///     The sound to use
    /// </summary>
    [DataField]
    public SoundSpecifier Sound = new SoundPathSpecifier("/Audio/Effects/Gasp/male_deathgasp_1.ogg");
}
