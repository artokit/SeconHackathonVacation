import asyncio
import json
from celery import Celery
import config
from depends.dependencies import get_email_service
from schemas.email_notification import GeneratedPasswordRequest

email_service = get_email_service()

celery_app = Celery(
    'tasks',
    broker=config.CELERY_BROKER,
    backend=config.CELERY_BACKEND
)


@celery_app.task
def send_generated_password(
        email_notification_data: str
):
    email_data: GeneratedPasswordRequest = GeneratedPasswordRequest(**json.loads(email_notification_data))
    asyncio.get_event_loop().run_until_complete(email_service.send_generated_password(email_data))
