using Content.Client.Chat.Managers;
using Content.Shared.Sirena.CollectiveMind;
using Robust.Client.Player;

namespace Content.Client.Sirena.CollectiveMind;

public sealed class CollectiveMindSystem : EntitySystem
{
    [Dependency] private readonly IChatManager _chatManager = default!;
    [Dependency] private readonly IPlayerManager _playerManager = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<CollectiveMindComponent, ComponentInit>(OnInit);
        SubscribeLocalEvent<CollectiveMindComponent, ComponentRemove>(OnRemove);
    }

    public CollectiveMindComponent? Player => CompOrNull<CollectiveMindComponent>(_playerManager.LocalPlayer?.ControlledEntity);
    public bool IsCollectiveMind => Player != null;

    private void OnInit(EntityUid uid, CollectiveMindComponent component, ComponentInit args)
    {
        _chatManager.UpdatePermissions();
    }

    private void OnRemove(EntityUid uid, CollectiveMindComponent component, ComponentRemove args)
    {
        _chatManager.UpdatePermissions();
    }
}
