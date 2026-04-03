namespace printing_calculator.Singletones.Interfases
{
    public interface ITokenStore
    {
        string CreateNewToken(int dealId, string bitrixAuthToken);
        public DealAutorizationInfo GetDealAutorizationInfo(string token);
    }
}
