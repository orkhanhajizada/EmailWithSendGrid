using Microsoft.Extensions.Configuration;
using EmailWithSendGrid;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

await GetUserInfo();
return;

static async Task GetUserInfo()
{
    string fullname = GetValidFullname();
    string toEmail = await GetValidEmail();

    Dictionary<string, string> data = new()
    {
        { "fullname", fullname }
    };

    var result = await SendAsync(toEmail, data);

    Console.WriteLine(result ? "E-poçt göndərildi." : "E-poçt göndərilərkən xəta baş verdi.");
}

static async Task<bool> SendAsync(string toEmail, IDictionary<string, string> data)
{
    IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("/Users/orkhanhajizada/Desktop/EmailWithSendGrid/appsetting.json",
            optional: true, reloadOnChange: true)
        .Build();

    string apiKey = configuration["SendGrid:ApiKey"] ?? throw new NotFoundException("SendGrid ApiKey tapılmadı");
    string templateId = configuration["SendGrid:TemplateId"] ??
                        throw new NotFoundException("SendGrid TemplateId tapılmadı");
    string fromEmail = configuration["SendGrid:FromEmail"] ??
                       throw new NotFoundException("SendGrid FromEmail tapılmadı");
    string companyName = configuration["SendGrid:CompanyName"] ??
                         throw new NotFoundException("SendGrid CompanyName tapılmadı");
    string redirectUrl = configuration["SendGrid:RedirectUrl"] ??
                         throw new NotFoundException("SendGrid RedirectUrl tapılmadı");
    string eventName = configuration["SendGrid:EventName"] ??
                       throw new NotFoundException("SendGrid EventName tapılmadı");

    data.Add("redirectUrl", redirectUrl);
    data.Add("companyName", companyName);
    data.Add("eventName", eventName);
    data.Add("Subject", "Qeydiyyatınız uğurla başa çatdı!");
    var dataJson = JsonConvert.SerializeObject(data);
    var sendgridClient = new SendGridClient(apiKey);

    var sendgridMessage = new SendGridMessage();
    sendgridMessage.SetFrom(fromEmail, companyName);
    sendgridMessage.AddTo(toEmail);
    sendgridMessage.SetTemplateId(templateId);
    sendgridMessage.SetTemplateData(JsonConvert.DeserializeObject(dataJson));

    var response = await sendgridClient.SendEmailAsync(sendgridMessage);

    return response.IsSuccessStatusCode;
}

static Task<string> GetValidEmail()
{
    while (true)
    {
        Console.Write("E-poçt: ");
        string email = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(email) && IsValidEmail(email))
        {
            return Task.FromResult(email);
        }

        Console.WriteLine("E-poçt adresi düzgün deyil. Zəhmət olmasa yenidən yazın!");
    }
}

static string GetValidFullname()
{
    while (true)
    {
        Console.Write("Ad soyad: ");
        string fullname = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(fullname))
        {
            return fullname;
        }

        Console.WriteLine("Ad soyad boş ola bilməz. Zəhmət olmasa yenidən yazın!");
    }
}

static bool IsValidEmail(string email)
{
    try
    {
        var addr = new System.Net.Mail.MailAddress(email);
        return addr.Address == email;
    }
    catch
    {
        return false;
    }
}



// *
// **
// ***
// ****
// *****
// ******
// *******


// for (var i = 0; i < 7; i++)
// {
//     for (var j = 0; j < i; j++)
//     {
//         Console.Write("*");
//     }
//     Console.WriteLine();
// }

// * * * * * * *
// *           *
// *           *
// *           *
// *           *
// * * * * * * *

// for (var i = 0; i < 7; i++)
// {
//     for (var j = 0; j < 7; j++)
//     {
//         if (i == 0 || i == 6 || j == 0 || j == 6)
//         {
//             Console.Write("* ");
//         }
//         else
//         {
//             Console.Write("  ");
//         }
//     }
//     Console.WriteLine();
// }


// * * * * * * *
// * * * * * * *
// * * * * * * *
// * * * * * * *
// * * * * * * *
// * * * * * * *

// for (var i = 0; i < 7; i++)
// {
//     for (var j = 0; j < 7; j++)
//     {
//         Console.Write("* ");
//     }
//     Console.WriteLine();
// }


//Vurma cədvəli

// for (var i = 2; i <= 10; i++)
// {
//     for (var j = 1; j <= 10; j++)
//     {
//         Console.WriteLine($"{i} * {j} = {i * j}");
//     }
//     Console.WriteLine("--------");
// }