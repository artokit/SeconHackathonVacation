from services.email_notifications import EmailNotificationsService


def get_email_service() -> EmailNotificationsService:
    return EmailNotificationsService()
