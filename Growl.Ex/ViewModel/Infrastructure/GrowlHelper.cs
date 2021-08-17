using System;
using HandyControl.Data;
using G= HandyControl.Controls.Growl;

namespace Growl.Ex.ViewModel.Infrastructure
{
    public static class GrowlHelper
    {
        public static (string geometry, string brush) GetGrowlInfo(this InfoType infoType)
        {
            return infoType switch
            {
                InfoType.Success => (ResourceToken.SuccessGeometry, ResourceToken.SuccessBrush),
                InfoType.Info => (ResourceToken.InfoGeometry, ResourceToken.InfoBrush),
                InfoType.Warning => (ResourceToken.WarningGeometry, ResourceToken.WarningBrush),
                InfoType.Error => (ResourceToken.ErrorGeometry, ResourceToken.DangerBrush),
                InfoType.Fatal => (ResourceToken.FatalGeometry, ResourceToken.PrimaryTextBrush),
                InfoType.Ask => (ResourceToken.AskGeometry, ResourceToken.AccentBrush),
                _ => throw new ArgumentOutOfRangeException(nameof(infoType), infoType, null)
            };
        }

        public static Action ToGrowlAction(this GrowlInfo item) => item.Type switch
        {
            InfoType.Ask => () => G.Ask(item),
            InfoType.Info => () => G.Info(item),
            InfoType.Error => () => G.Error(item),
            InfoType.Fatal => () => G.Fatal(item),
            InfoType.Success => () => G.Success(item),
            InfoType.Warning => () => G.Warning(item),
            _ => () => G.Info(item)
        };

        public static GrowlInfo Copy(GrowlInfo growlInfo)
        {
            return new GrowlInfo
            {
                Message = growlInfo.Message,
                ShowDateTime = growlInfo.ShowDateTime,
                WaitTime = growlInfo.WaitTime,
                CancelStr = growlInfo.CancelStr,
                ConfirmStr = growlInfo.ConfirmStr,
                ActionBeforeClose = growlInfo.ActionBeforeClose,
                StaysOpen = growlInfo.StaysOpen,
                IsCustom = growlInfo.IsCustom,
                Type = growlInfo.Type,
                IconKey = growlInfo.IconKey,
                IconBrushKey = growlInfo.IconBrushKey,
                ShowCloseButton = growlInfo.ShowCloseButton,
                Token = growlInfo.Token,
            };
        }


        public static GrowlInfo ChangeToken(GrowlInfo growlInfo, string newToken)
        {
            return new GrowlInfo
            {
                Message = growlInfo.Message,
                ShowDateTime = growlInfo.ShowDateTime,
                WaitTime = growlInfo.WaitTime,
                CancelStr = growlInfo.CancelStr,
                ConfirmStr = growlInfo.ConfirmStr,
                ActionBeforeClose = growlInfo.ActionBeforeClose,
                StaysOpen = growlInfo.StaysOpen,
                IsCustom = growlInfo.IsCustom,
                Type = growlInfo.Type,
                IconKey = growlInfo.IconKey,
                IconBrushKey = growlInfo.IconBrushKey,
                ShowCloseButton = growlInfo.ShowCloseButton,
                Token = newToken,
            };
        }
    }

}
