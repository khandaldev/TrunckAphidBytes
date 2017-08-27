using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class Email
    {

        string hosturl;
        string mailFormat;

        public string ByterAccount(Guid token,string HostURL)
        {

           // hosturl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Accounts/UserConfirmAccount?token=" + token;
            mailFormat = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'>" +
         "<tr>"
         + "<td align='left' valign='top'><!-- 600px fixed center aligned wrapper table -->" + "<table width='600' border='0' cellspacing='0' cellpadding='0' align='center' class='Fluid_wrapper'>" +
         "<tr>" +
         "<td align='left' valign='top' bgcolor='#636468'><!-- Header section -->" +
         "<table width='100%' border='0' cellspacing='0' cellpadding='0'>"
         + "<tr>" +
         "<td height='30' align='left' valign='top' style='border-bottom:1px solid #98b955; background:#FFFFFF;'><img src='http://aph500.trainingncr.com/Images/spacer.gif' 10' height='1' alt='' style='display:block;' /> " +
         " <p style='text-align:center; font-family:Arial, Helvetica, sans-serif; font-size:14px;'>1800-APH-BYTE  I   Store  I <a href='#' style=' text-decoration:none; color:#000000;'>www.aphidbyte.com</a></p></td> " +
         "</tr>" +
         "<tr>" +
          "<td height='15' align='center' valign='top' style='background:#FFF;'><img src='http://aph500.trainingncr.com/Images/bphidbyte_sicon.png' width='150' height='120' align='middle'/></td>" +
         "</tr> " +
         "</table>" + "</td>" +
         "</tr>" +
         "<tr>" +
         "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Content section -->" +
         "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
         "<tr>" +
         "</tr>" +
         "<tr>" +
         "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
         "<tr>" +
         "<td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" +
         "<td align='left' valign='top' class='Split_cells'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
         "  <tr>" +
         "    <td colspan='3' align='left' valign='top' class='Heading' style='font-family:A bite; font-size: 30px; color: #558ed5; text-decoration: none; line-height:30px;'><img src='http://aph500.trainingncr.com/Images/ByterWelcomeLogo.png' /></td>" +
         "  </tr>" +
         "  <tr>" +
         "    <td height='14' colspan='3' align='left' valign='top'><img src='http://aph500.trainingncr.com/Images/spacer.gif' width='1' height='14' alt='' style='display:block;' /></td>" +
         " </tr>" +
         " <tr>" +
         "   <td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 19px;'><p>Congratulations! You have just registered  your AphidByte Byter Account. Click the &ldquo;Get Started&rdquo; button to  proceed to your account. If you need assistance, do not hesitate to contact us.  Attached is a quick start guide to jump start your account. With the Byter Account you will receive the latest news  on your favorite films, art, photography, and music. Earn credits for watching  ads and taking surveys and use those credits for rewards and discounts in our  store.</p></td>" +
         "  </tr>" +
         "  <tr>" +
         "    <td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><p>.<samp style='font-family:'Admiration Pains'; font-size:12px;'> –Jeremy Sheckell, </samp>Vice President of Marketing.</p></td>" +
         "  </tr>" +
         "  <tr>" +
         "    <td height='14' colspan='3' align='left' valign='top'></td>" +
         "  </tr>" +
         "  <tr>" +
         "    <td width='34%' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'>&nbsp;</td>" +
         "    <td width='38%' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><img src='http://aph500.trainingncr.com/Images/byter_bg2.png' width='110' height='75' /></td>" +
         "    <td width='28%' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><a href='"+HostURL+"'><img src='http://aph500.trainingncr.com/Images/getstart_bg.png' width='115' height='50' /></a></td>" +
         "  </tr>" +
         "  <tr>" +
         "    <td align='left' valign='top'></td>" +
         "    <td align='center' valign='top'><img src='http://aph500.trainingncr.com/Images/expand.png' width='200' height='30' /></td>" +
         "    <td align='left' valign='top'>&nbsp;</td>" +
         "  </tr>" +
         "  </table></td>" +
         "<td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" +
         "</tr>" +
         "</table></td>" +
         "</tr>" +
         "<tr>" +
         "</tr>" +
         "</table>" +
         "<!-- Content section --></td>" +
         "</tr>" +
         "<tr>" +
         "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Footer section -->" +
         "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
         "<tr>" +
         "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
         "<tr>" +
         "<td align='left' valign='top' width='21'>&nbsp;</td>" +
         "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #7f7f7f; text-decoration: none; line-height:5px; text-align:center;'><p>Copyright and TM © 2013 AphidByte LLC. 5403 Treelodge Pkwy,  Atlanta, GA 30350. </p>" +
         "<p>All Rights Reserved/ Privacy Policy / If you&rsquo;ve received this email  in error, <a href='#' style='text-decoration:underline; color:#7f7f7f;'>Unsubscribe here</a></p></td>" +
         "<td align='left' valign='top' width='21'>&nbsp;</td>" +
         "</tr>" +
         "</table></td>" +
         "</tr>" +
         "<tr>" +
         "<td align='left' valign='top'></td>" +
         "</tr>" +
         "</table>" +
         "<!-- Footer section --></td>" +
         "</tr>" +
         "</table>" +
         "<!-- 600px fixed center aligned wrapper table --></td>" +
         "</tr>" +
         "</table>";
            return mailFormat;
        }
        public string basicAccount(Guid token, string HostURL)
        {

           // hosturl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Accounts/UserConfirmAccount?token=" + token;
            mailFormat = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'>" +
                         "<tr>" +
                         "<td align='left' valign='top'><!-- 600px fixed center aligned wrapper table -->" +
                         "<table width='600' border='0' cellspacing='0' cellpadding='0' align='center' class='Fluid_wrapper'>" +
                         "<tr>" +
                         "<td align='left' valign='top' bgcolor='#636468'><!-- Header section -->" +
                         "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                         "<tr>" +
                         "<td height='30' align='left' valign='top' style='border-bottom:1px solid #98b955; background:#FFFFFF;'><img src='http://aph500.trainingncr.com/Images/spacer.gif' 10' height='1' alt='' style='display:block;' /><p style='text-align:center; font-family:Arial, Helvetica, sans-serif; font-size:14px;'> 1800-APH-BYTE I Store I <a href='#' style=' text-decoration:none; color:#000000;'>www.aphidbyte.com</a></p></td>" +
                         "</tr>" +
                         "<tr>" +
                         "<td height='15' align='center' valign='top' style='background:#FFF;'><img src='http://aph500.trainingncr.com/Images/bphidbyte_sicon.png' width='150' height='120' align='middle'/></td>" +
                         "</tr>" +
                         "</table>" +
                         "</td>" + "</tr>" +
                         "<tr>" +
                         "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Content section --><table width='100%' border='0' cellspacing='0' cellpadding='0'>"
                         + "<tr>" +
                         "</tr>" +
                         "<tr>" +
                         "<td align='left' valign='top'>" +
                         "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                         "<tr>" +
                         "<td align='left' valign='top' width='20' class='No_mobile'>" +
                         "&nbsp;" +
                         "</td>" +
                         "<td align='left' valign='top' class='Split_cells'>" +
                         "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                         "<tr>" +
                         "<td colspan='3' align='left' valign='top' class='Heading' style='font-family:A bite; font-size: 30px; color: #77933c; text-decoration: none; line-height:30px;'>" +
                         "<img src='http://aph500.trainingncr.com/Images/BasicWelcomeLogo.gif'/>" +
                         "</td>" +
                         "</tr>" +
                         "<tr>" +
                         "<td height='14' colspan='3' align='left' valign='top'><img src='http://aph500.trainingncr.com/Images/spacer.gif' width='1' height='14' alt='' style='display:block;' />" +
                         "</td>" + "</tr> <tr>   <td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 19px;'><p>Congratulations! Your application for the AphidByte <strong style='color:#77933c;'>Basic Account</strong> has been approved. Now it&#8217;s time to <em>Expand Your Genius </em>and show the world. Click the &#8220;Get Started&#8221; button to proceed to your account. If you need assistance, do not hesitate to contact us. Attached is a quick start guide to jump start your account. Thanks for joining the AphidByte journey and we will be with you ever step of your career. You deserve it. Thank you. </p></td>" +
                         "</tr>  <tr>    <td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><p>.<samp style='font-family:'Admiration Pains'; font-size:12px;'> &#8211;Jeremy Sheckell, </samp>Vice President of Marketing.</p></td>  </tr>  <tr>    <td height='14' colspan='3' align='left' valign='top'></td>  </tr>  <tr>    <td width='34%' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'>&nbsp;</td>    <td width='38%' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'>" +
                         "<img src='http://aph500.trainingncr.com/Images/basicaccountbg.png' width='98' height='96' /></td>    <td width='28%' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><a href='" +HostURL + "'><img src='http://aph500.trainingncr.com/Images/getstart_bg.png' width='115' height='50' /></a></td>  </tr>  <tr>    <td align='left' valign='top'></td>    <td align='center' valign='top'>" +
                         "<img src='http://aph500.trainingncr.com/Images/expand.png' width='200' height='30' /></td>    <td align='left' valign='top'>&nbsp;</td>  </tr>  </table></td><td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td></tr></table></td></tr><tr></tr></table><!-- Content section --></td></tr><tr><td align='left' valign='top' bgcolor='#FFFFFF'><!-- Footer section --><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top' width='21'>&nbsp;</td><td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #7f7f7f; text-decoration: none; line-height:5px; text-align:center;'><p>Copyright and TM &copy; 2013 AphidByte LLC. 5403 Treelodge Pkwy,  Atlanta, GA 30350. </p><p>All Rights Reserved/ Privacy Policy / If you&rsquo;ve received this email  in error, <a href='#' style='text-decoration:underline; color:#7f7f7f;'>Unsubscribe here</a></p></td><td align='left' valign='top' width='21'>" +
                         "&nbsp;" +
                         "</td>" +
                         "</tr>" +
                         "</table>" +
                         "</td>" +
                         "</tr>" +
                         "<tr>" +
                         "<td align='left' valign='top'>" +
                         "</td>" +
                         "</tr>" +
                         "</table><!-- Footer section --></td></tr></table><!-- 600px fixed center aligned wrapper table -->" +
                         "</td>" +
                         "</tr>" +
                         "</table>";
            return mailFormat;
        }
        public string premiumAccount(Guid token, string HostURL)
        {

            //hosturl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Accounts/UserConfirmAccount?token=" + token;
            mailFormat = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'>" +
                       "<tr>" + "<td align='left' valign='top'><!-- 600px fixed center aligned wrapper table -->" +
                       "<table width='600' border='0' cellspacing='0' cellpadding='0' align='center' class='Fluid_wrapper'>" +
                       "<tr>" +
                       "<td align='left' valign='top' bgcolor='#636468'><!-- Header section -->" +
                       "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                       "<tr>" +
                       "<td height='30' align='left' valign='top' style='border-bottom:1px solid #98b955; background:#FFFFFF;'><img src='http://aph500.trainingncr.com/Images/spacer.gif' 10' height='1' alt='' style='display:block;' />  <p style='text-align:center; font-family:Arial, Helvetica, sans-serif; font-size:14px;'> 1800-APH-BYTE I Store I <a href='#' style=' text-decoration:none; color:#000000;'>www.aphidbyte.com</a></p>" +
                       "</td>" +
                       "</tr>" +
                       "<tr>" +
                       "<td height='15' align='center' valign='top' style='background:#FFF;'><img src='http://aph500.trainingncr.com/Images/bphidbyte_sicon.png' width='150' height='120' align='middle'/>" +
                       "</td>" + "</tr>" +
                       "</table>" +
                       "</td>" +
                       "</tr>" +
                       "<tr>" +
                       "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Content section -->" +
                       "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                       "<tr>" + "</tr>" +
                       "<tr>" +
                       "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                       "<tr>" +
                       "<td align='left' valign='top' width='20' class='No_mobile'>" +
                       "&nbsp;"
                       + "</td>" +
                       "<td align='left' valign='top' class='Split_cells'>" +
                       "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" + "<tr>" + "<td colspan='3' align='left' valign='top' class='Heading' style='font-family:A bite; font-size: 30px; color: #e46c0a; text-decoration: none; line-height:30px;'><img src='http://aph500.trainingncr.com/Images/PremiumWelcomeLogo.png'></td>" +
                       "</tr>" +
                       "<tr>" +
                       "<td height='14' colspan='3' align='left' valign='top'><img src='http://aph500.trainingncr.com/Images/spacer.gif' width='1' height='14' alt='' style='display:block;' />" +
                       "</td>" +
                       "</tr>" + "<tr>" +
                       "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 19px;'><p>Congratulations! Your application for the AphidByte <strong style='color:#e46c0a;'>Premium Account</strong> has been approved. Now it&#8217;s time to Expand Your Genius and show the world. Click &#8220;Get Started&#8221; button to proceed to your account. If you need assistance, do not hesitate to contact us. Attached is a quick start guide to jump start your account. Thanks for joining the AphidByte journey and we will be with you ever step of your career. You deserve it. Thank you. </p></td>" +
                       "</tr>" +
                       "<tr>" + "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><p>.<samp style='font-family:'Admiration Pains'; font-size:12px;'> &#8211;Jeremy Sheckell, </samp>Vice President of Marketing.</p></td>" +
                       "</tr>" + "<tr>" +
                       "<td height='14' colspan='3' align='left' valign='top'></td>  </tr>  <tr>    <td width='34%' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'>&nbsp;" + "</td>" +
                       "<td width='38%' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><img src='http://aph500.trainingncr.com/Images/premiumaccount_bg.png' width='113' height='115' />" + "</td>" + "<td width='28%' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><a href='" +HostURL  + "'><img src='http://aph500.trainingncr.com/Images/getstart_bg.png' width='115' height='50' /></a></td>" +
                       "</tr>" +
                       "<tr>" + "<td align='left' valign='top'>" +
                       "</td>" +
                       "<td align='center' valign='top'><img src='http://aph500.trainingncr.com/Images/expand.png' width='200' height='30' />" +
                       "</td>" +
                       "<td align='left' valign='top'>&nbsp;</td>" +
                       "</tr>" +
                       "</table>" + "</td>" + "<td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" + "</tr>" + "</table>" + "</td>" + "</tr>" + "<tr>" + "</tr>" + "</table>" + "<!-- Content section -->" + "</td>" + "</tr>" +
                       "<tr>" + "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Footer section -->" +
                       "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top'>" +
                       "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" + "<tr>" +
                       "<td align='left' valign='top' width='21'>&nbsp;</td>" +
                       "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #7f7f7f; text-decoration: none; line-height:5px; text-align:center;'><p>Copyright and TM &copy; 2013 AphidByte LLC. 5403 Treelodge Pkwy,  Atlanta, GA 30350. </p><p>All Rights Reserved/ Privacy Policy / If you&rsquo;ve received this email  in error, <a href='#' style='text-decoration:underline; color:#7f7f7f;'>Unsubscribe here</a></p></td><td align='left' valign='top' width='21'>&nbsp;</td>" + "</tr>" +
                       "</table>" + "</td>" + "</tr>" + "<tr>" + "<td align='left' valign='top'>" + "</td>" + "</tr>" + "</table><!-- Footer section --></td></tr>" +
                       "</table><!-- 600px fixed center aligned wrapper table -->" + "</td>" + "</tr>" + "</table>";
            return mailFormat;
        }
        public void AphidTiseAccoount(Guid token)
        {

            hosturl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Accounts/UserConfirmAccount?token=" + token;
            mailFormat = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'>" +
               "<tr>" +
               "<td align='left' valign='top'><!-- 600px fixed center aligned wrapper table -->" +
               "<table width='600' border='0' cellspacing='0' cellpadding='0' align='center' class='Fluid_wrapper'>" +
               "<tr>" +
               "<td align='left' valign='top' bgcolor='#636468'><!-- Header section -->" +
               "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
               "<tr>" +
               "<td height='30' align='left' valign='top' style='border-bottom:1px solid #98b955; background:#FFFFFF;'><img src='http://aph500.trainingncr.com/Images/spacer.gif' 10' height='1' alt='' style='display:block;' />  <p style='text-align:center; font-family:Arial, Helvetica, sans-serif; font-size:14px;'> 1800-APH-BYTE I Store I <a href='#' style=' text-decoration:none; color:#000000;'>www.aphidbyte.com</a></p></td>" +
               "</tr>" +
               "<tr>" +
               "<td height='15' align='center' valign='top' style='background:#FFF;'><img src='http://aph500.trainingncr.com/Images/bphidbyte_sicon.png' width='150' height='120' align='middle'/>" +
               "</td>" +
               "</tr>" +
               "</table>" +
               "</td>" +
               "</tr>" +
               "<tr>" +
               "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Content section --><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
               "<tr></tr><tr><td align='left' valign='top'>" +
               "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" +
               "<td align='left' valign='top' class='Split_cells'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
               "<tr>" +
               "<td colspan='3' align='left' valign='top' class='Heading' style='font-family:A bite; font-size: 30px; color: #c00000; text-decoration: none; line-height:30px;'><img src='http://aph500.trainingncr.com/Images/AphidtiseWelcomeLogo.png'/></td>" +
               "</tr>" +
               "<tr>" +
               "<td height='14' colspan='3' align='left' valign='top'><img src='http://aph500.trainingncr.com/Images/spacer.gif' width='1' height='14' alt='' style='display:block;' /></td>" +
               "</tr>" + "<tr>" +
               "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 19px;'><p>Congratulations! Your application for the AphidByte <strong style='color:#c00000;'>Aphidtise Account</strong> has been approved. Now it&#8217;s time to Expand Your Genius and show the world. Click the &#8220;Get Started&#8221; button to proceed to your account. If you need assistance, do not hesitate to contact us. Attached is a quick start guide to jump start your account. Thanks for joining the AphidByte journey and we will be with you ever step of your career. You deserve it. Thank you.</p></td>" +
               "</tr>" +
               "<tr>" +
               "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><p>.<samp style='font-family:'Admiration Pains'; font-size:12px;'> &#8211;Jeremy Sheckell,</samp>Vice President of Marketing.</p></td>" +
               "</tr>" +
               "<tr>" +
               "<td height='14' colspan='3' align='left' valign='top'>" +
               "</td>" +
               "</tr>" +
               "<tr>" +
               "<td width='34%' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'>&nbsp;</td>" +
               "<td width='38%' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><img src='http://aph500.trainingncr.com/Images/aphidtiseaccountbg.png' width='130' height='115' />" +
               "</td>" +
               "<td width='28%' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><a href='" + hosturl + "'><img src='http://aph500.trainingncr.com/Images/getstart_bg.png' width='115' height='50' /></a>" + "</td>" + "</tr>" + "<tr>" +
               "<td align='left' valign='top'>" +
               "</td>" +
               "<td align='center' valign='top'><img src='http://aph500.trainingncr.com/Images/expand.png' width='200' height='30' />" +
               "</td>" +
               "<td align='left' valign='top'>&nbsp;</td>" +
               "</tr>" +
               "</table>" +
               "</td>" +
               "<td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" +
               "</tr>" +
               "</table>" +
               "</td>" +
               "</tr>" +
               "<tr>" +
               "</tr>" +
               "</table><!-- Content section --></td>" + "</tr>" + "<tr>" +
               "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Footer section -->" + "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" + "<tr>" +
               "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top' width='21'>&nbsp;</td>" +
               "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #7f7f7f; text-decoration: none; line-height:5px; text-align:center;'><p>Copyright and TM &copy; 2013 AphidByte LLC. 5403 Treelodge Pkwy,  Atlanta, GA 30350. </p><p>All Rights Reserved/ Privacy Policy / If you&rsquo;ve received this email  in error, <a href='#' style='text-decoration:underline; color:#7f7f7f;'>Unsubscribe here</a></p></td><td align='left' valign='top' width='21'>&nbsp;</td>" +
               "</tr>" +
               "</table>" +
               "</td>" +
               "</tr>" +
               "<tr>" +
               "<td align='left' valign='top'></td></tr>" +
               "</table>" + "<!-- Footer section -->" + "</td>" + "</tr>" + "</table>" + "<!-- 600px fixed center aligned wrapper table -->" +
               "</td>" +
               "</tr>" +
               "</table>";

        }

        public string AphidLabAccoount(Guid token, string HostURL)
        {

           // hosturl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Accounts/UserConfirmAccount?token=" + token;
            mailFormat = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'>" +
               "<tr>" +
               "<td align='left' valign='top'><!-- 600px fixed center aligned wrapper table -->" +
               "<table width='600' border='0' cellspacing='0' cellpadding='0' align='center' class='Fluid_wrapper'>" +
               "<tr>" +
               "<td align='left' valign='top' bgcolor='#636468'><!-- Header section -->" +
               "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
               "<tr>" +
               "<td height='30' align='left' valign='top' style='border-bottom:1px solid #98b955; background:#FFFFFF;'><img src='http://aph500.trainingncr.com/Images/spacer.gif' 10' height='1' alt='' style='display:block;' />  <p style='text-align:center; font-family:Arial, Helvetica, sans-serif; font-size:14px;'> 1800-APH-BYTE I Store I <a href='#' style=' text-decoration:none; color:#000000;'>www.aphidbyte.com</a></p></td>" +
               "</tr>" +
               "<tr>" +
               "<td height='15' align='center' valign='top' style='background:#FFF;'><img src='http://aph500.trainingncr.com/Images/bphidbyte_sicon.png' width='150' height='120' align='middle'/>" +
               "</td>" +
               "</tr>" +
               "</table>" +
               "</td>" +
               "</tr>" +
               "<tr>" +
               "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Content section --><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
               "<tr></tr><tr><td align='left' valign='top'>" +
               "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" +
               "<td align='left' valign='top' class='Split_cells'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
               "<tr>" +
               "<td colspan='3' align='left' valign='top' class='Heading' style='font-family:A bite; font-size: 30px; color: #c00000; text-decoration: none; line-height:30px;'><img src='http://aph500.trainingncr.com/Images/AphidtiseWelcomeLogo.png'/></td>" +
               "</tr>" +
               "<tr>" +
               "<td height='14' colspan='3' align='left' valign='top'><img src='http://aph500.trainingncr.com/Images/spacer.gif' width='1' height='14' alt='' style='display:block;' /></td>" +
               "</tr>" + "<tr>" +
               "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 19px;'><p>Congratulations! Your application for the AphidByte <strong style='color:#c00000;'>Aphidtise Account</strong> has been approved. Now it&#8217;s time to Expand Your Genius and show the world. Click the &#8220;Get Started&#8221; button to proceed to your account. If you need assistance, do not hesitate to contact us. Attached is a quick start guide to jump start your account. Thanks for joining the AphidByte journey and we will be with you ever step of your career. You deserve it. Thank you.</p></td>" +
               "</tr>" +
               "<tr>" +
               "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><p>.<samp style='font-family:'Admiration Pains'; font-size:12px;'> &#8211;Jeremy Sheckell,</samp>Vice President of Marketing.</p></td>" +
               "</tr>" +
               "<tr>" +
               "<td height='14' colspan='3' align='left' valign='top'>" +
               "</td>" +
               "</tr>" +
               "<tr>" +
               "<td width='34%' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'>&nbsp;</td>" +
               "<td width='38%' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><img src='http://aph500.trainingncr.com/Images/aphidtiseaccountbg.png' width='130' height='115' />" +
               "</td>" +
               "<td width='28%' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><a href='" + HostURL + "'><img src='http://aph500.trainingncr.com/Images/getstart_bg.png' width='115' height='50' /></a>" + "</td>" + "</tr>" + "<tr>" +
               "<td align='left' valign='top'>" +
               "</td>" +
               "<td align='center' valign='top'><img src='http://aph500.trainingncr.com/Images/expand.png' width='200' height='30' />" +
               "</td>" +
               "<td align='left' valign='top'>&nbsp;</td>" +
               "</tr>" +
               "</table>" +
               "</td>" +
               "<td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" +
               "</tr>" +
               "</table>" +
               "</td>" +
               "</tr>" +
               "<tr>" +
               "</tr>" +
               "</table><!-- Content section --></td>" + "</tr>" + "<tr>" +
               "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Footer section -->" + "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" + "<tr>" +
               "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top' width='21'>&nbsp;</td>" +
               "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #7f7f7f; text-decoration: none; line-height:5px; text-align:center;'><p>Copyright and TM &copy; 2013 AphidByte LLC. 5403 Treelodge Pkwy,  Atlanta, GA 30350. </p><p>All Rights Reserved/ Privacy Policy / If you&rsquo;ve received this email  in error, <a href='#' style='text-decoration:underline; color:#7f7f7f;'>Unsubscribe here</a></p></td><td align='left' valign='top' width='21'>&nbsp;</td>" +
               "</tr>" +
               "</table>" +
               "</td>" +
               "</tr>" +
               "<tr>" +
               "<td align='left' valign='top'></td></tr>" +
               "</table>" + "<!-- Footer section -->" + "</td>" + "</tr>" + "</table>" + "<!-- 600px fixed center aligned wrapper table -->" +
               "</td>" +
               "</tr>" +
               "</table>";
            return mailFormat;
        }
        public string  ForgetPassword(Guid token, string username)
        {
            hosturl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Accounts/UserConfirmationForgrtPassword?token=" + token;
            mailFormat = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'>" +
              "<tr>" +
              "<td align='left' valign='top'><!-- 600px fixed center aligned wrapper table --><table width='600' border='0' cellspacing='0' cellpadding='0' align='center' class='Fluid_wrapper'>" +
              "<tr>" +
              "<td align='left' valign='top' bgcolor='#636468'><!-- Header section -->" +
                 "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" + "<tr>" +
                 "<td height='30' align='left' valign='top' style='border-bottom:1px solid #98b955; background:#FFFFFF;'><img src='images/spacer.gif' 10' height='1' alt='' style='display:block;' />  <p style='text-align:center; font-family:Arial, Helvetica, sans-serif; font-size:14px;'> 1800-APH-BYTE I Store I <a href='#' style=' text-decoration:none; color:#000000;'>www.aphidbyte.com</a></p></td>" +
                     "</tr>" +
                     "<tr>" + "<td height='7' align='center' valign='top' style='background:#FFF;'><img src='http://aph500.trainingncr.com/Images/bphidbyte_sicon.png' width='150' height='120' align='middle'/>" +
                     "</td>" +
                     "</tr>" +
                     "<tr>" +
                     "<td height='8' align='center' valign='top' style='background:#FFF;'><img src='http://aph500.trainingncr.com/Images/lock_image.png' width='66' height='64' />" +
                      "</td>" +
                      "</tr>" +
                      "</table>" +
                      "</td>" +
                      "</tr>" +
                      "<tr>" +
                      "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Content section -->" +
                      "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                      "<tr>" +
                      "</tr>" +
                      "<tr>" +
                      "<td align='left' valign='top'>" +
                      "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" + "<tr>" + "<td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" +
                      "<td align='left' valign='top' class='Split_cells'>" +
                      "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                      "<tr>" +
                      "</tr>" +
                      "<tr>" +
                      "<td height='14' colspan='3' align='left' valign='top'>&nbsp;</td>" +
                      "</tr>" +
                      "<tr>" +
                      "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 19px;'><p>Hello,  [" + username + "]</p></td>" +
                      "</tr>" +
                      "<tr>" +
                      "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><p>To  reset your password on AphidByte, click here:</p></td>" +
                      "</tr>" +
                      "<tr>" +
                      "<td height='7' colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color:#3131fe; text-decoration: none; line-height: 16px;'><p><a href=" + hosturl + ">" + hosturl + "</a>.</p></td>" +
                      "</tr>" +
                      "<tr>" +
                      "<td height='7' colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color:#353e44; text-decoration: none; line-height: 16px;'>If  you believe these changes were made in error, or if you believe an unauthorized  person accessed your account, please&nbsp;contact our accounts  department&nbsp;immediately by going to <a href='http://www.aphidbyte.com/support'>www.aphidbyte.com/support</a><br /></td>" +
                      "</tr>" +
                      "<tr>" +
                      "<td height='7' colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><p>Thank you,</p></td>" +
                      "</tr>" +
                      "<tr>" +
                      "<td height='7' colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><p>AphidByte Staff</p></td>" +
                       "</tr>" +
                       "<tr>" +
                       "<td height='14' colspan='3' align='left' valign='top'>" + "</td>" + "</tr>" + "<tr>" +
                       "<td align='left' valign='top'>" +
                       "</td>" +
                       "<td align='center' valign='top'><img src='http://aph500.trainingncr.com/Images/expand.png' width='200' height='30' />" +
                       "</td>" +
                       "<td align='left' valign='top'>&nbsp;</td>" +
                       "</tr>  </table></td><td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" + "</tr>" +
                       "</table>" +
                       "</td>" +
                       "</tr>" +
                       "<tr>" +
                       "</tr>" +
                       "</table>" + "<!-- Content section --></td>" +
                       "</tr>" +
                       "<tr>" +
                       "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Footer section -->" +
                       "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                       "<tr>" +
                       "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                       "<tr>" +
                       "<td align='left' valign='top' width='21'>&nbsp;</td>" +
                       "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #7f7f7f; text-decoration: none; line-height:5px; text-align:center;'>" +
                       "<p>Copyright and TM &copy; 2013 AphidByte LLC. 5403 Treelodge Pkwy,  Atlanta, GA 30350. </p>" +
                       "<p>All Rights Reserved/ Privacy Policy / If you&rsquo;ve received this email  in error, <a href='#' style='text-decoration:underline; color:#7f7f7f;'>Unsubscribe here</a></p>" +
                       "</td>" +
                       "<td align='left' valign='top' width='21'>&nbsp;</td>" +
                       "</tr>" +
                       "</table>" +
                       "</td>" +
                       "</tr>" +
                       "<tr>" +
                       "<td align='left' valign='top'>" +
                       "</td>" +
                       "</tr>" +
                       "</table><!-- Footer section -->" +
                       "</td>" +
                       "</tr>" +
                       "</table>";

            return mailFormat;

        }

        public void FeedBackMail()
        {
            mailFormat = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'>" +
                   "<tr>" +
                   "<td align='left' valign='top'>" +
                   "<!-- 600px fixed center aligned wrapper table -->" +
                   "<table width='600' border='0' cellspacing='0' cellpadding='0' align='center' class='Fluid_wrapper'>" +
                   "<tr>" +
                   "<td align='left' valign='top' bgcolor='#636468'><!-- Header section -->" +
                   "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                   "<tr>" +
                   "<td height='30' align='left' valign='top' style='border-bottom:1px solid #98b955; background:#FFFFFF;'><img src='http://aph500.trainingncr.com/Images/spacer.gif' 10' height='1' alt='' style='display:block;'/>" +
                   "<p style='text-align:center; font-family:Arial, Helvetica, sans-serif; font-size:14px;'> 1800-APH-BYTE I Store I <a href='#' style=' text-decoration:none; color:#000000;'>www.aphidbyte.com</a></p>" +
                   "</td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td height='15' align='center' valign='top' style='background:#FFF;'><img src='http://aph500.trainingncr.com/Images/bphidbyte_sicon.png' width='150' height='120' align='middle'/></td>" +
                   "</tr>" +
                   "</table>" +
                   "</td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td align='left' valign='top' bgcolor='#FFFFFF'>" +
                   "<!-- Content section -->" +
                   "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr></tr><tr><td align='left' valign='top'>" +
                   "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" +
                   "<td align='left' valign='top' class='Split_cells'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                   "<tr>" +
                   "<td colspan='3' align='left' valign='top' class='Heading' style='font-family:A bite; font-size: 30px; color: #595959; text-decoration: none; line-height:30px;'>FeedBack</td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td height='14' colspan='3' align='left' valign='top'><img src='http://aph500.trainingncr.com/Images/spacer.gif' width='1' height='14' alt='' style='display:block;'/>" +
                   "</td>" + "</tr>" +
                   "<tr>" +
                   "<td colspan='3' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 15px; color: #353e44; text-decoration: none; line-height: 19px;'>" +
                   "<p>Thanks you for your feedback. We appreciate your </p>" +
                   "</td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td colspan='3' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 15px; color: #353e44; text-decoration: none; line-height: 16px;'><p>business. Have a great day.</p>" + "</td>" + "</tr>" + "<tr>" +
                   "<td height='7' colspan='3' align='left' valign='top'></td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td height='7' colspan='3' align='left' valign='top'>" + "</td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td height='14' colspan='3' align='left' valign='top'></td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><a href='#'></a>" +
                   "<table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>" +
                   "<tr>" +
                    "<td width='81%' colspan='2' align='center' valign='middle'>&nbsp;</td>" +
                  "</tr>" +
                  "</table>" + "</td>" +
                  "</tr>" +
                  "<tr>" +
                  "<td width='26%' align='left' valign='top'>" +
                  "</td>" +
                  "<td width='45%' align='center' valign='top'><img src='http://aph500.trainingncr.com/Images/expand.png' width='200' height='30' /></td>" +
                  "<td width='29%' align='left' valign='top'>&nbsp;</td>" +
                  "</tr>" +
                  "</table>" +
                  "</td>" +
                  "<td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" +
                  "</tr>" + "</table>" +
                  "</td>" +
                  "</tr>" +
                  "<tr>" +
                  "</tr>" +
                  "</table>" +
                  "<!-- Content section --></td>" +
                  "</tr>" +
                  "<tr>" +
                  "<td align='left' valign='top' bgcolor='#FFFFFF'>" +
                  "<!-- Footer section -->" +
                  "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top'>" +
                  "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top' width='21'>&nbsp;</td>" +
                  "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #7f7f7f; text-decoration: none; line-height:5px; text-align:center;'><p>Copyright and TM &copy; 2013 AphidByte LLC. 5403 Treelodge Pkwy,  Atlanta, GA 30350. </p><p>All Rights Reserved/ Privacy Policy / If you&rsquo;ve received this email  in error, <a href='#' style='text-decoration:underline; color:#7f7f7f;'>Unsubscribe here</a></p></td><td align='left' valign='top' width='21'>&nbsp;</td>" +
                  "</tr>" +
                  "</table>" +
                  "</td>" +
                  "</tr>" +
                  "<tr>" +
                  "<td align='left' valign='top'>" +
                  "</td>" +
                  "</tr>" +
                  "</table>" +
                  "<!-- Footer section --></td>" +
                  "</tr>" +
                  "</table>" +
                  "<!-- 600px fixed center aligned wrapper table --></td>" +
                  "</tr>" +
                  "</table>";
        }


        string slider8 = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'><tr><td align='left' valign='top'>" +
            "<!-- 600px fixed center aligned wrapper table --><table width='600' border='0' cellspacing='0' cellpadding='0' align='center' class='Fluid_wrapper'>" +
            "<tr>" +
            "<td align='left' valign='top' bgcolor='#636468'><!-- Header section --><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
            "<tr>" +
            "<td height='30' colspan='3' align='left' valign='top' style='border-bottom:1px solid #98b955; background:#FFFFFF;'><img src='http://aph500.trainingncr.com/Images/spacer.gif' 10' height='1' alt='' style='display:block;' />  <p style='text-align:center; font-family:Arial, Helvetica, sans-serif; font-size:14px;'> 1800-APH-BYTE I Store I <a href='#' style=' text-decoration:none; color:#000000;'>www.aphidbyte.com</a></p></td>" +
            "</tr>" +
            "<tr>" +
            "<td height='7' colspan='3' align='center' valign='top' style='background:#FFF;'><img src='http://aph500.trainingncr.com/Images/bphidbyte_sicon.png' width='150' height='120' align='middle'/>" + "</td>" + "</tr>" +
            "<tr>" +
            "<td width='42%' height='8' align='center' valign='top' style='background:#FFF;'>&nbsp;</td>" +
             "<td width='13%' align='center' valign='top' style='background:#FFF;'><img src='http://aph500.trainingncr.com/Images/lock_image.png' width='66' height='64' /></td>" +
             "<td width='45%' align='left' valign='middle' style='background:#FFF; font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44;'>" +
             "<p>Password Reset</p></td>" +
            "</tr>" +
            "</table>" +
            "</td>" +
            "</tr>" + "<tr>" + "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Content section -->" +
            "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" + "<tr>" + "</tr>" + "<tr>" +
            "<td align='left' valign='top'>" +
            "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td><td align='left' valign='top' class='Split_cells'><table width='100%' border='0' cellspacing='0' cellpadding='0'>  <tr>    <td height='14' colspan='3' align='left' valign='top'>&nbsp;</td> </tr> <tr>   <td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #5d5b5b; text-decoration: none; line-height: 19px;'><p>Limit 8 characters</p></td>  </tr>  <tr>    <td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'>" +
            "<table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>" +
            "<tr>" +
            "<td width='19%'>&nbsp;</td>" +
            "<td width='19%' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color:#000; line-height:45px'>New  Password</td>" +
            "<td width='3%'>&nbsp;</td>" +
            "<td width='20%'>" +
            "<label for='textarea'></label>" +
             "<input name='textarea' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color:#353e44; border: 1px solid #000000; padding:5px; box-shadow: 0 4px 2px -2px rgba(0,0,0,0.4)' type='text' id='textarea' value='' size='20' / >" + "</td>" +
       "<td width='31%'>&nbsp;</td>" +
       "<td width='8%'>&nbsp;</td>" +
     "</tr>" +
     "<tr>" +
       "<td>&nbsp;</td>" +
       "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color:#000; '><p>Verify Password: </p></td>" +
       "<td>&nbsp;</td>" +
       "<td ><input name='textarea2' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color:#353e44; border: 1px solid #000000; padding:5px; box-shadow: 0 4px 2px -2px rgba(0,0,0,0.4)' type='text' id='textarea2' value='' size='20' />" +
       "</td>" +
       "<td>&nbsp;</td>" +
       "<td>&nbsp;</td>" +
     "</tr>" +
     "<tr>" +
       "<td colspan='6'>&nbsp;</td>" +
     "</tr>" +
     "</table></td>" +
        "</tr>" +
                     "<tr>" +
                      "<td width='34%' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'>&nbsp;</td>    <td width='38%' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'>&nbsp;</td>" +
                       "<td width='28%' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><a href='#'><img src='http://aph500.trainingncr.com/Images/submit_bg.png' width='115' height='50' /></a></td>" +
                        "</tr>" +
                        "<tr>" +
                       "<td align='left' valign='top'></td>" +
                        "<td align='center' valign='top'><img src='http://aph500.trainingncr.com/Images/expand.png' width='200' height='30' /></td>" +
                       "<td align='left' valign='top'>&nbsp;</td>" +
                       "</tr>" +
                       "</table>" +
                       "</td>" +
                       "<td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" +
                       "</tr>" + "</table>" + "</td>" + "</tr>" + "<tr>" + "</tr>" + "</table>" + "<!-- Content section -->" + "</td>" + "</tr>" + "<tr>" +
                       "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Footer section -->" +
                       "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                       "<tr>" +
                       "<td align='left' valign='top'>" +
                       "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                       "<tr>" +
                       "<td align='left' valign='top' width='21'>&nbsp;</td>" +
                       "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #7f7f7f; text-decoration: none; line-height:5px; text-align:center;'>" +
                       "<p>Copyright and TM &copy; 2013 AphidByte LLC. 5403 Treelodge Pkwy,  Atlanta, GA 30350. </p>" +
                       "<p>All Rights Reserved/ Privacy Policy / If you&rsquo;ve received this email  in error, <a href='#' style='text-decoration:underline; color:#7f7f7f;'>Unsubscribe here</a></p>" + "</td>" +
                       "<td align='left' valign='top' width='21'>&nbsp;</td>" +
                       "</tr>" +
                       "</table>" +
                       "</td>" +
                       "</tr>" +
                       "<tr>" + "<td align='left' valign='top'>" + "</td>" + "</tr>" + "</table>" + "<!-- Footer section -->" + "</td>" + "</tr>" + "</table>" +
                       "<!-- 600px fixed center aligned wrapper table -->" +
                       "</td>" +
                       "</tr>" + "</table>";
        string slider9 = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'>" +
            "<tr>" +
            "<td align='left' valign='top'><!-- 600px fixed center aligned wrapper table --><table width='600' border='0' cellspacing='0' cellpadding='0' align='center' class='Fluid_wrapper'>" +
            "<tr>" +
            "<td align='left' valign='top' bgcolor='#636468'><!-- Header section -->" +
            "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
            "<tr>" +
            "<td height='30' colspan='3' align='left' valign='top' style='border-bottom:1px solid #98b955; background:#FFFFFF;'><img src='http://aph500.trainingncr.com/Images/spacer.gif' 10' height='1' alt='' style='display:block;' /><p style='text-align:center; font-family:Arial, Helvetica, sans-serif; font-size:14px;'> 1800-APH-BYTE I Store I <a href='#' style=' text-decoration:none; color:#000000;'>www.aphidbyte.com</a></p></td>" +
            "</tr>" +
            "<tr>" +
            "<td height='7' colspan='3' align='center' valign='top' style='background:#FFF;'><img src='http://aph500.trainingncr.com/Images/bphidbyte_sicon.png' width='150' height='120' align='middle'/>" + "</td>" +
            "</tr>" +
            "<tr>" + "<td width='42%' height='8' align='center' valign='top' style='background:#FFF;'>&nbsp;</td>" +
            "<td width='13%' align='center' valign='top' style='background:#FFF;'><img src='http://aph500.trainingncr.com/Images/lock_image.png' width='66' height='64' /></td>" +
            "<td width='45%' align='left' valign='middle' style='background:#FFF; font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44;'>" +
            "<p>Password Reset</p>" + "</td>" +
            "</tr>" +
            "</table>" + "</td>" + "</tr>" +
            "<tr>" +
            "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Content section -->" +
            "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr></tr><tr><td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" + "<tr>" +
            "<td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" +
            "<td align='left' valign='top' class='Split_cells'>" +
            "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
            "<tr>" +
            "<td height='14' colspan='3' align='left' valign='top'>&nbsp;</td>" +
            "</tr>" +
            "<tr>" +
            "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #ff0000; text-decoration: none; line-height: 19px; font-weight:bold;'>" + "<p>Limit 8 characters.Please re-renter a new password</p>" + "</td>" +
            "</tr>" +
            "<tr>" +
            "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>" +
            "<tr>" +
            "<td width='19%'>&nbsp;</td>" +
           "<td width='19%' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color:#000; line-height:45px'>New  Password</td>" +
           "<td width='3%'>&nbsp;</td>" +
          "<td width='20%'><label for='textarea'></label><input name='textarea' type='password'  id='textarea' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color:#353e44; border: 1px solid #000000; padding:5px; box-shadow: 0 4px 2px -2px rgba(0,0,0,0.4)' value='hhhhhhh' size='20'/>" +
          "</td>" +
          "<td width='31%'>&nbsp;</td>" +
         "<td width='8%'>&nbsp;</td>" +
        "</tr>" +
       "<tr>" +
       "<td>&nbsp;</td>" +
       "<td style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color:#000; '><p>Verify Password: </p></td>" +
       "<td>&nbsp;</td>" +
       "<td ><input name='textarea2' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color:#353e44; border: 1px solid #000000; padding:5px; box-shadow: 0 4px 2px -2px rgba(0,0,0,0.4)' type='password' id='textarea2' value='hhhhhhh' size='20' /></td>" +
       "<td>&nbsp;</td>" +
       "<td>&nbsp;</td>" +
      "</tr>" +
      "<tr>" +
       "<td colspan='6>&nbsp;</td>" +
      "</tr>" +
     "</table>" + "</td>" +
     "</tr>" +
                     "<tr>" +
                       "<td width='34%' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'>&nbsp;</td>    <td width='38%' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'>&nbsp;</td>" +
                       "<td width='28%' align='center' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><a href='#'><img src='http://aphidbyte.jobseeders.com/Images/submit_bg.png' width='115' height='50' /></a></td>" +
                       "</tr>" +
                        "<tr>" +
                        "<td align='left' valign='top'>" + "</td>" +
                        "<td align='center' valign='top'><img src='http://aph500.trainingncr.com/Images/expand.png' width='200' height='30' />" +
                        "</td>" + "<td align='left' valign='top'>&nbsp;</td>" +
                        "</tr>" +
                        "</table>" +
                        "</td>" + "<td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" + "</tr>" + "</table>" + "</td>" + "</tr>" + "<tr>" + "</tr>" +
                        "</table>" +
                        "<!-- Content section -->" +
                        "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Footer section --><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top' width='21'>&nbsp;</td>" +
                        "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #7f7f7f; text-decoration: none; line-height:5px; text-align:center;'><p>Copyright and TM &copy; 2013 AphidByte LLC. 5403 Treelodge Pkwy,  Atlanta, GA 30350. </p><p>All Rights Reserved/ Privacy Policy / If you&rsquo;ve received this email  in error, <a href='#' style='text-decoration:underline; color:#7f7f7f;'>Unsubscribe here</a></p></td><td align='left' valign='top' width='21'>&nbsp;</td>" + "</tr>" + "</table>" + "</td>" + "</tr>" + "<tr>" +
                        "<td align='left' valign='top'>" + "</td>" + "</tr>" +
                        "</table><!-- Footer section --></td>" +
                        "</tr>" +
                        "</table><!-- 600px fixed center aligned wrapper table -->" +
                        "</td>" +
                        "</tr>" +
                        "</table>";
        string slider10 = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'>" +
                       "<tr>" +
                       "<td align='left' valign='top'><!-- 600px fixed center aligned wrapper table --><table width='600' border='0' cellspacing='0' cellpadding='0' align='center' class='Fluid_wrapper'>" +
                       "<tr>" +
                       "<td align='left' valign='top' bgcolor='#636468'><!-- Header section -->" +
                       "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                       "<tr>" +
                       "<td height='30' colspan='3' align='left' valign='top' style='border-bottom:1px solid #98b955; background:#FFFFFF;'><img src='http://aph500.trainingncr.com/Images/spacer.gif' 10' height='1' alt='' style='display:block;' />  <p style='text-align:center; font-family:Arial, Helvetica, sans-serif; font-size:14px;'> 1800-APH-BYTE I Store I <a href='#' style=' text-decoration:none; color:#000000;'>www.aphidbyte.com</a></p></td>" +
                       "</tr>" +
                        "<tr>" +
                        "<td height='7' colspan='3' align='center' valign='top' style='background:#FFF;'><img src='http://aph500.trainingncr.com/Images/bphidbyte_sicon.png' width='150' height='120' align='middle'/></td>" +
                        "</tr>" +
                         "<tr>" +
                       "<td width='42%' height='8' align='center' valign='top' style='background:#FFF;'>&nbsp;</td>" +
                       "<td width='13%' align='center' valign='top' style='background:#FFF;'><img src='http://aph500.trainingncr.com/Images/lock_image.png' width='66' height='64' /></td>" +
                       "<td width='45%' align='left' valign='middle' style='background:#FFF; font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44;'>" +
                       "<p>Password Reset</p></td>" +
                       "</tr>" +
                       "</table>" + "</td>" +
                       "</tr>" +
                       "<tr>" +
                       "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Content section --><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                       "<tr>" +
                       "</tr>" +
                       "<tr>" +
                       "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" + "<tr>" +
                       "<td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td><td align='left' valign='top' class='Split_cells'>" +
                       "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                       "<tr>"
                       + "<td height='14' colspan='3' align='left' valign='top'>&nbsp;</td>" +
                       "</tr>" +
                       "<tr>" +
                       "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #ff0000; text-decoration: none; line-height: 19px; font-weight:bold;'><p>&nbsp;</p></td>" +
                       "</tr>" +
                       "<tr>" +
                       "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #353e44; text-decoration: none; line-height: 16px;'><table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>" +
                       "<tr>" +
                       "<td align='center'><p>Your password has been successfully changed.</p></td>" +
                       "</tr>" + "<tr>" + "<td>&nbsp;</td>" + "</tr>" + "</table>" + "</td>" +
                       "</tr>" +
                         "<tr>" +
                      "<td width='34%' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'>&nbsp;</td>" +
                      "<td colspan='2' align='right' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><a href='#'><img src='http://aph500.trainingncr.com/Images/proceed_image.png' width='200' height='40' /></a></td>" +
                       "</tr>" +
                        "<tr>" +
                        "<td align='left' valign='top'></td>" +
                        "<td width='38%' align='center' valign='top'>&nbsp;</td>" +
                        "<td width='28%' align='left' valign='top'>&nbsp;</td>" +
                        "</tr>" +
                        "<tr>" + "<td align='left' valign='top'></td>" +
                       "<td width='38%' align='center' valign='top'><img src='http://aph500.trainingncr.com/Images/expand.png' width='200' height='30' /></td>" +
                       "<td width='28%' align='left' valign='top'>&nbsp;</td>" +
                       "</tr>" +
                       "</table>" +
                       "</td>" +
                       "<td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" +
                       "</tr>" +
                       "</table>" +
                       "</td>" +
                       "</tr>" +
                       "<tr>" +
                       "</tr>" +
                       "</table>" +
                       "<!-- Content section --></td>" +
                       "</tr>" + "<tr>" +
                       "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Footer section -->" +
                       "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                       "<tr>" +
                       "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                       "<tr>" +
                       "<td align='left' valign='top' width='21'>&nbsp;</td>" +
                       "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #7f7f7f; text-decoration: none; line-height:5px; text-align:center;'><p>Copyright and TM &copy; 2013 AphidByte LLC. 5403 Treelodge Pkwy,  Atlanta, GA 30350. </p><p>All Rights Reserved/ Privacy Policy / If you&rsquo;ve received this email  in error, <a href='#' style='text-decoration:underline; color:#7f7f7f;'>Unsubscribe here</a></p></td><td align='left' valign='top' width='21'>&nbsp;</td>" +
                       "</tr>" +
                       "</table>" +
                       "</td>" +
                       "</tr>" +
                       "<tr>" +
                       "<td align='left' valign='top'>" +
                       "</td>" +
                       "</tr>" +
                       "</table>" +
                       "<!-- Footer section -->" +
                       "</td>" +
                       "</tr>" +
                       "</table>";
        string slider12 = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'><tr><td align='left' valign='top'><!-- 600px fixed center aligned wrapper table --><table width='600' border='0' cellspacing='0' cellpadding='0' align='center' class='Fluid_wrapper'><tr><td align='left' valign='top' bgcolor='#636468'><!-- Header section --><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td height='30' align='left' valign='top' style='border-bottom:1px solid #98b955; background:#FFFFFF;'><img src='http://aph500.trainingncr.com/Images/spacer.gif' 10' height='1' alt='' style='display:block;' />  <p style='text-align:center; font-family:Arial, Helvetica, sans-serif; font-size:14px;'> 1800-APH-BYTE I Store I <a href='#' style=' text-decoration:none; color:#000000;'>www.aphidbyte.com</a></p></td> </tr><tr><td height='7' align='center' valign='top' style='background:#FFF;'><img src='http://aphidbyte.jobseeders.com/Images/bphidbyte_sicon.png' width='150' height='120' align='middle'/></td>" +
                   "</tr>" + "<tr>" +
                   "<td height='8' align='center' valign='top' style='background:#FFF;'><img src='http://aph500.trainingncr.com/Images/lock_image.png' width='66' height='64' /></td>" +
                   "</tr>" +
                   "</table>" +
                   "</td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Content section -->" +
                   "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr></tr><tr><td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td><td align='left' valign='top' class='Split_cells'><table width='100%' border='0' cellspacing='0' cellpadding='0'>  <tr>      </tr>  <tr>    <td height='14' colspan='3' align='left' valign='top'>&nbsp;</td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 19px;'><p>Hello,</p></td>  </tr>  <tr>    <td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><p>To  reset your password on AphidByte, click here:</p></td>" +
                   "</tr>" +
                   "<tr>" +
                   "<td height='7' colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color:#353e44; text-decoration: none; line-height: 16px;'><p>The account  information for your AphidByte account containing email <a href='#'>bookinglivfli@gmail.com</a> was modified on 07/24/2013. If you believe these changes were made in error, or if you believe an unauthorized person accessed your account, please reset your account password immediately by going to <a href='#'>www.aphidbyte.com/Login</a></p></td>" +
                   "</tr>" +
                    "<tr>" +
                    "<td height='3' colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color:#353e44; text-decoration: none; line-height:45px;'>To view your account settings, sign in to <a href='#'>www.aphidbyte.com/login</a></td>" +
                     "</tr>" +
                     "<tr>" +
                       "<td height='4' colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color:#353e44; text-decoration: none; line-height:16px;'>Please do not reply to this email. This is an automated message. For further questions visit <a href='#'>www.aphidbyte.com/support</a>" + "</td>" +
                     "</tr>" +
                     "<tr>" +
                       "<td height='7' colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><p>Thank you,</p></td>" +
                     "</tr>" +
                     "<tr>" +
                       "<td height='7' colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><p>AphidByte Staff</p></td>" +
                     "</tr>" +
                     "<tr>" +
                       "<td height='14' colspan='3' align='left' valign='top'></td>" +
                    "</tr>" +
                     "<tr>" +
               "<td align='left' valign='top'></td>" +
               "<td align='center' valign='top'><img src='http://aph500.trainingncr.com/Images/expand.png' width='200' height='30' /></td>" +
               "<td align='left' valign='top'>&nbsp;</td>  </tr>  </table></td><td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" +
               "</tr>"
               + "</table>"
               + "</td>"
               + "</tr>" +
               "<tr>" +
               "</tr>" +
               "</table>" +
               "<!-- Content section -->" +
               "</td>" +
               "</tr>" +
               "<tr>" +
               "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Footer section -->" +
               "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
               "<tr>" +
               "<td align='left' valign='top'>" +
               "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
               "<tr>" +
               "<td align='left' valign='top' width='21'>&nbsp;</td><td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #7f7f7f; text-decoration: none; line-height:5px; text-align:center;'><p>Copyright and TM &copy; 2013 AphidByte LLC. 5403 Treelodge Pkwy,  Atlanta, GA 30350. </p><p>All Rights Reserved/ Privacy Policy / If you&rsquo;ve received this email  in error, <a href='#' style='text-decoration:underline; color:#7f7f7f;'>Unsubscribe here</a></p></td><td align='left' valign='top' width='21'>&nbsp;</td>" +
               "</tr>" +
               "</table>" +
               "</td>" +
               "</tr>" +
               "<tr>" +
               "<td align='left' valign='top'>" +
               "</td>" +
               "</tr>" +
               "</table>" +
               "<!-- Footer section --></td>" +
               "</tr>" +
               "</table>" +
               "<!-- 600px fixed center aligned wrapper table -->" +
               "</td>" +
               "</tr>" +
               "</table>";
        string slider14 = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'><tr><td align='left' valign='top'><!-- 600px fixed center aligned wrapper table -->" +
            "<table width='600' border='0' cellspacing='0' cellpadding='0' align='center' class='Fluid_wrapper'>" +
            "<tr>" +
            "<td align='left' valign='top' bgcolor='#636468'><!-- Header section -->" +
            "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
            "<tr>" +
            "<td height='30' align='left' valign='top' style='border-bottom:1px solid #98b955; background:#FFFFFF;'>" +
            "<img src='http://aph500.trainingncr.com/Images/spacer.gif' 10' height='1' alt='' style='display:block;' />" +
            "<p style='text-align:center; font-family:Arial, Helvetica, sans-serif; font-size:14px;'>1800-APH-BYTE I Store I <a href='#' style=' text-decoration:none; color:#000000;'>www.aphidbyte.com</a></p></td>" +
            "</tr>" +
            "<tr>" +
            "<td height='15' align='center' valign='top' style='background:#FFF;'>" +
            "<img src='http://aph500.trainingncr.com/Images/bphidbyte_sicon.png' width='150' height='120' align='middle'/>" +
            "</td>" +
            "</tr>" +
            "</table>" +
            "</td>" +
            "</tr>" +
            "<tr>" +
            "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Content section --><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr></tr><tr><td align='left' valign='top'>" +
            "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td><td align='left' valign='top' class='Split_cells'>" +
            "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
            "<tr>" +
            "<td colspan='3' align='left' valign='top' class='Heading' style='font-family:A bite; font-size: 30px; color: #595959; text-decoration: none; line-height:30px;'>How Did We Do?</td>" +
            "</tr>" +
            "<tr>" +
            "<td height='14' colspan='3' align='left' valign='top'><img src='http://aph500.trainingncr.com/Images/spacer.gif' width='1' height='14' alt='' style='display:block;'/>" + "</td>" +
            "</tr>" +
            "<tr>" +
            "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 15px; color: #353e44; text-decoration: none; line-height: 19px;'>" +
            "<p>We&#8217;d love to hear how your call went with &#8220;NAME&#8221; from our Customer Service Department. Please click on one of the following to rate your experience.</p>" +
            "</td>" +
            "</tr>" +
            "<tr>" +
            "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 15px; color: #353e44; text-decoration: none; line-height: 16px;'><p>How would you rate your AphidByte customer service experience?</p></td>" + "</tr>" + "<tr>" +
            "<td height='7' colspan='3' align='left' valign='top'></td>" + "</tr>" +
            "<tr>" +
            "<td height='7' colspan='3' align='left' valign='top'></td>" +
            "</tr>" +
            "<tr>" +
            "<td height='14' colspan='3' align='left' valign='top'></td>" +
            "</tr>" +
            "<tr> " +
            "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'>" +
            "<a href='#'></a><table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'><tr>" +
            "<td>" + "<a href='#'><img src='http://aph500.trainingncr.com/Images/great_image.png' width='115' height='50' /></a>" +
            "</td>" +
  "<td><a href='#'><img src='http://aph500.trainingncr.com/Images/good_image.png' width='115' height='50' /></a></td>" +
  "<td><a href='#'><img src='http://aph500.trainingncr.com/Images/pair_image.png' width='115' height='50' /></a></td>" +
  "<td><a href='#'><img src='http://aph500.trainingncr.com/Images/poor_image.png' width='115' height='50' /></a></td>" +
  "</tr>" +
"<tr>" +
  "<td colspan='4'>&nbsp;</td>" +
"</tr>" +
"<tr>" +
  "<td colspan='4'>&nbsp;</td>" +
"</tr>" +
"</table>" + "</td>" +
"</tr>" +
"<tr>" +
"<td width='17%' align='left' valign='top'>" + "</td>" +
"<td width='38%' align='center' valign='top'><img src='http://aph500.trainingncr.com/Images/expand.png' width='200' height='30' /></td>" +
"<td width='28%' align='left' valign='top'>&nbsp;</td>" +
"</tr>" +
"</table>" +
"</td>" +
"<td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" +
"</tr>" +
"</table>" +
"</td>" +
"</tr>" +
"<tr>" +
"</tr>" +
"</table>" +
"<!-- Content section -->" +
"</td>" +
"</tr>" +
"<tr>" +
"<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Footer section -->" +
"<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
"<tr>" +
"<td align='left' valign='top'>" +
"<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
"<tr>" +
"<td align='left' valign='top' width='21'>&nbsp;</td>" +
"<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #7f7f7f; text-decoration: none; line-height:5px; text-align:center;'><p>Copyright and TM &copy; 2013 AphidByte LLC. 5403 Treelodge Pkwy,  Atlanta, GA 30350. </p><p>All Rights Reserved/ Privacy Policy / If you&rsquo;ve received this email  in error, <a href='#' style='text-decoration:underline; color:#7f7f7f;'>Unsubscribe here</a></p></td>" +
"<td align='left' valign='top' width='21'>&nbsp;</td>" +
"</tr>" +
"</table>" +
"</td>" +
"</tr>" +
"<tr>" +
"<td align='left' valign='top'></td>" +
"</tr>" +
"</table><!-- Footer section --></td></tr></table>" +
"<!-- 600px fixed center aligned wrapper table -->" +
"</td>" +
"</tr>" +
"</table>";

        string slider15 = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'>" +
                        "<tr>" +
                        "<td align='left' valign='top'><!-- 600px fixed center aligned wrapper table -->" +
                        "<table width='600' border='0' cellspacing='0' cellpadding='0' align='center' class='Fluid_wrapper'><tr><td align='left' valign='top' bgcolor='#636468'><!-- Header section --><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td height='30' align='left' valign='top' style='border-bottom:1px solid #98b955; background:#FFFFFF;'><img src='http://aphidbytes.jobseeders.com/Images/spacer.gif' 10' height='1' alt='' style='display:block;'/><p style='text-align:center; font-family:Arial, Helvetica, sans-serif; font-size:14px;'> 1800-APH-BYTE I Store I <a href='#' style=' text-decoration:none; color:#000000;'>www.aphidbyte.com</a></p></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td height='15' align='center' valign='top' style='background:#FFF;'>" + "<img src='http://aph500.trainingncr.com/Images/bphidbyte_sicon.png' width='150' height='120' align='middle'/>" +
                        "</td>" +
                        "</tr>" +
                        "</table>" +
                        "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Content section -->" +
                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                        "<tr>" +
                        "</tr>" +
                        "<tr>" +
                        "<td align='left' valign='top'>" +
                        "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                        "<tr>" +
                        "<td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td><td align='left' valign='top' class='Split_cells'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                        "<tr>" +
                        "<td colspan='3' align='left' valign='top' class='Heading' style='font-family:A bite; font-size: 30px; color: #595959; text-decoration: none; line-height:30px;'>FeedBack</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td height='14' colspan='3' align='left' valign='top'>" +
                        "<img src='http://aph500.trainingncr.com/Images/spacer.gif' width='1' height='14' alt='' style='display:block;' />" + "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 15px; color: #353e44; text-decoration: none; line-height: 19px;'><p>Thanks you for your feedback</p></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 15px; color: #353e44; text-decoration: none; line-height: 16px;'><p>What could we have done to make your experience better?</p></td>" +
                        "</tr>" + "<tr>" +
                        "<td height='7' colspan='3' align='left' valign='top'></td>" + "</tr>" + "<tr>"
                         + "<td height='7' colspan='3' align='left' valign='top'></td>" + "</tr>" +
                       "<tr>" +
                        "<td height='14' colspan='3' align='left' valign='top'></td>" +
       "</tr>" +
       "<tr>" +
       "<td colspan='3' align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #353e44; text-decoration: none; line-height: 16px;'><a href='#'></a>" +
           "<table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>" +
           "<tr>" +
             "<td colspan='2' align='center' valign='middle'><textarea name='textarea2' cols='100' rows='8' id='textarea2' style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; color:#353e44; border:3px solid #385d8a; padding:5px; box-shadow: 0 4px 2px -2px rgba(0,0,0,0.4)'></textarea></td>" +
               "</tr>" +
               "<tr>" +
                "<td colspan='2'>&nbsp;</td>" +
                  "</tr>" +
              "<tr>" +
             "<td width='81%'>&nbsp;</td>" +
             "<td width='19%'><a href='#'><img src='http://aph500.trainingncr.com/Images/submit_bg2.png' width='130' height='40' /></a></td>" +
           "</tr>" +
           "</table>" +
           "</td>" +
         "</tr>" +
          "<tr>" +
          "<td width='26%' align='left' valign='top'>" + "</td>"
          + "<td width='45%' align='center' valign='top'><img src='http://aph500.trainingncr.com/Images/expand.png' width='200' height='30' /></td>" +
          "<td width='29%' align='left' valign='top'>&nbsp;</td>" +
          "</tr>" +
          "</table>" +
          "</td>" +
          "<td align='left' valign='top' width='20' class='No_mobile'>&nbsp;</td>" +
          "</tr>" + "</table>" +
          "</td>" +
          "</tr>" +
          "<tr>" +
          "</tr>" +
          "</table><!-- Content section --></td>" +
          "</tr>" +
          "<tr>" +
          "<td align='left' valign='top' bgcolor='#FFFFFF'><!-- Footer section -->" +
          "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
          "<tr>" +
          "<td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
          "<tr>" +
          "<td align='left' valign='top' width='21'>&nbsp;</td>" +
          "<td align='left' valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #7f7f7f; text-decoration: none; line-height:5px; text-align:center;'><p>Copyright and TM &copy; 2013 AphidByte LLC. 5403 Treelodge Pkwy,  Atlanta, GA 30350. </p>" +
          "<p>All Rights Reserved/ Privacy Policy / If you&rsquo;ve received this email  in error, <a href='#' style='text-decoration:underline; color:#7f7f7f;'>Unsubscribe here</a></p></td><td align='left' valign='top' width='21'>&nbsp;</td>" +
          "</tr>" +
          "</table>" +
          "</td>" +
          "</tr>" +
          "<tr>" +
          "<td align='left' valign='top'>" +
          "</td>" +
          "</tr>" +
          "</table><!-- Footer section -->" + "</td>"
          + "</tr>" +
          "</table>" +
          "<!-- 600px fixed center aligned wrapper table -->" +
          "</td>" +
          "</tr>" +
          "</table>";

        public void sendMaill(Guid userId, String email, string accountType, Guid token1, string username, string Subject)
        {

            string strUrlVerify = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            hosturl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Accounts/UserConfirmAccount?token=" + token1;
            string mailformat = "";
            if (accountType == "AphidTise")
            {
               AphidTiseAccoount(token1);
            }
            if (accountType == "AphidLab")
            {
                strUrlVerify = strUrlVerify + "/AphidLabs/verification/?id=" + userId;
                mailformat = AphidLabAccoount(token1,strUrlVerify);
            }
            if (accountType == "Basic")
            {
                strUrlVerify = strUrlVerify + "/Basic/verification/?id=" + userId;
                mailformat = basicAccount(token1, strUrlVerify);
            }
            if (accountType == "Premium")
            {
                strUrlVerify = strUrlVerify + "/Premium/verification/?id=" + userId;
                mailformat = premiumAccount(token1, strUrlVerify);
            }
            if (accountType == "Byter")
            {
                strUrlVerify = strUrlVerify + "/Byter/verification/?id=" + userId;
                mailformat = ByterAccount(token1, strUrlVerify);
            }
            if (accountType == "ForgetPassword")
            {
                mailformat= ForgetPassword(token1, username);
            }
            if (accountType == "FeedBack")
            {
                FeedBackMail();
            }

            //public void sendMail(string userId, string accountType,Guid token1,string username, string Subject)
            //{

            //    hosturl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Accounts/UserConfirmAccount?token=" + token1;
            //    if (accountType=="AphidTise")
            //    {
            //        AphidTiseAccoount(token1);
            //    }
            //    if (accountType == "Basic")
            //    {
            //        basicAccount(token1);
            //    }
            //    if (accountType == "Premium")
            //    {
            //        premiumAccount(token1);
            //    }
            //    if (accountType == "Byter")
            //    {
            //        ByterAccount(token1);
            //    }
            //    if (accountType=="ForgetPassword")
            //    {
            //        ForgetPassword(token1,username);
            //    }
            //    if (accountType=="FeedBack")
            //    {
            //        FeedBackMail();
            //    }
            //MailMessage mail = new MailMessage();
            //MailAddressCollection ms = new MailAddressCollection();
            //ms.Add(new MailAddress("testtechpro@gmail.com"));
            //mail.To.Add(userId);
            //mail.From = new MailAddress("testtechpro@gmail.com");
            //mail.Subject = "Welcome";
            //mail.Body = mailFormat;
            //mail.IsBodyHtml = true;
            //SmtpClient scServer = new SmtpClient("smtp.gmail.com", 587);
            //scServer.EnableSsl = true;
            //scServer.UseDefaultCredentials = false;
            //scServer.Credentials = new NetworkCredential("testtechpro@gmail.com", "test!@#123");
            //scServer.Send(mail);


           

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress("accounts@aphidbyte.com", "Support Aphidbyte");
            message.To.Add(new MailAddress(email));

            message.IsBodyHtml = true;
            message.Subject = Subject;
            message.Body = mailformat;//"<a href='"+ strUrlVerify+"'>click here to verify </a>";
            // message.Body = mailFormat;
            smtp.Host = "smtpout.europe.secureserver.net";
            // smtp.Host = "smtpout.secureserver.net";
            smtp.Port = 25;
            smtp.Timeout = 10000;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("accounts@aphidbyte.com", "byte10billion");
            smtp.Send(message);
        }
    }
}
