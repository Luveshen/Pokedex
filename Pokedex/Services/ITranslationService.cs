using System.Threading.Tasks;
using Pokedex.Services.DataTransferObjects;

namespace Pokedex.Services
{
    public interface ITranslationService
    {
        Task<TranslationResponse> TranslateTextAsync(TranslationOption translationOption, string originalText);
    }
}