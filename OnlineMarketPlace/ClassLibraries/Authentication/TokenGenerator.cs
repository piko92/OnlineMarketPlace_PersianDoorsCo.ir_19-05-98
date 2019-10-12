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

namespace OnlineMarketPlace.ClassLibraries.Authentication
{
    public class TokenGenerator
    {
        UserManager<ApplicationUser> _userManager;
        OnlineMarketContext _db;
        public TokenGenerator(UserManager<ApplicationUser> userManager, OnlineMarketContext db)
        {
            _userManager = userManager;
            _db = db;
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
                    var isUserName_Number = double.TryParse(_User.UserName, out double r) ? r : 0;
                    if (isUserName_Number > 0)
                    {
                        string token = await GenerateForPhoneNumber(_User);
                        string message = $"کد تایید شما: {token}";

                        Tokens tokens = new Tokens()
                        {
                            UserId = _User.Id,
                            Token = token,
                            Used = false
                        };
                        await _db.Tokens.AddAsync(tokens);
                        await _db.SaveChangesAsync();

                        SMSService.SMSService SMS = new SMSService.SMSService(_db);
                        SMS.SendSMS(new List<string> { _User.UserName }, message);
                        return 1;
                    }
                    else
                    {
                        //still not complete....
                        string token = await GenerateForEmail(_User);
                        string message = $@"<p>
                                        کاربر بنام کاربری : {_User.UserName}, جهت تایید آدرس ایمیل خود در وبسایت پرشین درب جنوب، بروی لینک زیر کلیک نمایید. <br/>
                                        {token}
                                    </p>";
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
                if (_User != null)
                {
                    var isUserName_Number = double.TryParse(_User.UserName, out double r) ? r : 0;
                    if (isUserName_Number > 0)
                    {
                        IdentityResult status;
                        if (phoneNumber == null)
                        {
                            var foundToken = _db.Tokens.Where(x => x.Token == token && x.UserId == _User.Id && x.Used == false).FirstOrDefault();
                            if (foundToken != null)
                            {
                                var pastTime = (DateTime.Now - foundToken.RegDateTime).TotalMinutes;
                                if (pastTime <= 5)
                                {
                                    bool isConfirmd = await _userManager.IsPhoneNumberConfirmedAsync(_User);
                                    if (isConfirmd == false)
                                    {
                                        _User.PhoneNumberConfirmed = true;
                                        await _userManager.UpdateAsync(_User);
                                        return 1; // success by SMS
                                    }
                                }
                            }
                        }
                        else
                        {
                            status = await _userManager.ChangePhoneNumberAsync(_User, phoneNumber, token);
                        }
                    }
                    else
                    {
                        return 2; // success by Email
                    }
                }
                return 0;// no action
            }
            catch(Exception ex)
            {
                return -1; // error
            }
        }//end ApproveVerificationToken


        //======================================================================================================
        private async Task<string> GenerateForPhoneNumber(ApplicationUser user)
        {
            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
            return token;
        }
        private async Task<string> GenerateForEmail(ApplicationUser user)
        {
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, user.Email);
            return token;
        }
    }
}
