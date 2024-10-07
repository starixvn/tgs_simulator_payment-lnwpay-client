using Microsoft.AspNetCore.Mvc;

namespace Saba.Payment.lnwPay.Controllers;

[Route("[controller]")]
[ApiController]
public class DepositController(ILogger<DepositController> logger) : ControllerBase
{
    [HttpPost("callback")]
    [HttpGet("callback")]
    public async Task<string> DepositCallbackAsync()
    {
        return "this is callback function";
    }
};