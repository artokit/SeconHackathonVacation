from fastapi import APIRouter
from schemas.email_notification import EmailNotificationRequest, GeneratedPasswordRequest
import celery_app

router = APIRouter()


@router.post("/generated-password")
async def send_generated_password(email_request: GeneratedPasswordRequest):
    celery_app.send_generated_password.delay(email_request.to_json())
    return {"status": "ok"}



# Todo: доделать методы.
# @router.post("/request-to-director")
# async def send_email(email_request: EmailNotificationRequest):
#     celery_app.send_email.delay(email_request.to_json())
#     return {"status": "ok"}
#
#
# @router.post("/answer-for-request")
# async def send_email(email_request: EmailNotificationRequest):
#     celery_app.send_email.delay(email_request.to_json())
#     return {"status": "ok"}
