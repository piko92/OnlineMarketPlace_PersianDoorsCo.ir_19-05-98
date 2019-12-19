using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineMarketPlace.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Mail;
using OnlineMarketPlace.Models;
using OnlineMarketPlace.ClassLibraries.SMSService.SMSIR;
using OnlineMarketPlace.Models.AdminViewModels;

namespace OnlineMarketPlace.ClassLibraries.Authentication
{
    public class TokenGenerator
    {
        UserManager<ApplicationUser> _userManager;
        OnlineMarketContext _db;
        const string ApiKey = "ae868fb306a4d18320668fb";
        const string SecurityCode = "PersianDoorsCo!@#12345";
        const string lineNumber = "30004747474480";

        public TokenGenerator(UserManager<ApplicationUser> userManager, OnlineMarketContext db)
        {
            _userManager = userManager;
            _db = db;
            var expiredTokens = _db.Tokens.Where(x => (DateTime.Now - x.RegDateTime).TotalMinutes > 5 || x.Used == true).ToList();
            if (expiredTokens.Count > 0)
            {
                _db.Tokens.RemoveRange(expiredTokens);
                _db.SaveChanges();
            }
        }

        /// <summary>
        /// Return Parameters: 1 => SentBySMS, 2 => SentByEmail, 0 => nothing happens, -1 => Error
        /// </summary>
        /// <param name="_User"></param>
        /// <returns></returns>
        public async Task<int> SendVerificationToken(ApplicationUser _User)
        {
            try
            {
                if (_User != null)
                {
                    var setting = _db.Setting.FirstOrDefault();
                    string token = await GenerateForPhoneNumber(_User);
                    //string message = $"کد تایید شما: {token}";
                    string message =token;
                    var lastTokens = _db.Tokens.Where(x => x.UserId == _User.Id).ToList();
                    if (lastTokens.Count > 0)
                    {
                        _db.Tokens.RemoveRange(lastTokens);
                        _db.SaveChanges();
                    }

                    Tokens tokens = new Tokens()
                    {
                        UserId = _User.Id,
                        Token = token,
                        Used = false
                    };
                    await _db.Tokens.AddAsync(tokens);
                    await _db.SaveChangesAsync();

                    var isUserName_Number = double.TryParse(_User.UserName, out double r) ? r : 0;
                    if (isUserName_Number > 0)
                    {
                        //متن پیامک
                        //string message = smsText;
                        //شماره مقصد
                        string mobileNumber = _User.UserName;
                        var result = SmsIrService.SendVerificationCode(setting.SMSApiAddress, setting.SMSUsername, message, mobileNumber);

                        //SMSService.SMSService SMS = new SMSService.SMSService(_db); 
                        //SMS.SendSMS(new List<string> { _User.UserName }, message);
                        return 1;
                    }
                    else
                    {
                        //still not complete....

                        EmailViewModel emailViewModel = new EmailViewModel()
                        {
                            Subject = "ایمیل تاییدیه ثبت نام",
                            ReceiverEmail = _User.UserName,
                            Content = message,
                            SenderEmail = setting.AdminEmail,
                            Password = setting.AdminEmailPassword
                        };
                        var port = int.TryParse(setting.EmailPort, out int rr) ? rr : 587;
                        EmailService.EmailService.Send(emailViewModel, setting.EmailServiceProvider, port);

                        return 2;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }//end SendVerificationToken

        /// <summary>
        /// Return Parameters: 1 => RecievedBySMS, 2 => RecievedEmail, 0 => Nothing happens, -1 => Error
        /// </summary>
        /// <param name="_User"></param>
        /// <returns></returns>
        public async Task<int> ApproveVerificationToken(ApplicationUser _User, string token, string phoneNumber = null)
        {
            try
            {
                if (_User != null && token != null)
                {
                    var isUserName_Number = double.TryParse(_User.UserName, out double r) ? r : 0;
                    bool isVerified = false;
                    if (phoneNumber == null)
                    {
                        var tokenExist = _db.Tokens.Where(x => x.UserId == _User.Id && x.Token == token && x.Used == false).LastOrDefault();
                        if (tokenExist != null)
                        {
                            if ((DateTime.Now - tokenExist.RegDateTime).TotalMinutes <= 5)
                            {
                                isVerified = await _userManager.VerifyChangePhoneNumberTokenAsync(_User, token, _User.UserName);
                                tokenExist.Used = true;
                                _db.Tokens.Update(tokenExist);
                            }
                        }
                    }
                    else
                    {
                        var tokenExist = _db.Tokens.Where(x => x.UserId == _User.Id && x.Token == token && x.Used == false).LastOrDefault();
                        if (tokenExist != null)
                        {
                            if ((DateTime.Now - tokenExist.RegDateTime).Minutes <= 5)
                            {
                                isVerified = await _userManager.VerifyChangePhoneNumberTokenAsync(_User, token, phoneNumber);
                                tokenExist.Used = true;
                                _db.Tokens.Update(tokenExist);
                            }
                            else
                            {
                                return -2;
                            }
                        }
                    }

                    if (isVerified == true)
                    {
                        if (isUserName_Number > 0)
                        {
                            _User.PhoneNumberConfirmed = true;
                        }
                        else
                        {
                            _User.EmailConfirmed = true;
                        }
                        await _userManager.UpdateAsync(_User);
                        return 1; // success by SMS
                    }
                    else
                    {
                        return 0;
                    }
                    //if (isUserName_Number > 0)
                    //{

                    //}
                    //else
                    //{
                    //    return 2; // success by Email
                    //}
                }
                return 0;// no action
            }
            catch (Exception ex)
            {
                return -1; // error
            }
        }//end ApproveVerificationToken

        public async Task<int> ResetPassword(ApplicationUser _User)
        {
            if (_User != null)
            {
                var setting = _db.Setting.FirstOrDefault();
                var isUserName_Number = double.TryParse(_User.UserName, out double r) ? r : 0;

                var token = await GeneratePhoneNumberTokenAsync(_User);
                if (token != null)
                {
                   // string message = $"کد بازیابی کلمه عبور: {token}";
                    string message =token;

                    if (isUserName_Number > 0)
                    {
                        string mobileNumber = _User.UserName;
                        var result = SmsIrService.SendVerificationCode(setting.SMSApiAddress, setting.SMSUsername, message, mobileNumber);
                    }
                    else
                    {

                        EmailViewModel emailViewModel = new EmailViewModel()
                        {
                            Subject = "ایمیل تاییدیه ثبت نام",
                            ReceiverEmail = _User.UserName,
                            Content = message,
                            SenderEmail = setting.AdminEmail,
                            Password = setting.AdminEmailPassword
                        };
                        var port = int.TryParse(setting.EmailPort, out int rr) ? rr : 587;
                        EmailService.EmailService.Send(emailViewModel, setting.EmailServiceProvider, port);
                    }

                    var hasPassword = await _userManager.HasPasswordAsync(_User);
                    if (hasPassword == true)
                    {
                        var removePassword = await _userManager.RemovePasswordAsync(_User);
                    }
                    return 1;
                }

                
                return -2;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> VerifyResetPassword(ApplicationUser _User, string token, string newPassword)
        {
            if (_User != null && token != null)
            {
                PhoneNumberTokenProvider<ApplicationUser> phoneNumberTokenProvider = new PhoneNumberTokenProvider<ApplicationUser>();
                var isValidToken = await phoneNumberTokenProvider.ValidateAsync("ResetPassword", token, _userManager, _User);
                if (isValidToken)
                {
                    var status = await _userManager.AddPasswordAsync(_User, newPassword);
                    if (status.Succeeded)
                    {
                        return 1;
                    }
                }
                return -1;
            }
            return 0;
        }

        //======================================================================================================
        public async Task<string> GeneratePhoneNumberTokenAsync(ApplicationUser _User)
        {
            PhoneNumberTokenProvider<ApplicationUser> phoneNumberTokenProvider = new PhoneNumberTokenProvider<ApplicationUser>();
            var token = await phoneNumberTokenProvider.GenerateAsync("ResetPassword", _userManager, _User);
            return token;
        }

        private async Task<string> GenerateForPhoneNumber(ApplicationUser user)
        {
            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.UserName);
            return token;
        }
        private async Task<string> GenerateForEmail(ApplicationUser user)
        {
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, user.Email);
            return token;
        }
    }
}
