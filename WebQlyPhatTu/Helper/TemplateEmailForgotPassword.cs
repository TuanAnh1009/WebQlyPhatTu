namespace WebQlyPhatTu.Helper
{
    public class TemplateEmailForgotPassword
    {
        public static string TemplateHTMLEmail(string code, DateTime expireat)
        {
            string htmlContent = @$"
                <!DOCTYPE html>
                <html lang=""vi"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Code Refresh Password</title>
                    <style>
                        /* Đoạn mã CSS của giao diện email */
                    </style>
                </head>
                <body>
                    <p>Your Refresh Code : {code}</p>
                    <p>Expire at : {expireat}</p>
                </body>
                </html>
            ";
            return htmlContent;

        }
    }
}
