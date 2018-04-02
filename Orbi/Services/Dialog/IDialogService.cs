using System;
using System.Threading.Tasks;

namespace Orbi.Services
{
    public interface IDialogService
    {
        /// <summary>
        /// Asking to add an album. True if it was succeful
        /// </summary>
        /// <returns>The to add album.</returns>
        Task<(bool IsOk, string Title)> AskToAddAlbum();
    }
}
