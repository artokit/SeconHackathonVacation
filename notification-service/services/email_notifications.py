import os

from schemas.email_notification import GeneratedPasswordRequest
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText
import smtplib
import config


class EmailNotificationsService:
    async def send_generated_password(self, email_request: GeneratedPasswordRequest):
        msg = MIMEMultipart()
        msg["From"] = config.FROM_EMAIL
        msg["To"] = email_request.to_email
        msg["Subject"] = "Ваш новый пароль для доступа к системе"

        with open(os.path.join(config.ASSETS_PATH, "mails", "generate-password-mail.html"), "rb") as f:
            msg.attach(
                MIMEText(
                    f.read().decode().replace("${password}", email_request.password),
                    "html"
                )
            )

        with smtplib.SMTP(config.SMTP_SERVER, config.SMTP_PORT) as server:
            server.starttls()
            server.login(config.SMTP_USERNAME, config.SMTP_PASSWORD)
            server.send_message(msg)

        return {"message": "Email sent successfully"}
