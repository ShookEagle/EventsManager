using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using EventsManager.api.plugin;
using EventsManager.plugin.menus.style;
using RMenu;
using RMenu.Enums;

namespace EventsManager.plugin.menus.components;
//Look I'm well fucking aware how janky this is. this lib should probs have pagination built-in
public abstract class PaginatedMenuBase(
    IEventsManager plugin,
    CCSPlayerController player,
    MenuValue[] header,
    MenuBase? parent = null)
    : RMenuBase(plugin, player, header, parent)
{
    private List<string> Items { get; set; } = [];
    private int PageSize { get; } = 5;
    private int _currentPage = 1;

    protected void SetItems(IEnumerable<string> items)
    {
        Items = items.ToList();
        _currentPage = 1;
    }

    private void UpdateItems()
    {
        var start = (_currentPage - 1) * PageSize;
        var end = Math.Min(start + PageSize, Items.Count);

        for (var i = start; i < end; i++)
            Menu.Items[i - start] = CreateMenuItem(Items[i]);

        //Pad with blanks if not enough items
        for (var i = end; i < start + PageSize; i++)
            Menu.Items[i - start] = Theme.BlankItem;
    }

    protected override void OnAction(CCSPlayerController player, MenuBase menu, MenuAction action)
    {
        if (action is not (MenuAction.Select or MenuAction.Update or MenuAction.Input))
            return;

        if (menu.SelectedItem?.Index == PageSize)
        {
            var pageData = menu.SelectedItem?.Item?.SelectedValue?.Value?.Data;
            if (pageData is not int page || page <= 0 || page > TotalPages) return;
            _currentPage = page;
            UpdateItems();
            return;
        }

        if (action != MenuAction.Select) return;

        var index = (_currentPage - 1) * PageSize + (menu.SelectedItem?.Index ?? -1);
        if (index >= 0 && index < Items.Count)
            OnItemSelected(Items[index]);
    }

    protected override void Build()
    {
        for (var i = 0; i < PageSize; i++) Menu.AddItem(Theme.BlankItem);

        // Add pagination control at the end
        var pages = new List<MenuValue>();
        for (var i = 1; i <= TotalPages; i++)
            pages.Add(new MenuValue($"{i}", data: i));

        var pageChoice = new MenuItem(MenuItemType.Choice, "Page: ", pages)
        { SelectedValue = (_currentPage - 1, pages[_currentPage - 1]) };

        Menu.AddItem(pageChoice);
    }

    private int TotalPages => Math.Max(1, (int)Math.Ceiling((double)Items.Count / PageSize));
    protected abstract MenuItem CreateMenuItem(string item);
    protected abstract void OnItemSelected(string selected);
}