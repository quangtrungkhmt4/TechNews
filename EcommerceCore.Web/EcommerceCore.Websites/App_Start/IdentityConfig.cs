using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using EcommerceCore.Websites.Models;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Diagnostics;
using Twilio.Types;
using Twilio.Clients;

namespace EcommerceCore.Websites
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                // Truy cập vào link https://www.google.com/settings/security/lesssecureapps
                //và bật quyền truy cập cho các ứng dụng kém an toàn.
                Credentials = new NetworkCredential("lasp1806e@gmail.com", "123456a@A"),       // Chỉnh email và password gửi đi
                EnableSsl = true,
            };

            var from = new MailAddress("lasp1806e@gmail.com", "Admin trang web thương mại điện tử");
            var to = new MailAddress(message.Destination);

            var mail = new MailMessage(from, to)
            {
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true,
            };

            client.Send(mail);
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var accountSid = ConfigurationManager.AppSettings["TwilioSid"];
            var authToken = ConfigurationManager.AppSettings["TwilioToken"];
            TwilioClient.Init(accountSid, authToken);


            var result = MessageResource.Create(
                to: new Twilio.Types.PhoneNumber(message.Destination),
                from: new Twilio.Types.PhoneNumber(ConfigurationManager.AppSettings["TwilioFromPhone"]),
                body: message.Body);
            //status is one of Queued, Sending, Sent, Failed or null if the number is not valid.
            Trace.TraceInformation(result.Status.ToString());

            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                // tối thiểu 6 kí tự
                RequiredLength = 6,
                // có kí tự đặc biệt
                RequireNonLetterOrDigit = true,
                // có kí tự số
                RequireDigit = true,
                // có chữ cái thường
                RequireLowercase = true,
                // có chữ cái in hoa
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            // khóa tài khoản 2 phút
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(2);
            // đăng nhập sai 5 lần sẽ bị khóa
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
