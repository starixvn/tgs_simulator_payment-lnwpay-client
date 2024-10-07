

namespace lnwPay.Client;
    public record InwPayDepositSettings(
    string WebsitePrefix,
    string WebsiteToken,
    string ClientAccount,
    string ClientAccountName,
    string ClientBank,
    Guid FormId,
    Uri RedirectUrl
  );
