using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microservice.Producer.Infra.Helpers.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class StringExtension
    {
        public static string ToPipedMessage(this IEnumerable<string> words) =>
            words is null || !words.Any()
            ? string.Empty
            : string.Join('|', words);
    }
}
