using Microsoft.AspNetCore.Mvc;
using SaveEmailOnLocalDisk.Services;

namespace SaveEmailOnLocalDisk.Controllers;


[Route("[controller]")]
[ApiController]
public class EmailsController : ControllerBase
{
    private readonly ISaveMessageService _saveMessageService;

    public EmailsController(ISaveMessageService saveMessageService)
    {
        _saveMessageService = saveMessageService;
    }

    [HttpPost("{emailAddresFrom}/{emailAddressTo}/{subjectEmail}/{bodyEmail}")]
    public ActionResult SaveEmail(string emailAddresFrom, string emailAddressTo, string subjectEmail, string bodyEmail)
    {
        try
        {
            if (!CheckEmailValide(emailAddressTo))
                return BadRequest();

            _saveMessageService.SaveEmailAsync(emailAddresFrom, emailAddressTo, subjectEmail, bodyEmail);

            return Ok("Email Ok");
        }
        catch (Exception)
        {
            return RedirectToAction("Email Fail");
        }

    }

    private bool CheckEmailValide(string emailAddress)
    {
        if (emailAddress.Contains("@"))
            return true;

        return false;
    }
}
