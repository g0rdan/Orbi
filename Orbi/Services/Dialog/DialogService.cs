using System;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace Orbi.Services
{
    public class DialogService : IDialogService
    {
        public DialogService()
        {
        }

        public async Task<(bool IsOk, string Title)> AskToAddAlbum()
        {
            var config = new PromptConfig()
                .SetTitle("Album's name")
                .SetInputMode(InputType.Default);

            var result = await UserDialogs.Instance.PromptAsync(config);
            return (result.Ok, result.Text.ToString());
        }
    }
}
