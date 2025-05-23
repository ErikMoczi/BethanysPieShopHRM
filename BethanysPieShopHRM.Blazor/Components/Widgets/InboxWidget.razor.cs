﻿using BethanysPieShopHRM.Blazor.State;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.Blazor.Components.Widgets;

public partial class InboxWidget
{
    [Inject]
    public ApplicationState ApplicationState { get; set; } = null!;

    public int MessageCount { get; set; } = 0;

    protected override void OnInitialized()
    {
        MessageCount = ApplicationState.NumberOfMessages;
    }
}