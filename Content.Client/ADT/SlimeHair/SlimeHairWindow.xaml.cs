using Content.Shared.Humanoid.Markings;
using Content.Shared.SlimeHair;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;
using Robust.Client.UserInterface.XAML;

namespace Content.Client.ADT.SlimeHair;

[GenerateTypedNameReferences]
public sealed partial class SlimeHairWindow : DefaultWindow
{
    // MMMMMMM
    public Action<(int slot, string id)>? OnHairSelected;
    public Action<(int slot, Marking marking)>? OnHairColorChanged;
    public Action<int>? OnHairSlotRemoved;
    public Action? OnHairSlotAdded;

    public Action<(int slot, string id)>? OnFacialHairSelected;
    public Action<(int slot, Marking marking)>? OnFacialHairColorChanged;
    public Action<int>? OnFacialHairSlotRemoved;
    public Action? OnFacialHairSlotAdded;

    public SlimeHairWindow()
    {
        RobustXamlLoader.Load(this);

        HairPicker.OnMarkingSelect += args => OnHairSelected!(args);
        HairPicker.OnColorChanged += args => OnHairColorChanged!(args);
        HairPicker.OnSlotRemove += args => OnHairSlotRemoved!(args);
        HairPicker.OnSlotAdd += delegate { OnHairSlotAdded!(); };

        FacialHairPicker.OnMarkingSelect += args => OnFacialHairSelected!(args);
        FacialHairPicker.OnColorChanged += args => OnFacialHairColorChanged!(args);
        FacialHairPicker.OnSlotRemove += args => OnFacialHairSlotRemoved!(args);
        FacialHairPicker.OnSlotAdd += delegate { OnFacialHairSlotAdded!(); };
    }

    public void UpdateState(SlimeHairUiState state)
    {
        HairPicker.UpdateData(state.Hair, state.Species, state.HairSlotTotal);
        FacialHairPicker.UpdateData(state.FacialHair, state.Species, state.FacialHairSlotTotal);

        if (!HairPicker.Visible && !FacialHairPicker.Visible)
        {
            AddChild(new Label { Text = Loc.GetString("magic-mirror-component-activate-user-has-no-hair") });
        }
    }
}
