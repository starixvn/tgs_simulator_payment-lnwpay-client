

namespace lnwPay.Client;
    public record LnwPayDepositSettings(
    string WebsitePrefix,
    string WebsiteToken,
    string ClientAccount,
    string ClientAccountName,
    string ClientBank,
    Guid FormId,
    Uri RedirectUrl
  );
