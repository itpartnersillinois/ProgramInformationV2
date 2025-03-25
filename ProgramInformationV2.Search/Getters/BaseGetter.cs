using OpenSearch.Client;

namespace ProgramInformationV2.Search.Getters {

    public abstract class BaseGetter<T>(OpenSearchClient? openSearchClient) where T : class {
        internal readonly OpenSearchClient _openSearchClient = openSearchClient ?? default!;

        internal void LogDebug(ISearchResponse<T> response) {
            if (_openSearchClient.ConnectionSettings.DisableDirectStreaming) {
                Console.WriteLine(response.DebugInformation);
            }
        }

        internal void LogDebug(GetResponse<T> response) {
            if (_openSearchClient.ConnectionSettings.DisableDirectStreaming) {
                Console.WriteLine(response.DebugInformation);
            }
        }
    }
}