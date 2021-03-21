using AppSolutions.Platform.Models.MailManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Platform.Services.DataRepository.Impl
{
    public class MailManagementFileDataRepository : IMailManagementDataRepositoryService
    {
        private IList<MailTemplateHeader> _mailTemplateHeaders = new List<MailTemplateHeader>() 
        {
            new MailTemplateHeader
            {
                MailTemplateHeaderId = Guid.Parse("ac0ecec1-2dd0-421e-ae7d-2fbd87f4fec3"),
                RegisteredClientId = "0000000000",
                Customer = "AppSolutions",
                Name = "NewClientRegistrationUserMailAddressValidation",
                Description = "Email sent after registration of a new client to validate users email address",
                IsLocked = false,
                CreationDate = DateTime.Now,
                CreationUser = "m.schlestein"
            }
        };
        private IList<MailTemplateDetail> _mailTemplateDetails = new List<MailTemplateDetail>()
        {
            new MailTemplateDetail
            {
                MailTemplateHeaderId = Guid.Parse("ac0ecec1-2dd0-421e-ae7d-2fbd87f4fec3"),
                MailTemplateDetailId = Guid.Parse("041961d7-4ff7-4c39-95fe-2d70111709e3"),
                Subject = "[AppSolutions] Please verify your email address.",
                IsBodyHtml = true,
                Language = "EN",
                Body = @"
<div style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji;font-size:14px;line-height:1.5;color:#24292e;background-color:#fff;margin:0' bgcolor='#fff'>
    <table align='center' width='100%' style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;max-width:544px;margin-right:auto;margin-left:auto;width:100%!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
        <tbody>
            <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                <td align='center' valign='top' style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:16px'>
                    <center style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                        <table border='0' cellspacing='0' cellpadding='0' align='center' width='100%' style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;margin-right:auto;margin-left:auto;max-width:768px;width:100%!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                            <tbody>
                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                    <td align='center' style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>
                                        <table style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                            <tbody style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                    <td height='16' style='font-size:16px;line-height:16px;box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>&nbsp;</td>
                                                </tr>
                                            </tbody>
                                        </table>

                                        <table border='0' cellspacing='0' cellpadding='0' align='left' width='100%' style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                            <tbody>
                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                    <td style='box-sizing:border-box;text-align:left!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0' align='left'>
                                                        <h2 style='box-sizing:border-box;margin-top:8px!important;margin-bottom:0;font-weight:400!important;font-size:24px;line-height:1.25!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                            Almost an App Creator, <strong style='font-weight:600;box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>{{ first_name }} {{ last_name }}</strong>!

                                                        </h2>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                            <tbody style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                    <td height='16' style='font-size:16px;line-height:16px;box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>&nbsp;</td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width='100%' style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;width:100%!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                            <tbody>
                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                    <td style='box-sizing:border-box;border-radius:6px!important;display:block!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0;border:1px solid #e1e4e8'>
                                        <table align='center' style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;width:100%!important;text-align:center!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                            <tbody>
                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                    <td style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:16px'>
                                                        <table border='0' cellspacing='0' cellpadding='0' align='center' width='100%' style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;width:100%!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                            <tbody>
                                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                    <td align='center' style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>

                                                                        <table style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;width:100%!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                            <tbody style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>

                                                                                    <td style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>
                                                                                        <table style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                                            <tbody>
                                                                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                                                    <td style='box-sizing:border-box;text-align:left!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0' align='left'>
                                                                                                        To complete your sign up to the AppSolutions Team Server,
                                                                                                        we just need to verify your email address: <strong style='font-weight:600;box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'><a href='mailto:{{ mail_address }}' target='_blank'>{{ mail_address }}</a></strong>.
                                                                                                    </td>
                                                                                                    <td style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'></td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>

                                                                                </tr>
                                                                            </tbody>
                                                                        </table>

                                                                        <table style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;width:100%!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                            <tbody style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>

                                                                                    <td style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>
                                                                                        <table style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                                            <tbody>
                                                                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                                                    <td style='box-sizing:border-box;text-align:left!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0' align='left'>
                                                                                                        <table style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                                                            <tbody style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                                                                    <td height='16' style='font-size:16px;line-height:16px;box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>&nbsp;</td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>


                                                                                                        <table width='100%' border='0' cellspacing='0' cellpadding='0' style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                                                            <tbody>
                                                                                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                                                                    <td style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>
                                                                                                                        <table border='0' cellspacing='0' cellpadding='0' width='100%' style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                                                                            <tbody>
                                                                                                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                                                                                    <td align='center' style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>
                                                                                                                                        <a href='{{ verification_link }}' style='background-color:#28a745!important;box-sizing:border-box;color:#fff;text-decoration:none;display:inline-block;font-size:inherit;font-weight:500;line-height:1.5;white-space:nowrap;vertical-align:middle;border-radius:.5em;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:.75em 1.5em;border:1px solid #28a745' target='_blank' data-saferedirecturl='{{ verification_link }}'>Verify email address</a>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </tbody>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>

                                                                                                    </td>
                                                                                                    <td style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'></td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                        <table border='0' cellspacing='0' cellpadding='0' align='center' width='100%' style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;color:#6a737d!important;width:100%!important;font-size:14px!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                            <tbody>
                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                    <td align='center' style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>
                                        <table style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                            <tbody style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                    <td height='16' style='font-size:16px;line-height:16px;box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>&nbsp;</td>
                                                </tr>
                                            </tbody>
                                        </table>

                                        <table style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;width:100%!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                            <tbody style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>

                                                    <td style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>
                                                        <table style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                            <tbody>
                                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                    <td style='box-sizing:border-box;text-align:left!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0' align='left'>
                                                                        Once verified, you can start using all of our Team Server's features to create, build, and share apps.

                                                                        <table style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                            <tbody style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                                    <td height='16' style='font-size:16px;line-height:16px;box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>&nbsp;</td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>


                                                                        Button not working? Paste the following link into your browser: <br style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                                                                                                        <a href='{{ verification_link }}' style='background-color:initial;box-sizing:border-box;color:#0366d6;text-decoration:none;word-break:break-all!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important' target='_blank' data-saferedirecturl='{{ verification_link }}'>{{ verification_link }}</a>
                                                                    </td>
                                                                    <td style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>

                                                </tr>
                                            </tbody>
                                        </table>

                                        <hr style='box-sizing:initial;height:0;overflow:hidden;background-color:transparent;border-bottom-color:#dfe2e5;border-bottom-style:solid;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;margin:15px 0;border-width:0 0 1px'>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                        <table border='0' cellspacing='0' cellpadding='0' align='center' width='100%' style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;width:100%!important;text-align:center!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                            <tbody>
                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                    <td align='center' style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>
                                        <table style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                            <tbody style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                    <td height='16' style='font-size:16px;line-height:16px;box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>&nbsp;</td>
                                                </tr>
                                            </tbody>
                                        </table>

                                        <p style='box-sizing:border-box;margin-top:0;margin-bottom:10px;font-size:12px!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                            <a href='https://github.com/settings/emails' style='background-color:initial;box-sizing:border-box;color:#0366d6;text-decoration:none;display:inline-block!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important' target='_blank' data-saferedirecturl='https://www.google.com/url?q=https://github.com/settings/emails&amp;source=gmail&amp;ust=1616278636366000&amp;usg=AFQjCNHXTdVbu3Q_Ub1NVJo9QqcNsRN8zA'>Email preferences</a> ・
                                            <a href='https://docs.github.com/articles/github-terms-of-service/' style='background-color:initial;box-sizing:border-box;color:#0366d6;text-decoration:none;display:inline-block!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important' target='_blank' data-saferedirecturl='https://www.google.com/url?q=https://docs.github.com/articles/github-terms-of-service/&amp;source=gmail&amp;ust=1616278636366000&amp;usg=AFQjCNGHKY0xQ7idNSQ47b-sDt7qzKvszQ'>Terms</a> ・
                                            <a href='https://docs.github.com/articles/github-privacy-policy/' style='background-color:initial;box-sizing:border-box;color:#0366d6;text-decoration:none;display:inline-block!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important' target='_blank' data-saferedirecturl='https://www.google.com/url?q=https://docs.github.com/articles/github-privacy-policy/&amp;source=gmail&amp;ust=1616278636366000&amp;usg=AFQjCNHrFihYla2i_nAeNj8h7FYexDYvUA'>Privacy</a> ・
                                            <a href='https://github.com/login' style='background-color:initial;box-sizing:border-box;color:#0366d6;text-decoration:none;display:inline-block!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important' target='_blank' data-saferedirecturl='https://www.google.com/url?q=https://github.com/login&amp;source=gmail&amp;ust=1616278636366000&amp;usg=AFQjCNGSTLAx8Oet24J773srbpUFOsNsgw'>Sign in to AppSolutions Team Server</a>
                                        </p>
                                        <table style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                            <tbody style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                    <td height='16' style='font-size:16px;line-height:16px;box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>&nbsp;</td>
                                                </tr>
                                            </tbody>
                                        </table>

                                        <p style='box-sizing:border-box;margin-top:0;margin-bottom:10px;color:#6a737d!important;font-size:14px!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                            You’re receiving this email because you recently created a new AppSolutions account. If this wasn’t you, please ignore this email.
                                        </p>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table border='0' cellspacing='0' cellpadding='0' align='center' width='100%' style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;width:100%!important;text-align:center!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                            <tbody>
                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                    <td align='center' style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>
                                        <table style='box-sizing:border-box;border-spacing:0;border-collapse:collapse;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                            <tbody style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                <tr style='box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>
                                                    <td height='16' style='font-size:16px;line-height:16px;box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important;padding:0'>&nbsp;</td>
                                                </tr>
                                            </tbody>
                                        </table>

                                        <p style='box-sizing:border-box;margin-top:0;margin-bottom:10px;color:#6a737d!important;font-size:12px!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji!important'>AppSolutions, Ltd. ・Freischuetzstreet 82a ・81927 Munich, Germany</p>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </center>
                </td>
            </tr>
        </tbody>
    </table><div class='yj6qo'></div><div class='adL'>

    </div><div style='display:none;white-space:nowrap;box-sizing:border-box;font:15px apple-system,BlinkMacSystemFont,Segoe,UI,Helvetica,Arial,sans-serif,Apple,Color,Emoji,Segoe,UI,Emoji' class='adL'> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </div><div class='adL'>
    </div>
</div>
",
                CreationDate = DateTime.Now,
                CreationUser = "m.schlestein"
            }
        };

        public MailManagementFileDataRepository()
        {
            Directory.CreateDirectory(DataDirectory);

            if (!File.Exists(MailTemplateDetailsFileName) || !File.Exists(MailTemplateHeadersFileName))
            {
                SaveData();
            }

            var content = File.ReadAllText(MailTemplateHeadersFileName);
            _mailTemplateHeaders = JsonConvert.DeserializeObject<IList<MailTemplateHeader>>(content);

            content = File.ReadAllText(MailTemplateDetailsFileName);
            _mailTemplateDetails = JsonConvert.DeserializeObject<IList<MailTemplateDetail>>(content);
        }

        private string DataDirectory
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data");
            }
        }

        private string MailTemplateHeadersFileName
        {
            get
            {
                return Path.Combine(DataDirectory, "MailTemplateHeaders.json");
            }
        }

        private string MailTemplateDetailsFileName
        {
            get
            {
                return Path.Combine(DataDirectory, "MailTemplateDetails.json");
            }
        }

        private void SaveData()
        {
            File.WriteAllText(MailTemplateDetailsFileName, JsonConvert.SerializeObject(_mailTemplateDetails));
            File.WriteAllText(MailTemplateHeadersFileName, JsonConvert.SerializeObject(_mailTemplateHeaders));
        }

        public void CreateMailTemplateHeader(MailTemplateHeader header)
        {
            _mailTemplateHeaders.Add(header);

            SaveData();
        }

        public MailTemplateHeader GetMailTemplateById(Guid mailTemplateHeaderId)
        {
            var header = _mailTemplateHeaders.FirstOrDefault(o => o.MailTemplateHeaderId == mailTemplateHeaderId);
            header.DetailTemplates = _mailTemplateDetails.Where(o => o.MailTemplateHeaderId == header.MailTemplateHeaderId).ToList();
            return header;
        }

        public MailTemplateHeader GetMailTemplateByName(string registeredClientId, string customer, string name)
        {
            var header = _mailTemplateHeaders.FirstOrDefault(o => o.RegisteredClientId == registeredClientId && o.Customer == customer && o.Name == name);
            header.DetailTemplates = _mailTemplateDetails.Where(o => o.MailTemplateHeaderId == header.MailTemplateHeaderId).ToList();
            return header;
        }

        public ICollection<MailTemplateHeader> GetMailTemplatesOfClient(string registeredClientId)
        {
            var headers = _mailTemplateHeaders.Where(o => o.RegisteredClientId == registeredClientId).ToList();
            foreach (var h in headers)
            {
                h.DetailTemplates = _mailTemplateDetails.Where(o => o.MailTemplateHeaderId == h.MailTemplateHeaderId).ToList();
            }
            return headers;
        }

        public void UpdateMailTemplateHeader(MailTemplateHeader header)
        {
            var dbHeader = _mailTemplateHeaders.FirstOrDefault(c => c.MailTemplateHeaderId == header.MailTemplateHeaderId);
            header.CreationDate = dbHeader.CreationDate;
            header.CreationUser = dbHeader.CreationUser;
            _mailTemplateHeaders.Remove(dbHeader);
            _mailTemplateHeaders.Add(header);

            SaveData();
        }

        public void CreateMailTemplateDetail(MailTemplateDetail detail)
        {
            _mailTemplateDetails.Add(detail);

            SaveData();
        }

        public void UpdateMailTemplateDetail(MailTemplateDetail detail)
        {
            var dbDetail = _mailTemplateDetails.FirstOrDefault(c => c.MailTemplateDetailId == detail.MailTemplateDetailId);
            detail.CreationDate = dbDetail.CreationDate;
            detail.CreationUser = dbDetail.CreationUser;
            _mailTemplateDetails.Remove(dbDetail);
            _mailTemplateDetails.Add(detail);

            SaveData();
        }
    }
}
