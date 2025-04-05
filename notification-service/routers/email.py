from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText

from fastapi import APIRouter
from schemas.email import EmailRequest

router = APIRouter()

@router.post("/send-email/")
async def send_email(email_request: EmailRequest):
    try:
        msg = MIMEMultipart()
        msg["From"] = FROM_EMAIL
        msg["To"] = email_request.to_email
        msg["Subject"] = email_request.subject

        # Добавляем текст письма
        msg.attach(MIMEText(email_request.message, "plain"))

        # Подключаемся к SMTP серверу и отправляем письмо
        with smtplib.SMTP(SMTP_SERVER, SMTP_PORT) as server:
            server.starttls()  # Включаем шифрование
            server.login(SMTP_USERNAME, SMTP_PASSWORD)
            server.send_message(msg)

        return {"message": "Email sent successfully"}

    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Failed to send email: {str(e)}")