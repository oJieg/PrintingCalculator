using printing_calculator.Singletones.Interfases;
using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace printing_calculator.Singletones
{
    public class TokensStore : ITokenStore
    {
        private const int MAX_COUNT_TOKENS = 67; //TODO вынести в конфиг
        private const int COUNT_REMOVE_CLEAR = 32;

        private readonly ConcurrentDictionary<string, DealAutorizationInfo> _tokens = new();
        private readonly ITokenGenerator _tokenGenerator;

        public TokensStore(ITokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        public string CreateNewToken(int dealId, string bitrixAuthToken)
        {
            if(_tokens.Count >= MAX_COUNT_TOKENS)
            {
                var removeTokens = _tokens.OrderBy(x => x.Value.CreatDataTime).Take(COUNT_REMOVE_CLEAR);
                foreach (var removeToken in removeTokens) {
                    _tokens.TryRemove(removeToken);
                }
            }

            while (true)
            {
                string token = _tokenGenerator.GenerateRandomToken();
                if (_tokens.TryAdd(token, new DealAutorizationInfo()
                {
                    BitrixAuthToken = bitrixAuthToken,
                    DealId = dealId,
                    CreatDataTime = DateTime.UtcNow
                }))
                {
                    return token;
                }
            }
        }

        public DealAutorizationInfo? GetDealAutorizationInfo(string token)
        {
            return _tokens.GetValueOrDefault(token);
        }

    }

    public class DealAutorizationInfo
    {
        public int DealId { get; set; }
        public string BitrixAuthToken { get; set; }
        public DateTime CreatDataTime { get; set; }
    }
}
