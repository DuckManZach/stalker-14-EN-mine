using Content.Shared.Access;
using Robust.Shared.Prototypes;

namespace Content.Shared._Stalker_EN.Factions;

/// <summary>
/// This is a prototype for centralizing data related to the various factions that would normally be difficult to find a reference to
/// </summary>
[Prototype()]
public sealed partial class StalkerFactionPrototype : IPrototype
{
    /// <inheritdoc/>
    [IdDataField]
    public string ID { get; } = default!;

    /// <summary>
    ///     The name of this faction as displayed to players.
    /// </summary>
    [DataField]
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    ///     The description of this faction as displayed to players.
    /// </summary>
    [DataField]
    public string? Description { get; private set; }


    /// <summary>
    /// The access levels associated with normal members of this faction
    /// </summary>
    [DataField]
    public HashSet<ProtoId<AccessLevelPrototype>> Tags = new();

    /// <summary>
    /// The ID of the portal associated with the home base of the faction
    /// </summary>
    [DataField]
    public EntProtoId FactionSpawnPortal;

    /// <summary>
    /// The portal associated with a faction's shared stash
    /// </summary>
    [DataField]
    public EntProtoId FactionBandPortal;
}
